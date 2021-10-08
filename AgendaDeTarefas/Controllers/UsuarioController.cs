using AgendaDeTarefas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjetoData.Entities;
using ProjetoData.Persistence;
using ProjetoUtil;
using System.Web.Security; //autenticação...

namespace AgendaDeTarefas.Controllers
{
    [AllowAnonymous]
    public class UsuarioController : Controller
    {
        // GET: /Usuario/Cadastro
        public ActionResult Cadastro()
        {
            return View(); 
        }

        //GET: /Usuario/Login
        public ActionResult Login()
        {
            return View(); 
        }

        //POST: /Usuario/AutenticarUsuario
        [HttpPost]
        public ActionResult AutenticarUsuario(UsuarioModelLogin model)
        {
            //verificar se não ocorreram erros na validação no model
            if(ModelState.IsValid)
            {
                try
                {
                    UsuarioData d = new UsuarioData(); //persistencia da entidade
                    Usuario u = d.Authenticate(model.Login, Criptografia.GetMD5Hash(model.Senha));

                    if(u != null) //se usuario for encontrado
                    {
                        //Gera um ticket de acesso pro usuario
                        FormsAuthentication.SetAuthCookie(u.Login, false);

                        //Armazena o objeto Usuario numa sessão
                        Session.Add("usuariologado", u);

                        //redireciona pra página Agenda
                        return RedirectToAction("Index", "Agenda");
                    }
                    else
                    {
                        ViewBag.Mensagem = "Acesso Negado.";
                    }
                }
                catch(Exception e)
                {
                    ViewBag.Mensagem = e.Message;
                }
            }

            return View("Login"); //page_load
        }

        //POST: /Usuario/CadastrarUsuario
        [HttpPost]
        public ActionResult CadastrarUsuario(UsuarioModelCadastro model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    Usuario u = new Usuario() //entidade
                    {
                        Nome = model.Nome,
                        Login = model.Login,
                        Senha = Criptografia.GetMD5Hash(model.Senha),
                        DataCadastro = DateTime.Now
                    };

                    UsuarioData d = new UsuarioData(); //persistencia
                    d.Insert(u); //Grava os dados na bd

                    ViewBag.Mensagem = "Usuario " + u.Nome + ", cadastro com sucesso.";
                    ModelState.Clear(); //limpar o conteudo do formulario
                }
                catch(Exception e)
                {
                    ViewBag.Mensagem = e.Message;
                }
            }

            return View("Cadastro"); //page_load
        }
        [Authorize]
        public ActionResult Logout()
        {
            //Passo 1 - Remove o ticket de acesso criado.
            FormsAuthentication.SignOut();

            //Passe 2 - Remove o usuario logado na sessão
            Session.Remove("usuariologado");
            Session.Abandon(); //invalida a sessão

            return View("Login");
        }
    }
}