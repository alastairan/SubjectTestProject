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
    public class SubjectDeliveryController : ApiController
    {
        private DomainDbContext db = new DomainDbContext();

        // GET: api/SubjectDelivery
        public IQueryable<SubjectDelivery> GetSubjectDeliveries()
        {
            return db.SubjectDeliveries;
        }

        // GET: api/SubjectDelivery/5
        [HttpGet]
        public IHttpActionResult GetSubjectDelivery(int id)
        {
            SubjectDelivery subjectDelivery = db.SubjectDeliveries.Find(id);
            if (subjectDelivery == null)
            {
                return NotFound();
            }

            return Ok(subjectDelivery);
        }

        // PUT: api/SubjectDelivery/5
        [HttpPut]
        public IHttpActionResult PutSubjectDelivery(int id, SubjectDelivery subjectDelivery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subjectDelivery.Id)
            {
                return BadRequest();
            }

            db.Entry(subjectDelivery).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectDeliveryExists(id))
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

        // POST: api/SubjectDelivery
        [HttpPost]
        public IHttpActionResult PostSubjectDelivery(SubjectDeliveryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CourseDelivery course = db.CourseDeliveries.FirstOrDefault(c => c.Id == model.CourseDeliveryId);
            var subject = new Subject(model.Name);
            subject.CourseId = model.CourseId;
            subject.Course = db.Courses.FirstOrDefault(i => i.Id == model.CourseId);
            subject.Name = model.Name;
            foreach (var u in model.Units)
            {
                var ux = db.Units.FirstOrDefault(s => s.Id == u.Id);
                if (ux != null)
                {
                    subject.AddUnit(ux);
                }
            }
            var subjectDelivery = new SubjectDelivery(subject);
            course.addSubjectDelivery(subjectDelivery);
            db.SaveChanges();
            model.Id = subjectDelivery.Id;
            CreatedAtRoute("DefaultApi", new { id = subjectDelivery.Id }, subjectDelivery);
            return Ok(model);
        }

        // DELETE: api/SubjectDelivery/5
        [HttpDelete]
        public IHttpActionResult DeleteSubjectDelivery(int id)
        {
            SubjectDelivery subjectDelivery = db.SubjectDeliveries.Include("Subject").FirstOrDefault(x => x.Id == id);
            if (subjectDelivery == null)
            {
                return NotFound();
            }
            db.SubjectDeliveries.Remove(subjectDelivery);
            db.SaveChanges();

            return Ok(subjectDelivery);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubjectDeliveryExists(int id)
        {
            return db.SubjectDeliveries.Count(e => e.Id == id) > 0;
        }
    }
}