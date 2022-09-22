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
using ApiSakudaira;

namespace ApiSakudaira.Controllers
{
    public class PlatoesController : ApiController
    {
        private SakudairaEntities db = new SakudairaEntities();

        // GET: api/Platoes
        public IQueryable<Plato> GetPlato()
        {
            return db.Plato;
        }

        // GET: api/Platoes/5
        [ResponseType(typeof(Plato))]
        public IHttpActionResult GetPlato(int id)
        {
            Plato plato = db.Plato.Find(id);
            if (plato == null)
            {
                return NotFound();
            }

            return Ok(plato);
        }

        // PUT: api/Platoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlato(int id, Plato plato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != plato.Num_Plato)
            {
                return BadRequest();
            }

            db.Entry(plato).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatoExists(id))
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

        // POST: api/Platoes
        [ResponseType(typeof(Plato))]
        public IHttpActionResult PostPlato(Plato plato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Plato.Add(plato);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = plato.Num_Plato }, plato);
        }

        // DELETE: api/Platoes/5
        [ResponseType(typeof(Plato))]
        public IHttpActionResult DeletePlato(int id)
        {
            Plato plato = db.Plato.Find(id);
            if (plato == null)
            {
                return NotFound();
            }

            db.Plato.Remove(plato);
            db.SaveChanges();

            return Ok(plato);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlatoExists(int id)
        {
            return db.Plato.Count(e => e.Num_Plato == id) > 0;
        }
    }
}