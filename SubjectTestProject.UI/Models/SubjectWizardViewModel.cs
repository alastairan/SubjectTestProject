using SubjectTestProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectTestProject.UI.Models
{
    public class SubjectWizardViewModel
    {
        public SubjectWizardViewModel()
        {
            this.Units = new List<SubjectDeliveryUnitViewModel>();
            this.Assessments = new List<AssessmentViewModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubjectDeliveryUnitViewModel> Units { get; set; }
        public List<AssessmentViewModel> Assessments { get; set; }
    }
}
