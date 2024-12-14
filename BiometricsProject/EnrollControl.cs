using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BiometricsProject
{
    public partial class EnrollControl : capture
    {
        public delegate void OnTemplateEventHandler(DPFP.Template template);
        public event OnTemplateEventHandler OnTemplate;

        private DPFP.Processing.Enrollment EnrollerLeft;
        private DPFP.Processing.Enrollment EnrollerRight;
        private bool isLeftIndexCaptured = false; // Track if left fingerprint has been captured.

        public EnrollControl()
        {
            InitializeComponent();
            Init(); // Initialize the capture logic
        }

        protected override void Init()
        {
            base.Init();
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
                        isLeftIndexCaptured = true;
                        MessageBox.Show("Left Index Fingerprint captured. Now capture the Right Index Fingerprint.");
                        Stop();
                        Start(); // Restart capturing process for the right index.
                    }
                    else if (EnrollerRight.TemplateStatus == DPFP.Processing.Enrollment.Status.Ready && isLeftIndexCaptured)
                    {
                        OnTemplate?.Invoke(EnrollerLeft.Template);
                        OnTemplate?.Invoke(EnrollerRight.Template);

                        SaveFingerprintsToDatabase(EnrollerLeft.Template, EnrollerRight.Template);
                    }
                    else if (EnrollerLeft.TemplateStatus == DPFP.Processing.Enrollment.Status.Failed ||
                             EnrollerRight.TemplateStatus == DPFP.Processing.Enrollment.Status.Failed)
                    {
                        EnrollerLeft.Clear();
                        EnrollerRight.Clear();
                        Stop();
                        UpdateStatus();
                        OnTemplate?.Invoke(null);
                        Start();
                    }
                }
            }
        }

        private void SaveFingerprintsToDatabase(DPFP.Template leftTemplate, DPFP.Template rightTemplate)
        {
            try
            {
                using (var conn = new MySqlConnection("datasource=localhost;username=root;password="))
                {
                    conn.Open();

                    string checkQuery = "SELECT * FROM swushsdb.dpfp_fingerprints WHERE UPPER(fname) = @fname";
                    using (var checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@fname", FirstName.ToUpper());
                        using (var reader = checkCmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                MessageBox.Show("The person you want to register already exists.");
                                return;
                            }
                        }
                    }

                    string insertQuery = "INSERT INTO swushsdb.dpfp_fingerprints (user_id, fname, left_index_fingerprint, right_index_fingerprint) VALUES (@user_id, @fname, @left_finger, @right_finger)";
                    using (var insertCmd = new MySqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@user_id", UserID);
                        insertCmd.Parameters.AddWithValue("@fname", FirstName);

                        using (var leftStream = new MemoryStream())
                        using (var rightStream = new MemoryStream())
                        {
                            leftTemplate.Serialize(leftStream);
                            rightTemplate.Serialize(rightStream);

                            insertCmd.Parameters.AddWithValue("@left_finger", leftStream.ToArray()).DbType = DbType.Binary;
                            insertCmd.Parameters.AddWithValue("@right_finger", rightStream.ToArray()).DbType = DbType.Binary;

                            insertCmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show($"{FirstName} was successfully registered!", "Register Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void UpdateStatus()
        {
            if (!isLeftIndexCaptured)
            {
                SetStatus($"Left Fingerprint Samples Needed: {EnrollerLeft.FeaturesNeeded}");
            }
            else
            {
                SetStatus($"Right Fingerprint Samples Needed: {EnrollerRight.FeaturesNeeded}");
            }
        }
    }
}
