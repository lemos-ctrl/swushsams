namespace BiometricsProject
{
    partial class main
    {
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.enrol_btn = new System.Windows.Forms.Button();
            this.verify_btn = new System.Windows.Forms.Button();
            this.attendance_btn = new System.Windows.Forms.Button();
            this.contentPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.sidebarPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.sidebarPanel);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.contentPanel);
            this.splitContainer.Size = new System.Drawing.Size(1087, 467);
            this.splitContainer.SplitterDistance = 270;
            this.splitContainer.TabIndex = 0;
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.BackColor = System.Drawing.Color.LightGray;
            this.sidebarPanel.Controls.Add(this.titleLabel);
            this.sidebarPanel.Controls.Add(this.enrol_btn);
            this.sidebarPanel.Controls.Add(this.verify_btn);
            this.sidebarPanel.Controls.Add(this.attendance_btn);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 0);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(270, 467);
            this.sidebarPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.Black;
            this.titleLabel.Location = new System.Drawing.Point(0, 150);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(270, 50);
            this.titleLabel.TabIndex = 3;
            this.titleLabel.Text = "Navigation";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // enrol_btn
            // 
            this.enrol_btn.BackColor = System.Drawing.Color.LightBlue;
            this.enrol_btn.Dock = System.Windows.Forms.DockStyle.Top;
            this.enrol_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.enrol_btn.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.enrol_btn.Location = new System.Drawing.Point(0, 100);
            this.enrol_btn.Name = "enrol_btn";
            this.enrol_btn.Size = new System.Drawing.Size(270, 50);
            this.enrol_btn.TabIndex = 0;
            this.enrol_btn.Text = "Enroll";
            this.enrol_btn.UseVisualStyleBackColor = false;
            this.enrol_btn.Click += new System.EventHandler(this.enrol_btn_Click);
            // 
            // verify_btn
            // 
            this.verify_btn.BackColor = System.Drawing.Color.LightGreen;
            this.verify_btn.Dock = System.Windows.Forms.DockStyle.Top;
            this.verify_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.verify_btn.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.verify_btn.Location = new System.Drawing.Point(0, 50);
            this.verify_btn.Name = "verify_btn";
            this.verify_btn.Size = new System.Drawing.Size(270, 50);
            this.verify_btn.TabIndex = 1;
            this.verify_btn.Text = "Verify";
            this.verify_btn.UseVisualStyleBackColor = false;
            this.verify_btn.Click += new System.EventHandler(this.verify_btn_Click);
            // 
            // attendance_btn
            // 
            this.attendance_btn.BackColor = System.Drawing.Color.LightSalmon;
            this.attendance_btn.Dock = System.Windows.Forms.DockStyle.Top;
            this.attendance_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.attendance_btn.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.attendance_btn.Location = new System.Drawing.Point(0, 0);
            this.attendance_btn.Name = "attendance_btn";
            this.attendance_btn.Size = new System.Drawing.Size(270, 50);
            this.attendance_btn.TabIndex = 2;
            this.attendance_btn.Text = "Attendance";
            this.attendance_btn.UseVisualStyleBackColor = false;
            this.attendance_btn.Click += new System.EventHandler(this.attendance_btn_Click);
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(813, 467);
            this.contentPanel.TabIndex = 0;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 467);
            this.Controls.Add(this.splitContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SWU-SHS AMS Biometrics System";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.sidebarPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Button enrol_btn;
        private System.Windows.Forms.Button verify_btn;
        private System.Windows.Forms.Button attendance_btn;
        private System.Windows.Forms.Label titleLabel;
    }
}
