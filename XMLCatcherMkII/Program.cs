using Renci.SshNet;
using Renci.SshNet.Messages;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;
using XMLCatcherMkII_Installer;
using System.Diagnostics;

namespace XMLCatcherMkII
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuracoes configs = (Configuracoes)_xMLReader.Deserializa();

            string[] licencaReply = _licenca.VerificarSerialOnline(configs.Serial, configs.CNPJ);
            if (!(licencaReply[0] == "100"))
            {
                myLog.WriteEntry("Falha ao autorizar o BackUp XML", EventLogEntryType.Information);
            }

            WebRequest request = WebRequest.Create($@"ftp://ftp.ambisoft.com.br/XML/{configs.CNPJ}/");
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential(configs.FTPUser, _encryption.DecodeText(configs.FTPPass));
            try
            {
                using FtpWebResponse resposta = (FtpWebResponse)request.GetResponse();
            }
            catch (WebException)
            {

            }

            foreach (string diretorio in configs.PastasMonitoradas)
            {
                string zipFilePath = $@"{AppDomain.CurrentDomain.BaseDirectory}temp\{Environment.MachineName}_{Path.GetFileName(diretorio)}.zip";
                if (!Directory.Exists(diretorio))
                {
                    continue;
                }
                else
                {
                    Directory.GetDirectoryRoot(diretorio);
                    Directory.CreateDirectory($@"{AppDomain.CurrentDomain.BaseDirectory}temp");
                    if (File.Exists(zipFilePath)) File.Delete(zipFilePath);
                    ZipFile.CreateFromDirectory(diretorio, zipFilePath);
                }
                WebClient client = new WebClient();
                client.Credentials = request.Credentials;
                client.UploadFile($@"ftp://ftp.ambisoft.com.br/XML/{configs.CNPJ}/{Path.GetFileName(zipFilePath)}", zipFilePath);
            }
        }
        static Encryption _encryption = new Encryption();
        static XMLReader _xMLReader = new XMLReader($@"{AppDomain.CurrentDomain.BaseDirectory}configs.xml", new XmlSerializer(typeof(Configuracoes)));
        static LicencaDeUso _licenca = new LicencaDeUso();
        static EventLog myLog = new EventLog("Application") { Source = "Application" };
    }


}
