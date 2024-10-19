using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BiometricsProject
{
    public partial class enroll : capture
    {
        public delegate void OnTemplateEventHandler(DPFP.Template template);
        public event OnTemplateEventHandler OnTemplate;

        private DPFP.Processing.Enrollment EnrollerLeft;
        private DPFP.Processing.Enrollment EnrollerRight;
        private bool isLeftIndexCaptured = false;  // Track if left fingerprint has been captured.

        protected override void Init()
        {
            base.Init();
            base.Text = "Fingerprint Enrollment";
            EnrollerLeft = new DPFP.Processing.Enrollment();
            EnrollerRight = new DPFP.Processing.Enrollment();
            UpdateStatus();
        }

        protected override void Process(DPFP.Sample Sample)
        {
            base.Process(Sample);
            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Enrollment);

            if (features != null)
            {
                try
                {
                    if (!isLeftIndexCaptured)
                    {
                        MakeReport("Left Index Fingerprint feature set was created.");
                        EnrollerLeft.AddFeatures(features);
                    }
                    else
                    {
                        MakeReport("Right Index Fingerprint feature set was created.");
                        EnrollerRight.AddFeatures(features);
                    }
                }
                finally
                {
                    UpdateStatus();

                    if (EnrollerLeft.TemplateStatus == DPFP.Processing.Enrollment.Status.Ready && !isLeftIndexCaptured)
                    {
                        // Left fingerprint captured, now move to the right
                        isLeftIndexCaptured = true;
                        MessageBox.Show("Left Index Fingerprint captured. Now capture the Right Index Fingerprint.");
                        Stop();
                        Start(); // Restart capturing process for the right index.
                    }

                    else if (EnrollerRight.TemplateStatus == DPFP.Processing.Enrollment.Status.Ready && isLeftIndexCaptured)
                    {
                        // Both fingerprints captured, proceed to enrollment
                        OnTemplate(EnrollerLeft.Template);
                        OnTemplate(EnrollerRight.Template);

                        MemoryStream leftFingerprintData = new MemoryStream();
                        MemoryStream rightFingerprintData = new MemoryStream();

                        EnrollerLeft.Template.Serialize(leftFingerprintData);
                        EnrollerRight.Template.Serialize(rightFingerprintData);

                        leftFingerprintData.Position = 0;
                        rightFingerprintData.Position = 0;

                        byte[] leftBytes = new BinaryReader(leftFingerprintData).ReadBytes((Int32)leftFingerprintData.Length);
                        byte[] rightBytes = new BinaryReader(rightFingerprintData).ReadBytes((Int32)rightFingerprintData.Length);

                        try
                        {
                            string MyConnection = "datasource=localhost;username=root;password=";
                            string Query = "SELECT * FROM swushsdb.dpfp_fingerprints WHERE UPPER(fname) = @fname";
                            MySqlConnection MyConn = new MySqlConnection(MyConnection);
                            MySqlCommand MyCommand = new MySqlCommand(Query, MyConn);

                            MyCommand.Parameters.AddWithValue("@fname", FirstName.ToUpper());
                            MyConn.Open();
                            MySqlDataReader myReader = MyCommand.ExecuteReader();

                            int count = 0;
                            while (myReader.Read())
                            {
                                count++;
                            }

                            if (count > 0)
                            {
                                MessageBox.Show("The person you want to register already exists.");
                                Stop();
                                Start();
                            }
                            else
                            {
                                myReader.Close();

                                // Insert the two fingerprints into the database
                                string Query1 = "INSERT INTO swushsdb.dpfp_fingerprints (user_id, fname, left_index_fingerprint, right_index_fingerprint) VALUES (@user_id, @fname, @left_finger, @right_finger)";
                                MySqlCommand MyCommand1 = new MySqlCommand(Query1, MyConn);
                                MyCommand1.Parameters.AddWithValue("@user_id", UserID);
                                MyCommand1.Parameters.AddWithValue("@fname", FirstName);
                                MyCommand1.Parameters.AddWithValue("@left_finger", leftBytes).DbType = DbType.Binary;
                                MyCommand1.Parameters.AddWithValue("@right_finger", rightBytes).DbType = DbType.Binary;

                                MyCommand1.ExecuteNonQuery();
                                MessageBox.Show(FirstName + " was successfully registered with both fingerprints!", "Register Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                MyConn.Close();
                                Stop();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }

                    else if (EnrollerLeft.TemplateStatus == DPFP.Processing.Enrollment.Status.Failed || EnrollerRight.TemplateStatus == DPFP.Processing.Enrollment.Status.Failed)
                    {
                        EnrollerLeft.Clear();
                        EnrollerRight.Clear();
                        Stop();
                        UpdateStatus();
                        OnTemplate(null);
                        Start();
                    }
                }
            }
        }

        private void UpdateStatus()
        {
            if (!isLeftIndexCaptured)
            {
                SetStatus(String.Format("Left Fingerprint Samples Needed: {0}", EnrollerLeft.FeaturesNeeded));
            }
            else
            {
                SetStatus(String.Format("Right Fingerprint Samples Needed: {0}", EnrollerRight.FeaturesNeeded));
            }
        }
    }
}
