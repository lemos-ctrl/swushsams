using System.Windows.Forms;
using System; // Add this namespace

namespace BiometricsProject
{
    partial class VerifyControl
    {
        private System.ComponentModel.IContainer components = null;

        private Button btnStart;
        private Button btnStop;
        private TextBox txtStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnStart = new Button();
            this.btnStop = new Button();
            this.txtStatus = new TextBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(20, 20);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(150, 40);
            this.btnStart.Text = "Start Verification";
            this.btnStart.Click += new EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(20, 70);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(150, 40);
            this.btnStop.Text = "Stop Verification";
            this.btnStop.Click += new EventHandler(this.btnStop_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(20, 120);
            this.txtStatus.Multiline = true;
            this.txtStatus.ScrollBars = ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(300, 200);
            this.txtStatus.ReadOnly = true;
            // 
            // VerifyControl
            // 
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.txtStatus);
            this.Size = new System.Drawing.Size(800, 450);
            this.ResumeLayout(false);
        }
    }
}
