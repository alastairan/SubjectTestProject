using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectTestProject.UI.Models
{
    public class SubjectViewModel
    {
        public SubjectViewModel()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int DelliveryId { get; set; }
    }
}
