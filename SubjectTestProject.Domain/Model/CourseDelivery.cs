using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectTestProject.Domain.Model
{
    public class CourseDelivery
    {
        public CourseDelivery()
        {
            SubjectDeliveries = new List<SubjectDelivery>();
        }
        public int Id { get; set; }
        public Semestar Semestar { get; set; }
        public List<SubjectDelivery> SubjectDeliveries { get; set; }
        public DeliveryMode DeliveryType { get; set; }
        public  void addSubjectDelivery(SubjectDelivery subjectDelivery)
        {
            SubjectDeliveries.Add(subjectDelivery);
        }
        public enum DeliveryMode
        {
            FullTime,
            PartTime,
            Blended,
            Online
        }
    }
}
