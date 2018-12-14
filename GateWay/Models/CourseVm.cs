using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWay.Models
{
    public class CourseVm
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public CategoryVm Category { get; set; }
    }
}
