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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.enrol_btn = new System.Windows.Forms.Button();
            this.verify_btn = new System.Windows.Forms.Button();
            this.attendance_btn = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.flowLayoutPanel, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.564103F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.72649F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.709402F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(792, 468);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flowLayoutPanel.AutoSize = true;
            this.flowLayoutPanel.Controls.Add(this.label1);
            this.flowLayoutPanel.Controls.Add(this.enrol_btn);
            this.flowLayoutPanel.Controls.Add(this.verify_btn);
            this.flowLayoutPanel.Controls.Add(this.attendance_btn);
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.Location = new System.Drawing.Point(143, 52);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(506, 368);
            this.flowLayoutPanel.TabIndex = 1;
            this.flowLayoutPanel.WrapContents = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(492, 113);
            this.label1.TabIndex = 2;
            this.label1.Text = "SWU SHS-AMS BIOMETRIC SYSTEM";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // enrol_btn
            // 
            this.enrol_btn.BackColor = System.Drawing.Color.LightBlue;
            this.enrol_btn.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.enrol_btn.Location = new System.Drawing.Point(3, 116);
            this.enrol_btn.Name = "enrol_btn";
            this.enrol_btn.Size = new System.Drawing.Size(500, 79);
            this.enrol_btn.TabIndex = 0;
            this.enrol_btn.Text = "Enroll";
            this.enrol_btn.UseVisualStyleBackColor = false;
            this.enrol_btn.Click += new System.EventHandler(this.enrol_btn_Click);
            // 
            // verify_btn
            // 
            this.verify_btn.BackColor = System.Drawing.Color.LightGreen;
            this.verify_btn.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.verify_btn.Location = new System.Drawing.Point(3, 201);
            this.verify_btn.Name = "verify_btn";
            this.verify_btn.Size = new System.Drawing.Size(500, 79);
            this.verify_btn.TabIndex = 1;
            this.verify_btn.Text = "Verify";
            this.verify_btn.UseVisualStyleBackColor = false;
            this.verify_btn.Click += new System.EventHandler(this.verify_btn_Click);
            // 
            // attendance_btn
            // 
            this.attendance_btn.BackColor = System.Drawing.Color.LightSalmon;
            this.attendance_btn.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.attendance_btn.Location = new System.Drawing.Point(3, 286);
            this.attendance_btn.Name = "attendance_btn";
            this.attendance_btn.Size = new System.Drawing.Size(500, 79);
            this.attendance_btn.TabIndex = 3;
            this.attendance_btn.Text = "Attendance";
            this.attendance_btn.UseVisualStyleBackColor = false;
            this.attendance_btn.Click += new System.EventHandler(this.attendance_btn_Click);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::BiometricsProject.Properties.Resources.swubg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(792, 468);
            this.Controls.Add(this.tableLayoutPanel);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Biometrics System";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.flowLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button enrol_btn;
        private System.Windows.Forms.Button verify_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button attendance_btn;
    }
}
