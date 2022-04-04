using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Covid_Final.Controllers
{
    
    public class HomeController : Controller
    {
        proyectocovidEntities context = new proyectocovidEntities();
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.ListOfDropdown = context.Paciente.ToList();
            return View();
        }

        public JsonResult GetAllLocation()
        {
            var data = context.Paciente.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllLocationById(int id)
        {  
            var dato=context.Paciente.Where(y => y.paciente_ID == id).Select(x => new {x.paciente_latitud, x.paciente_longitud }).SingleOrDefault();
            Double Latitud = Convert.ToDouble(dato.paciente_latitud);
            Double Longitud = Convert.ToDouble(dato.paciente_longitud);
            var data = context.Paciente.Where(y => y.paciente_ID == id).Select(x => new { x.paciente_direccion, Latitud, Longitud }).SingleOrDefault(); 
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}