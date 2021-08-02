using AgendaDeTarefas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgendaDeTarefas.Controllers
{
    public class UsuarioController : Controller
    {
        //GET: /Usuario/Cadastro
        public ActionResult Cadastro()
        {
            return View(); //page_load
        }

        //GET: /Usuario/Login
        public ActionResult Login()
        {
            return View(); //page_load
        }

        //POST: /Usuario/AutenticarUsuario
        [HttpPost]
        public ActionResult AutenticarUsuario(UsuarioModelLogin model)
        {
            //verificar se não ocorreram erros na validação no model
            if(ModelState.IsValid)
            {
                ViewBag.Mensagem = "Teste";
            }

            return View("Login"); //page_load
        }

        //POST: /Usuario/CadastrarUsuario
        [HttpPost]
        public ActionResult CadastrarUsuario(UsuarioModelCadastro model)
        {
            if(ModelState.IsValid)
            {
                ViewBag.Mensagem = "Teste";
            }

            return View("Cadastro"); //page_load
        }
    }
}