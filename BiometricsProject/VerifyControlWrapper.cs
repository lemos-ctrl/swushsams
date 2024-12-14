using System;
using System.Windows.Forms;

namespace BiometricsProject
{
    public partial class VerifyControlWrapper : UserControl
    {
        private VerifyControl verifyControl;

        public VerifyControlWrapper(DPFP.Template template)
        {
            InitializeComponent();
            InitializeVerifyControl(template);
        }

        private void InitializeVerifyControl(DPFP.Template template)
        {
            // Create an instance of VerifyControl (a Form)
            verifyControl = new VerifyControl(template)
            {
                TopLevel = false, // Disable top-level behavior
                FormBorderStyle = FormBorderStyle.None, // Remove borders
                Dock = DockStyle.Fill // Make it fill the wrapper
            };

            // Add VerifyControl to the wrapper
            this.Controls.Add(verifyControl);
            verifyControl.Show(); // Show the VerifyControl inside the wrapper
        }
    }
}
