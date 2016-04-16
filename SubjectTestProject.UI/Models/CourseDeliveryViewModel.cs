using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectTestProject.UI.Models
{
    public class CourseDeliveryViewModel
    {
        public string Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Domain.Model.CourseDelivery.DeliveryMode DeliveryType { get; set; }
    }
}
