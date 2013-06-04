using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Walmart.Model;
using Walmart.Data;

namespace Walmart.Controllers
{
    public class EstadosController : Controller
    {
         
        public ActionResult Index()
        {
            List<Estado> estados = EstadoDAO.ObterEstados();
            return View(estados);
        }
      
        
        public ActionResult Novo()
        {
            return View();
        } 

        
        [HttpPost]
        public ActionResult Novo(Estado estado)
        {
            try
            {
                EstadoDAO.Novo(estado);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
       
        public ActionResult Editar(int codigo)
        {
            Estado estado = EstadoDAO.ObterEstado(codigo);
            return View(estado);
        } 

        [HttpPost]
        public ActionResult Editar(Estado estado)
        {
            try
            {
                EstadoDAO.Atualizar(estado); 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult Delete(int codigo)
        {
            try
            {
                EstadoDAO.Delete(codigo);
                return Json("sucesso");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
