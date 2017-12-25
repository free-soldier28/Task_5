using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService.BLL.Interfaces
{
    public interface IParser
    {
        bool ParseFile(string filePath);
    }
}
