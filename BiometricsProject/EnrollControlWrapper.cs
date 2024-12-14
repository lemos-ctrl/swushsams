using System;
using System.Windows.Forms;

namespace BiometricsProject
{
    public partial class EnrollControlWrapper : UserControl
    {
        private EnrollControl enrollControl;

        public EnrollControlWrapper()
        {
            InitializeComponent();
            InitializeEnrollControl();
        }

        private void InitializeEnrollControl()
        {
            // Create an instance of EnrollControl (a Form)
            enrollControl = new EnrollControl
            {
                TopLevel = false, // Disable top-level behavior
                FormBorderStyle = FormBorderStyle.None, // Remove borders
                Dock = DockStyle.Fill // Fill the wrapper
            };

            // Add it to the UserControl
            this.Controls.Add(enrollControl);
            enrollControl.Show(); // Show the form within the UserControl
        }
    }
}
