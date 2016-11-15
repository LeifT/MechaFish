using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using TextBox = System.Windows.Controls.TextBox;

namespace MasterAngler.View.Data
{
    /// <summary>
    /// Description for KeyBindingsView.
    /// </summary>
    public partial class KeyBindingsView
    {
        /// <summary>
        /// Initializes a new instance of the KeyBindingsView class.
        /// </summary>
        public KeyBindingsView()
        {
            InitializeComponent();
            
        }

        //public IEnumerable<Key> KeysDown()
        //{
        //    foreach (Key key in Enum.GetValues(typeof(Key))) {
        //        if (key == Key.None) {
        //            continue;
        //        }

        //        if (Keyboard.IsKeyDown(key)) {
        //            yield return key;
        //        }
        //    }
        //}

        private TextBox _lastFocusedTextBox;

        private void UIElement_OnKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.LeftShift || e.Key == Key.System || e.Key == Key.LeftCtrl) {
                return;
            }

            TextBox textBox = sender as TextBox;

            if (textBox != null) {
                textBox.Text = e.Key.ToString();
                Keyboard.ClearFocus();
                Unbind.Focus();
                Unbind.IsEnabled = false;
            }

            MasterAngler.Properties.Settings.Default.Save();
        }

        private void UIElement_OnGotFocus(object sender, RoutedEventArgs e) {
            _lastFocusedTextBox = sender as TextBox;
            Unbind.IsEnabled = true;
            Command.Content = _lastFocusedTextBox.Name;
        }

        private void Unbind_OnClick(object sender, RoutedEventArgs e) {
            _lastFocusedTextBox.Text = Key.None.ToString();
            Unbind.IsEnabled = false;
        }
    }
}