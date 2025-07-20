using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTaskSchedulingSystem.BusinessLogic.ViewModel
{
    public class TaskWithLocationDTO
    {

        public int TaskID { get; set; }

        public int LocID { get; set; }
        public string Task { get; set; }
        public string Location { get; set; }


        public int MinEmployees { get; set; }


        public int MaxEmployees { get; set; }
    }
}
