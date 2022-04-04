using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto_Covid_Final;

namespace Proyecto_Covid_Final.Controllers
{
    public class PacientesController : Controller
    {
        private proyectocovidEntities db = new proyectocovidEntities();

        // GET: Pacientes
        public ActionResult Index()
        {
            var paciente = db.Paciente.Include(p => p.Contacto).Include(p => p.Notificaciones);
            return View(paciente.ToList());
        }

        // GET: Pacientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // GET: Pacientes/Create
        public ActionResult Create()
        {
            ViewBag.contacto_ID = new SelectList(db.Contacto, "contacto_ID", "contacto_nombre");
            ViewBag.paciente_ID = new SelectList(db.Notificaciones, "noti_ID", "noti_asunto");
            return View();
        }

        // POST: Pacientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "paciente_ID,paciente_nombres,paciente_apellidos,paciente_password,paciente_fechaNaci,paciente_sexo,paciente_cedula,paciente_telefono,paciente_celular,paciente_email,paciente_direccion,paciente_latitud,paciente_longitud,paciente_personasvive,paciente_trabajo,paciente_estudia,paciente_enfermedad,paciente_diabetis,paciente_sobrepeso,paciente_seguro,contacto_ID")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                db.Paciente.Add(paciente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.contacto_ID = new SelectList(db.Contacto, "contacto_ID", "contacto_nombre", paciente.contacto_ID);
            ViewBag.paciente_ID = new SelectList(db.Notificaciones, "noti_ID", "noti_asunto", paciente.paciente_ID);
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            ViewBag.contacto_ID = new SelectList(db.Contacto, "contacto_ID", "contacto_nombre", paciente.contacto_ID);
            ViewBag.paciente_ID = new SelectList(db.Notificaciones, "noti_ID", "noti_asunto", paciente.paciente_ID);
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "paciente_ID,paciente_nombres,paciente_apellidos,paciente_password,paciente_fechaNaci,paciente_sexo,paciente_cedula,paciente_telefono,paciente_celular,paciente_email,paciente_direccion,paciente_latitud,paciente_longitud,paciente_personasvive,paciente_trabajo,paciente_estudia,paciente_enfermedad,paciente_diabetis,paciente_sobrepeso,paciente_seguro,contacto_ID")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.contacto_ID = new SelectList(db.Contacto, "contacto_ID", "contacto_nombre", paciente.contacto_ID);
            ViewBag.paciente_ID = new SelectList(db.Notificaciones, "noti_ID", "noti_asunto", paciente.paciente_ID);
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paciente paciente = db.Paciente.Find(id);
            db.Paciente.Remove(paciente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
