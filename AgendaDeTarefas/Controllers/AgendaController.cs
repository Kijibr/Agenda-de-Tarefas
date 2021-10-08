using AgendaDeTarefas.Models;
using ProjetoData.Entities;
using ProjetoData.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgendaDeTarefas.Controllers
{
    [Authorize]
    public class AgendaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastro()
        {
            return View(new AgendaModelCadastro());
        }

        public ActionResult Consulta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarTarefa(AgendaModelCadastro model)
        {
            if(ModelState.IsValid) //regras de validação estao certas
            {
                try
                {
                    Tarefa t = new Tarefa() //entidade
                    {
                        Titulo = model.Titulo,
                        Descricao = model.Descricao,
                        DataHoraInicio = model.DataHoraInicio,
                        DataHoraFim = model.DataHoraFim,
                        Categoria = new CategoriaData().Find(model.IdCategoria),
                        Usuario = (Usuario) Session["usuariologado"]
                    };

                    TarefaData d = new TarefaData(); //persistencia em instancia
                    d.Insert(t);
                    ViewBag.Mensagem = "Tarefa " + t.Titulo
                                     + ", cadastrado com sucesso.";
                    ModelState.Clear(); //limpar o conteudo da model...

                }catch(Exception e)
                {
                    ViewBag.Mensagem = e.Message;
                }
            }
            return View("Cadastro", new AgendaModelCadastro()); //nome da view

        }

        [HttpPost]
        public ActionResult ConsultarTarefas(AgendaModelConsulta model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    TarefaData d = new TarefaData(); //persistencia
                    Usuario u = (Usuario)Session["usuariologado"];
                    model.ListagemTarefas = d.FindAll
                    (model.DataIni, model.DataFim, u.IdUsuario);
                }
                catch (Exception e)
                {
                    ViewBag.Mensagem = e.Message;
                }
            }
            return View("Consulta", model);
        }

        [HttpGet]
        public ActionResult ExcluirTarefa(int id)
        {
            try
            {
                TarefaData d = new TarefaData();
                Tarefa t = d.Find(id); //procura a tarefa por id
                d.Delete(t);

                ViewBag.Mensagem = "Tarefa excluída com sucesso.";
            }
            catch (Exception e)
            {
                ViewBag.Mensagem = e.Message;
            }

            return View("Consulta");
        }
    }
}