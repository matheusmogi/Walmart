using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Walmart.Model;
using Walmart.Data;

namespace Walmart.Controllers
{
    public class CidadesController : Controller
    {

        public ActionResult Index()
        {
            List<Cidade> cidades = CidadeDAO.ObterCidades();
            return View(cidades);
        }        

        public ActionResult Novo()
        {
            ViewBag.Estados = EstadoDAO.ObterEstados();
            return View();
        } 

        [HttpPost]
        public ActionResult Novo(Cidade cidade)
        {
            try
            {
                CidadeDAO.Novo(cidade);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Estados = EstadoDAO.ObterEstados();
                ViewBag.Erro = "Erro";
                return View(cidade);
            }
        }
        
 
        public ActionResult Editar(int codigo)
        {
            ViewBag.Estados = EstadoDAO.ObterEstados();
            Cidade c = CidadeDAO.ObterCidade(codigo: codigo);
            return View(c);
        }


        [HttpPost]
        public ActionResult Editar(Cidade cidade)
        {
            try
            {
                CidadeDAO.Atualizar(cidade); 
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Erro = "Erro";
                ViewBag.Estados = EstadoDAO.ObterEstados();
                return View(cidade);
            }
        }
        [HttpPost]
        public JsonResult Delete(int codigo)
        {
            try
            {
                CidadeDAO.Delete(codigo);
                return Json("sucesso");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Detalhes(int codigo)
        {
            Cidade cidade = CidadeDAO.ObterCidadeXml(codigo: codigo);
            return View(cidade);
        }

      
    }
}
