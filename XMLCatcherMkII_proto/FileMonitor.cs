using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XMLCatcherMkII_Installer
{
    class FileMonitor
    {
        public FileMonitor(Configuracoes configuracoes)
        {
            foreach (var item in configuracoes.PastasMonitoradas)
            {
                FileSystemWatcher watcher = new FileSystemWatcher() { Filter = "*.xml", InternalBufferSize = 64, Path = item };
                watcher.Created += NovoXMLDetectado;
                watcher.EnableRaisingEvents = true;
            }
        }

        private void NovoXMLDetectado(object sender, FileSystemEventArgs e)
        {
            
        }
    }
}
