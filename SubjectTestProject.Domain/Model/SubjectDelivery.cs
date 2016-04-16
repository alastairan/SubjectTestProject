using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectTestProject.Domain.Model
{
    public class SubjectDelivery
    {
        public SubjectDelivery()
        {

        }
        public SubjectDelivery(Subject Subject)
        {
            this.Subject = Subject;
        }
        public int Id { get; set; }
        public Subject Subject { get; set; }
    }
}
