using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;
using System.Windows.Input;
using System;
using System.IO;

namespace XMLCatcherMkII_Installer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _shiftPressed = false;
        private Configuracoes _configuracoes = new Configuracoes();
        private Encryption _encryption = new Encryption();
        private XMLReader _xMLReader = new XMLReader($@"{AppDomain.CurrentDomain.BaseDirectory}config.xml", new XmlSerializer(typeof(Configuracoes)));
        private Agendador _agendador = new Agendador();
        public MainWindow()
        {
            InitializeComponent();
            if (_agendador.VerificaTarefa())
            {
                _agendador.VerificaTarefaAtiva();
                but_Instalar.IsEnabled = false;
            }
        }

        private void but_Instalar_Click(object sender, RoutedEventArgs e)
        {
            Configuracoes configs = (Configuracoes)_xMLReader.Deserializa();
            foreach (var item in configs.PastasMonitoradas)
            {
                if (!Directory.Exists(item))
                {
                    MessageBox.Show("Uma das pastas não foi encontrada no sistema. Verifique o arquivo de configuração");
                    return;
                }
            }

            _xMLReader.Serializa(configs);
            _agendador.CriaTarefa();
        }

        private void but_Configurar_Click(object sender, RoutedEventArgs e)
        {
            if (_shiftPressed)
            {
                Configuracoes configs = (Configuracoes)_xMLReader.Deserializa();
                var pergunta = new AskUserQuestion("Digite a senha do FTP");
                pergunta.ShowDialog();
                if (pergunta.DialogResult == true)
                {
                    configs.FTPPass = _encryption.EncodeText(pergunta.userResponse);
                }
                _xMLReader.Serializa(configs);
            }
            else
            {
                System.Diagnostics.Process.Start("notepad.exe", $@"{AppDomain.CurrentDomain.BaseDirectory}config.xml");
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                _shiftPressed = true;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                _shiftPressed = false;
            }
        }
    }
}
