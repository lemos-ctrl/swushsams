using DPFP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace BiometricsProject
{
    public partial class verify : capture
    {

        private DPFP.Template Template;
        private DPFP.Verification.Verification Verificator;

        public void Verify(DPFP.Template template)
        {
            Template = template;
            ShowDialog();
        }

        protected override void Process(Sample Sample)
        {
            try
            {
                string MyConnection = "datasource=localhost;username=root;password=";
                string Query = "SELECT * FROM swushsdb.dpfp_fingerprints";
                MySqlConnection MyConn = new MySqlConnection(MyConnection);
                MySqlCommand MyCommand = new MySqlCommand(Query, MyConn);

                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand;
                DataTable dTable = new DataTable();

                MyAdapter.Fill(dTable);

                MySqlDataReader myReader;
                MyConn.Open();
                myReader = MyCommand.ExecuteReader();

                // Iterate through each row (user) in the database
                foreach (DataRow row in dTable.Rows)
                {
                    // Retrieve both left and right index fingerprint data
                    byte[] leftFingerprintData = (byte[])row["left_index_fingerprint"];
                    byte[] rightFingerprintData = (byte[])row["right_index_fingerprint"];

                    // Create memory streams for each fingerprint
                    MemoryStream leftMs = new MemoryStream(leftFingerprintData);
                    MemoryStream rightMs = new MemoryStream(rightFingerprintData);

                    // Deserialize the templates
                    DPFP.Template leftTemplate = new DPFP.Template();
                    leftTemplate.DeSerialize(leftMs);

                    DPFP.Template rightTemplate = new DPFP.Template();
                    rightTemplate.DeSerialize(rightMs);

                    // Process the fingerprint sample for verification
                    base.Process(Sample);

                    // Extract the features from the sample
                    DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);

                    if (features != null)
                    {
                        // Verify the left index fingerprint
                        DPFP.Verification.Verification.Result resultLeft = new DPFP.Verification.Verification.Result();
                        Verificator.Verify(features, leftTemplate, ref resultLeft);

                        // Verify the right index fingerprint if the left one fails
                        DPFP.Verification.Verification.Result resultRight = new DPFP.Verification.Verification.Result();
                        Verificator.Verify(features, rightTemplate, ref resultRight);

                        // Update status for the FAR (False Accept Rate)
                        UpdateStatus(Math.Min(resultLeft.FARAchieved, resultRight.FARAchieved));

                        // If either fingerprint is verified
                        if (resultLeft.Verified || resultRight.Verified)
                        {
                            // Report which fingerprint was verified
                            if (resultLeft.Verified)
                            {
                                MakeReport("The Left Index Fingerprint was verified as " + row["fname"].ToString());
                            }
                            else
                            {
                                MakeReport("The Right Index Fingerprint was verified as " + row["fname"].ToString());
                            }

                            Setfname(row["fname"].ToString());
                            SetUserID(row["user_id"].ToString());
                            MyConn.Close();
                            break;
                        }
                        else
                        {
                            MakeReport("The fingerprint was not verified.");
                            Setfname("NO DATA");
                            SetUserID("NO DATA");
                        }
                    }
                }

                MyConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override void Init()
        {
            base.Init();
            base.Text = "Fingerprint Verification";
            Verificator = new DPFP.Verification.Verification();
            UpdateStatus(0);
        }

        private void UpdateStatus(int FAR)
        {
            SetStatus(String.Format("False Accept Rate (FAR) = {0}", FAR));
        }
    }
}
