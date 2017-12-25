using System;
using System.IO;
using System.Text.RegularExpressions;
using WindowsService.BLL.Interfaces;
using WindowsService.DAL.Interfaces;
using WindowsService.DAL.Repositories;
using System.Configuration;

namespace WindowsService.BLL
{
    public class ParserCSV: IParser
    {
       private string _dirLog = ConfigurationManager.AppSettings["LogPath"];
       private string fileName { get; set; }
       private string managerName { get; set; }
       private DateTime date { get; set; }
       private string[] substrings { get; set; }

        public bool ParseFile(string filePath)
        {
            try
            {
                FileInfo fileInf = new FileInfo(filePath);

                if (fileInf.Exists)
                {
                    fileName = fileInf.Name;

                    int n1 = 0;
                    int n2 = 0;

                    for (int i = 1; i < fileName.Length; i++)
                    {
                        if (fileName[i] == '_')
                        {
                            n1 = i;
                        }
                        else if (fileName[i] == '.')
                        {
                            n2 = i;
                        }
                    }

                    //date = DateTime.Parse(fileName.Substring(n1 + 1, ".", n2 - n1 - 1));
                    managerName = fileName.Substring(0, n1);


                    using (StreamReader file = new StreamReader(filePath, System.Text.Encoding.Default))
                    {
                        IUnitOfWork uow = new EFUnitOfWork();
                        ISalesService salesService = new SalesService(uow);

                        string line = null;

                        while ((line = file.ReadLine()) != null)
                        {
                            if (line != "")
                            {
                                substrings = Regex.Split(line, ",");
                                salesService.AddSales(managerName, substrings);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                using (StreamWriter writer = new StreamWriter(_dirLog, true))
                {
                    writer.WriteLine(e);
                    writer.Flush();
                }
                // throw; 
                Console.WriteLine("Возникло исключение! Подробнее можно узнать в логах: {0}", _dirLog);
                return false;
            }
        }
    }
}
