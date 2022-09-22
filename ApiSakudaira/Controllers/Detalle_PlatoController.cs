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
    public class Detalle_PlatoController : ApiController
    {
        private SakudairaEntities db = new SakudairaEntities();

        // GET: api/Detalle_Plato
        public IQueryable<Detalle_Plato> GetDetalle_Plato()
        {
            return db.Detalle_Plato;
        }

        // GET: api/Detalle_Plato/5
        [ResponseType(typeof(Detalle_Plato))]
        public IHttpActionResult GetDetalle_Plato(int id)
        {
            Detalle_Plato detalle_Plato = db.Detalle_Plato.Find(id);
            if (detalle_Plato == null)
            {
                return NotFound();
            }

            return Ok(detalle_Plato);
        }

        // PUT: api/Detalle_Plato/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDetalle_Plato(int id, Detalle_Plato detalle_Plato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detalle_Plato.Num_Plato)
            {
                return BadRequest();
            }

            db.Entry(detalle_Plato).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalle_PlatoExists(id))
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

        // POST: api/Detalle_Plato
        [ResponseType(typeof(Detalle_Plato))]
        public IHttpActionResult PostDetalle_Plato(Detalle_Plato detalle_Plato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Detalle_Plato.Add(detalle_Plato);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Detalle_PlatoExists(detalle_Plato.Num_Plato))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = detalle_Plato.Num_Plato }, detalle_Plato);
        }

        // DELETE: api/Detalle_Plato/5
        [ResponseType(typeof(Detalle_Plato))]
        public IHttpActionResult DeleteDetalle_Plato(int id)
        {
            Detalle_Plato detalle_Plato = db.Detalle_Plato.Find(id);
            if (detalle_Plato == null)
            {
                return NotFound();
            }

            db.Detalle_Plato.Remove(detalle_Plato);
            db.SaveChanges();

            return Ok(detalle_Plato);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Detalle_PlatoExists(int id)
        {
            return db.Detalle_Plato.Count(e => e.Num_Plato == id) > 0;
        }
    }
}