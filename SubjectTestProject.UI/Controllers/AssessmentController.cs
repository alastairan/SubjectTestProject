using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SubjectTestProject.Domain;
using SubjectTestProject.Domain.Model;
using SubjectTestProject.UI.Models;

namespace SubjectTestProject.UI.Controllers
{
    public class AssessmentController : ApiController
    {
        private DomainDbContext db = new DomainDbContext();

        // GET: api/Assessments
        public IQueryable<Assessment> GetAssessments()
        {
            return db.Assessments;
        }

        // GET: api/Assessments/5
        [HttpGet]
        public IHttpActionResult GetAssessment(int id)
        {
            Assessment assessment = db.Assessments.Find(id);
            if (assessment == null)
            {
                return NotFound();
            }

            return Ok(assessment);
        }

        // PUT: api/Assessments/5
        [HttpPut]
        public IHttpActionResult PutAssessment(int id, Assessment assessment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assessment.Id)
            {
                return BadRequest();
            }

            db.Entry(assessment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssessmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Assessments
        [HttpPost]
        public IHttpActionResult PostAssessment(AssessmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Assessment assessment = new Assessment(model.Code, model.Type, model.StartDate, model.EndDate);
            foreach (var u in model.Units)
            {
                var unit = db.Units.Find(u.Id);
                assessment.AddUnit(unit);
            }
            var subject = db.Subjects.Include("Assessments").FirstOrDefault(s => s.Id == model.SubjectId);
            subject.AddAssessment(assessment);
            db.Entry(subject).State = EntityState.Modified;
            db.SaveChanges();
            model.Id = assessment.Id;
            CreatedAtRoute("DefaultApi", new { id = assessment.Id }, assessment);
            return Ok(model);
        }

        // DELETE: api/Assessments/5
        [HttpDelete]
        public IHttpActionResult DeleteAssessment(int id)
        {
            Assessment assessment = db.Assessments.Find(id);
            if (assessment == null)
            {
                return NotFound();
            }
            db.Assessments.Remove(assessment);
            db.SaveChanges();

            return Ok(assessment);
        }
        [HttpDelete]
        public IHttpActionResult DeleteSubjectAssessment(int id, int SubjectId)
        {
            Assessment assessment = db.Assessments.Find(id);
            if (assessment == null)
            {
                return NotFound();
            }
            var subject = db.Subjects.Include("Assessments").FirstOrDefault(s => s.Id == SubjectId);
            subject.RemoveAssessment(assessment);
            db.Entry(subject).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(assessment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssessmentExists(int id)
        {
            return db.Assessments.Count(e => e.Id == id) > 0;
        }
    }
}