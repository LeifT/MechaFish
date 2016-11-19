using System;
using System.Diagnostics;
using System.Windows.Controls;
using MechaFish.ViewModel;

namespace MechaFish
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void Populate(object sender, EventArgs e) {
            ComboBox comboBox = sender as ComboBox;

            if (comboBox != null) {
             
                comboBox.ItemsSource = Process.GetProcessesByName("Wow");;
            }
        }
    }
}