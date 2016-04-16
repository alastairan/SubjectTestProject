using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectTestProject.UI.Models
{
    public class AssessmentViewModel
    {
        public AssessmentViewModel()
        {
            this.Units = new List<SubjectDeliveryUnitViewModel>();
        }
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string Code { get; set; }
        public Domain.Model.Assessment.type Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<SubjectDeliveryUnitViewModel> Units { get; set; }
    }
}
