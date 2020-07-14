using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace XMLCatcherMkII_Installer
{
    /// <summary>
    /// Interaction logic for AskUserQuestion.xaml
    /// </summary>
    public partial class AskUserQuestion : Window
    {
        public string userResponse;
        public AskUserQuestion(string Pergunta)
        {
            InitializeComponent();
            tbl_Titulo.Text = Pergunta;
            txb_UserResponse.Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            { 
                DialogResult = false; 
                Close();
            }
        }

        private void txb_UserResponse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                userResponse = txb_UserResponse.Password ?? "";
                DialogResult = true;
                Close();
            }
        }
    }
}
