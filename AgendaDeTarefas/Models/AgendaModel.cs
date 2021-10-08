using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjetoData.Persistence;
using ProjetoData.Entities;
using ProjetoData.Dto;

namespace AgendaDeTarefas.Models
{
    public class AgendaModelCadastro
    {
        [Required(ErrorMessage = "Por favor, informe o titulo da tarefa")]
        [Display(Name = "Titulo da Tarefa:")] //label
        public string Titulo { get; set; } //campo

        [Required(ErrorMessage = "Por favor, informe a descrição da tarefa.")]
        [Display(Name = "Descrição: ")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data/hora de inicio de tarefa.")]
        [Display(Name = "Data/Hora de Inicio:")]
        public DateTime DataHoraInicio { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data/hora de fim de tarefa.")]
        [Display(Name = "Data/Hora de Inicio:")]
        public DateTime DataHoraFim { get; set; }

        #region Campo de Seleção de Categorias

        [Required(ErrorMessage = "Por favor, selecione a categoria da tarefa")]
        [Display(Name = "Selecione a Categoria: ")]
        public int IdCategoria { get; set; } //captura o valor selecionado

        public List<SelectListItem> ListagemCategorias
        {
            get
            {
                //retornar uma lista com as categorias do banco de dados..
                List<SelectListItem> lista = new List<SelectListItem>();
                CategoriaData d = new CategoriaData();
                foreach (Categoria c in d.FindAll())
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = c.IdCategoria.ToString(); //valor do campo
                    item.Text = c.Nome; //texto exibido no campo
                    lista.Add(item); 
                }
                return lista;
            }
        }
        #endregion
    }
    public class AgendaModelConsulta
    {
        [Required(ErrorMessage = "Por favor, informe a data de inicio.")]
        [Display(Name = "Data de Início")]
        public DateTime DataIni { get; set; }


        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        [Display(Name = "Data de Término")]
        public DateTime DataFim { get; set; }

        //prop para exibir o resultado da pesquisa
       public List<TarefaDto> ListagemTarefas { get; set; } //saída das tasks
    }
}