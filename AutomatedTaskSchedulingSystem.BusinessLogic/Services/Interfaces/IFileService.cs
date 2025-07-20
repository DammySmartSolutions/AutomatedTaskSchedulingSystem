using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTaskSchedulingSystem.BusinessLogic.Services.Interfaces
{
    public interface IFileService
    {
        bool Exists(string path);
        string ReadAllText(string path);


    }
}
