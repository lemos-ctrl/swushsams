using DPFP;
using DPFP.Capture;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace BiometricsProject
{
    public partial class attendance : Form, DPFP.Capture.EventHandler
    {
        private DPFP.Template Template;
        private DPFP.Capture.Capture Capturer;
        private DPFP.Verification.Verification Verificator;
        private bool IsVerified = false;
        private string VerifiedUserName;
        private int VerifiedUserId;

        public attendance()
        {
            InitializeComponent();
            Init();
            StartCapture();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void Init()
        {
            try
            {
                Capturer = new DPFP.Capture.Capture();
                if (Capturer != null)
                {
                    Capturer.EventHandler = this;
                    Verificator = new DPFP.Verification.Verification();
                }
                else
                {
                    MakeReport("Can't initiate fingerprint capture.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StartCapture()
        {
            if (Capturer != null)
            {
                try
                {
                    Capturer.StartCapture();
                    MakeReport("Using the fingerprint reader...");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void StopCapture()
        {
            if (Capturer != null)
            {
                try
                {
                    Capturer.StopCapture();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
            MakeReport("The fingerprint sample was captured.");
            Process(Sample);
        }

        public async void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            // Wait for 3 seconds before showing the message
            await Task.Delay(3000);
            MakeReport("The finger was removed from the reader.");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The finger was placed on the reader.");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The fingerprint reader was connected.");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The fingerprint reader was disconnected.");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
                MakeReport("The quality of the fingerprint sample is good.");
            else
                MakeReport("The quality of the fingerprint sample is poor.");
        }

        protected void Process(DPFP.Sample Sample)
        {
            // Extract features from the sample
            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);
            if (features != null)
            {
                // Load fingerprints from the database and verify
                VerifyFingerprint(features);
            }
        }

        private void VerifyFingerprint(DPFP.FeatureSet features)
        {
            try
            {
                string MyConnection = "datasource=localhost;username=root;password=;database=swushsdb";
                string Query = "SELECT * FROM dpfp_fingerprints";
                MySqlConnection MyConn = new MySqlConnection(MyConnection);
                MySqlCommand MyCommand = new MySqlCommand(Query, MyConn);

                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand;
                DataTable dTable = new DataTable();

                MyAdapter.Fill(dTable);

                foreach (DataRow row in dTable.Rows)
                {
                    byte[] leftFingerprintData = (byte[])row["left_index_fingerprint"];
                    byte[] rightFingerprintData = (byte[])row["right_index_fingerprint"];

                    MemoryStream leftMs = new MemoryStream(leftFingerprintData);
                    MemoryStream rightMs = new MemoryStream(rightFingerprintData);

                    DPFP.Template leftTemplate = new DPFP.Template();
                    leftTemplate.DeSerialize(leftMs);

                    DPFP.Template rightTemplate = new DPFP.Template();
                    rightTemplate.DeSerialize(rightMs);

                    DPFP.Verification.Verification.Result resultLeft = new DPFP.Verification.Verification.Result();
                    Verificator.Verify(features, leftTemplate, ref resultLeft);

                    DPFP.Verification.Verification.Result resultRight = new DPFP.Verification.Verification.Result();
                    Verificator.Verify(features, rightTemplate, ref resultRight);

                    if (resultLeft.Verified || resultRight.Verified)
                    {
                        // Fingerprint verified
                        VerifiedUserName = row["fname"].ToString();
                        VerifiedUserId = Convert.ToInt32(row["user_id"]);
                        IsVerified = true;

                        MySqlConnection conn = new MySqlConnection(MyConnection);
                        conn.Open();

                        // Check if the user is on leave for today
                        string leaveQuery = "SELECT * FROM leave_records WHERE user_id = @UserId AND leave_status = 'Approved' AND CURDATE() BETWEEN leave_start_date AND leave_end_date";
                        MySqlCommand leaveCmd = new MySqlCommand(leaveQuery, conn);
                        leaveCmd.Parameters.AddWithValue("@UserId", VerifiedUserId);

                        MySqlDataReader leaveReader = leaveCmd.ExecuteReader();

                        if (leaveReader.HasRows)
                        {
                            // User is on leave for today
                            MakeReport($"{VerifiedUserName} is on leave today. Attendance action is not allowed.");
                            leaveReader.Close();
                            conn.Close();
                            return; // Stop any further actions for attendance
                        }
                        leaveReader.Close();

                        // Check if the user has already completed attendance for the day
                        string checkCompletionQuery = "SELECT * FROM attendance WHERE user_id = @UserId AND attendance_date = CURDATE() AND attendance_complete = 1";
                        MySqlCommand checkCompletionCmd = new MySqlCommand(checkCompletionQuery, conn);
                        checkCompletionCmd.Parameters.AddWithValue("@UserId", VerifiedUserId);

                        MySqlDataReader completionReader = checkCompletionCmd.ExecuteReader();

                        if (completionReader.HasRows)
                        {
                            // Attendance for today is already completed
                            MakeReport($"{VerifiedUserName} has already completed attendance for today. No further action is allowed.");
                            completionReader.Close();
                        }
                        else
                        {
                            completionReader.Close();

                            // Check if the user is currently checked in
                            string checkStatusQuery = "SELECT * FROM attendance WHERE user_id = @UserId AND attendance_date = CURDATE() AND is_checked_in = 1";
                            MySqlCommand checkStatusCmd = new MySqlCommand(checkStatusQuery, conn);
                            checkStatusCmd.Parameters.AddWithValue("@UserId", VerifiedUserId);

                            MySqlDataReader reader = checkStatusCmd.ExecuteReader();

                            if (reader.HasRows)
                            {
                                // User is currently checked in, so log them out
                                reader.Close();
                                string updateQuery = "UPDATE attendance SET check_out_time = @CheckOutTime, is_checked_in = 0, attendance_complete = 1 WHERE user_id = @UserId AND attendance_date = CURDATE() AND is_checked_in = 1";
                                MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                                updateCmd.Parameters.AddWithValue("@UserId", VerifiedUserId);
                                updateCmd.Parameters.AddWithValue("@CheckOutTime", DateTime.Now);
                                updateCmd.ExecuteNonQuery();

                                MakeReport($"{VerifiedUserName} has been logged out at {DateTime.Now.ToString("hh:mm:ss tt")}");
                            }
                            else
                            {
                                // User is not checked in, so log them in
                                reader.Close();
                                string insertQuery = "INSERT INTO attendance (user_id, check_in_time, attendance_date, status, is_checked_in, is_late, attendance_complete) VALUES (@UserId, @CheckInTime, @AttendanceDate, 'Present', 1, @IsLate, 0)";
                                MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                                insertCmd.Parameters.AddWithValue("@UserId", VerifiedUserId);
                                insertCmd.Parameters.AddWithValue("@CheckInTime", DateTime.Now);
                                insertCmd.Parameters.AddWithValue("@AttendanceDate", DateTime.Now.Date);

                                // Late arrival detection logic
                                bool isLate = DateTime.Now.TimeOfDay > new TimeSpan(9, 0, 0); // Assuming 9:00 AM is the cutoff
                                insertCmd.Parameters.AddWithValue("@IsLate", isLate ? 1 : 0);

                                insertCmd.ExecuteNonQuery();

                                MakeReport($"{VerifiedUserName} has been logged in at {DateTime.Now.ToString("hh:mm:ss tt")}");
                            }
                        }

                        conn.Close();
                        break;
                    }
                }

                if (!IsVerified)
                {
                    MakeReport("Fingerprint not verified.");
                }

                MyConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }





        private void MarkAttendance(int userId, DateTime checkInTime)
        {
            try
            {
                string MyConnection = "datasource=localhost;username=root;password=;database=swushsdb";
                string Query = "INSERT INTO attendance (user_id, check_in_time, attendance_date, status) VALUES (@UserId, @CheckInTime, @AttendanceDate, 'Present')";
                MySqlConnection MyConn = new MySqlConnection(MyConnection);
                MySqlCommand MyCommand = new MySqlCommand(Query, MyConn);

                MyCommand.Parameters.AddWithValue("@UserId", userId);
                MyCommand.Parameters.AddWithValue("@CheckInTime", checkInTime);
                MyCommand.Parameters.AddWithValue("@AttendanceDate", checkInTime.Date);

                MyConn.Open();
                MyCommand.ExecuteNonQuery();
                MyConn.Close();

                // Ensure the UI update is marshaled to the main thread
                if (lblStatus.InvokeRequired)
                {
                    lblStatus.BeginInvoke(new Action(() =>
                        lblStatus.Text = $"Status: {VerifiedUserName} logged in at {checkInTime.ToString("hh:mm:ss tt")}."
                    ));
                }
                else
                {
                    lblStatus.Text = $"Status: {VerifiedUserName} logged in at {checkInTime.ToString("hh:mm:ss tt")}.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            DPFP.Processing.FeatureExtraction extractor = new DPFP.Processing.FeatureExtraction();
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet features = new DPFP.FeatureSet();

            extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);
            if (feedback == DPFP.Capture.CaptureFeedback.Good)
            {
                return features;
            }
            else
            {
                return null;
            }
        }

        private void MakeReport(string message)
        {
            if (lblStatus.InvokeRequired)
            {
                // Using BeginInvoke to avoid blocking UI
                lblStatus.BeginInvoke(new Action(() => lblStatus.Text = message));
            }
            else
            {
                lblStatus.Text = message;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            StopCapture();
            this.Close();
        }
    }
}
