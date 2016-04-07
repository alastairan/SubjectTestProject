using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectTestProject.Domain.Model
{
    public class Course
    {
        public Course()
        {

        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ParentCode { get; set; }
        public string ParentTitle { get; set; }
    }
}
