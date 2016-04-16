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
    public class SubjectsController : ApiController
    {
        private DomainDbContext db = new DomainDbContext();

        // GET: api/Subjects
        public IQueryable<SubjectViewModel> GetSubjects()
        {
            var model = db.SubjectDeliveries.Include("Subject").Include("Course").Select(S => new SubjectViewModel()
            {
                Id = S.Subject.Id,
                Name = S.Subject.Name,
                CourseCode = S.Subject.Course.Code,
                CourseName = S.Subject.Course.Name,
                DelliveryId = S.Id
            });
            return model;
        }

        // GET: api/Subjects/5
        [HttpGet]
        public IHttpActionResult GetSubject(int id)
        {
            Subject subject = db.Subjects.Include("Units").Include("Assessments").FirstOrDefault(s => s.Id == id);
            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }
        [HttpGet]
        public IHttpActionResult GetWizard(int code, bool what)
        {
            var subject = db.Subjects.Include("Units").Include("Assessments").Include("Assessments.Units").Select(s => new SubjectWizardViewModel()
            {
                Id = s.Id,
                Name = s.Name,
                Assessments = s.Assessments.Select(a => new AssessmentViewModel() {
                    Id = a.Id,
                    Code = a.Code,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    SubjectId = s.Id,
                    Type = a.Type,
                    Units = a.Units.Select(au => new SubjectDeliveryUnitViewModel()
                    {
                        Id = au.Id,
                        Code = au.Code,
                        Name = au.Name
                    }).ToList()
                }).ToList(),
                Units = s.Units.Select(u => new SubjectDeliveryUnitViewModel() {
                    Id = u.Id,
                    Code = u.Code,
                    Name = u.Name
                }).ToList()
            }).FirstOrDefault(s => s.Id == code);
            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }

        // PUT: api/Subjects/5
        [HttpPut]
        public IHttpActionResult PutSubject(int id, Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subject.Id)
            {
                return BadRequest();
            }

            db.Entry(subject).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
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

        // POST: api/Subjects
        [HttpPost]
        public IHttpActionResult PostSubject(Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Subjects.Add(subject);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = subject.Id }, subject);
        }

        // DELETE: api/Subjects/5
        [HttpDelete]
        public IHttpActionResult DeleteSubject(int id)
        {
            Subject subject = db.Subjects.Include("Subject").FirstOrDefault(s => s.Id == id);
            if (subject == null)
            {
                return NotFound();
            }
            
            db.SaveChanges();

            return Ok(subject);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubjectExists(int id)
        {
            return db.Subjects.Count(e => e.Id == id) > 0;
        }
    }
}