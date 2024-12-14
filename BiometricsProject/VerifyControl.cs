using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using DPFP;

namespace BiometricsProject
{
    public partial class VerifyControl : capture
    {
        private DPFP.Template Template;
        private DPFP.Verification.Verification Verificator;

        public VerifyControl(DPFP.Template template)
        {
            InitializeComponent();
            this.Template = template;
            Init(); // Initialize fingerprint capture
        }

        protected override void Init()
        {
            base.Init();
            Verificator = new DPFP.Verification.Verification();
            UpdateStatus(0);
        }

        protected override void Process(Sample sample)
        {
            try
            {
                // Database connection
                string connectionString = "datasource=localhost;username=root;password=";
                string query = "SELECT * FROM swushsdb.dpfp_fingerprints";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    connection.Open();

                    // Process sample and extract features
                    base.Process(sample);
                    DPFP.FeatureSet features = ExtractFeatures(sample, DPFP.Processing.DataPurpose.Verification);

                    if (features != null)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            byte[] leftFingerprintData = (byte[])row["left_index_fingerprint"];
                            byte[] rightFingerprintData = (byte[])row["right_index_fingerprint"];

                            // Deserialize templates
                            DPFP.Template leftTemplate = new DPFP.Template(new MemoryStream(leftFingerprintData));
                            DPFP.Template rightTemplate = new DPFP.Template(new MemoryStream(rightFingerprintData));

                            // Verify fingerprints
                            DPFP.Verification.Verification.Result leftResult = new DPFP.Verification.Verification.Result();
                            DPFP.Verification.Verification.Result rightResult = new DPFP.Verification.Verification.Result();

                            Verificator.Verify(features, leftTemplate, ref leftResult);
                            Verificator.Verify(features, rightTemplate, ref rightResult);

                            UpdateStatus(Math.Min(leftResult.FARAchieved, rightResult.FARAchieved));

                            if (leftResult.Verified || rightResult.Verified)
                            {
                                // Display verification result
                                string verifiedUser = row["fname"].ToString();
                                MakeReport(leftResult.Verified
                                    ? $"The Left Index Fingerprint was verified as {verifiedUser}"
                                    : $"The Right Index Fingerprint was verified as {verifiedUser}");

                                Setfname(verifiedUser);
                                SetUserID(row["user_id"].ToString());
                                return;
                            }
                        }

                        MakeReport("The fingerprint was not verified.");
                        Setfname("NO DATA");
                        SetUserID("NO DATA");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void UpdateStatus(int FAR)
        {
            SetStatus($"False Accept Rate (FAR) = {FAR}");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Start(); // Start fingerprint capture
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop(); // Stop fingerprint capture
        }
    }
}
