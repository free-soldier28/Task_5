using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WindowsService.BLL;


namespace WindowsService
{
    public class Logger
    {
        private string _dirSales = ConfigurationManager.AppSettings["SalesPath"];
        private string _dirSalesTreated = ConfigurationManager.AppSettings["SalesTreatedPath"];
        private string _dirLog = ConfigurationManager.AppSettings["LogPath"];

        FileSystemWatcher watcher;
        object obj = new object();
        bool enabled = true;


        public Logger()
        {
            watcher = new FileSystemWatcher(_dirSales);
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            watcher.Filter = "*.csv";

            watcher.Created += Watcher_Created;
            //watcher.Changed += Watcher_Changed;
            //watcher.Deleted += Watcher_Deleted;
            //watcher.Renamed += Watcher_Renamed;
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;

            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
            enabled = false;
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            string fileEvent = "был переименован в " + e.FullPath;
            string filePath = e.OldFullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "был изменен";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            bool flag = false;
            string fileEvent = "был создан";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);

            ParserCSV parser = new ParserCSV();
            Task.Factory.StartNew(() =>
            {
                flag = parser.ParseFile(filePath);

                if (flag)
                {
                    fileEvent = "успешно распаршен";
                    RecordEntry(fileEvent, filePath);

                    if (File.Exists(_dirSalesTreated + "\\" + e.Name))
                    {
                        File.Delete(_dirSalesTreated);
                    }
                    File.Move(_dirSales + "\\" + e.Name, _dirSalesTreated + "\\" + e.Name);
                    fileEvent = "был перенесен в " + _dirSalesTreated + "\\" + e.Name;
                    RecordEntry(fileEvent, filePath);
                }
                else
                {
                    fileEvent = "не удалось распарсить";
                    RecordEntry(fileEvent, filePath);
                }
            });
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "был удален";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            lock (obj)
            {
                using (StreamWriter writer = new StreamWriter(_dirLog, true))
                {
                    writer.WriteLine(String.Format("{0} файл {1} {2}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), filePath, fileEvent));
                    writer.Flush();
                }
                Console.WriteLine(String.Format("{0} файл {1} {2}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), filePath, fileEvent));
            }
        }
    }
}
