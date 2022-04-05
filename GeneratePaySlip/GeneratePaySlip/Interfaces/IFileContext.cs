using GeneratePaySlip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePaySlip.Interfaces
{
    public interface IFileContext
    {
        IList<Employee> ReadFile();
        //void WriteStream(IList<Employee> employees, StreamWriter writer);
       // void WriteFile(MemoryStream stream);
    }
}
