using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XMLCatcherMkII_Installer
{

    public class XMLReader
    {
        private string filePath;
        private XmlSerializer xMLSerializer;

        public XMLReader(string filePath, XmlSerializer xMLSerializer)
        {
            this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            this.xMLSerializer = xMLSerializer ?? throw new ArgumentNullException(nameof(xMLSerializer));
        }

        public void Serializa(object XMLObject)
        {
            var settings = new XmlWriterSettings() { Encoding = new UTF8Encoding(true), OmitXmlDeclaration = true, Indent = true };
            var XMLPendFinal = new StringBuilder();

            using (XmlWriter writer = XmlWriter.Create(XMLPendFinal, settings))
            {
                var xns = new XmlSerializerNamespaces();
                xns.Add(string.Empty, string.Empty);
                xMLSerializer.Serialize(writer, XMLObject, xns);
            }
            File.WriteAllText($@"{filePath}", XMLPendFinal.ToString());
        }

        public object Deserializa()
        {
            string xmlReadStream = File.ReadAllText($@"{filePath}");
            using var XmlRetorno = new StringReader(xmlReadStream);
            using var xreader = XmlReader.Create(XmlRetorno);
            return xMLSerializer.Deserialize(xreader);
        }
    }
}
