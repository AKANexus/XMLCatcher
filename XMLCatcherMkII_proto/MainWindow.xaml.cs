using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;

namespace XMLCatcherMkII_Installer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var a = new Agendador();
            if (a.VerificaTarefa())
            {
                a.VerificaTarefaAtiva();
                but_Instalar.IsEnabled = false;
            }
        }
    }
}
