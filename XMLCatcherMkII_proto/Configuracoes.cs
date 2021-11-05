using System.Xml.Serialization;

namespace XMLCatcherMkII_Installer
{
    public class Configuracoes
    {
        public string FTPUser { get; set; }
        public string FTPPass { get; set; }
        public string CNPJ { get; set; }
        public string Version { get; set; }
        [XmlArrayItem("Pasta")]
        public string[] PastasMonitoradas { get; set; }
    }

}
