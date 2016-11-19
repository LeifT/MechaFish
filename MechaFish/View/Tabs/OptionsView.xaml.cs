using System;

namespace MechaFish.View.Tabs
{
    /// <summary>
    /// Description for OptionsView.
    /// </summary>
    public partial class OptionsView {
        /// <summary>
        /// Initializes a new instance of the OptionsView class.
        /// </summary>


        public OptionsView() {
            InitializeComponent();
        }

        private void Changed(object sender, EventArgs e) {
            Properties.Settings.Default.Save();
        }
    }
}