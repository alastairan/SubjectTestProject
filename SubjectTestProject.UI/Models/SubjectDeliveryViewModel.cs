using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectTestProject.UI.Models
{
    public class SubjectDeliveryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<SubjectDeliveryUnitViewModel> Units { get; set; }
        public int CourseId { get; set; }
        public int CourseDeliveryId { get; set; }
    }
}
