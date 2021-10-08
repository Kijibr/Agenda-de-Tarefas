using ProjetoData.Dto;
using ProjetoData.Util;
using ProjetoData.Entities;
using ProjetoData.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace ProjetoData.Persistence
{
    public class TarefaData : GenericData<Tarefa>
    {
        public List<TarefaDto> FindAll(DateTime Dataini, DateTime DataFim, int IdUsuario)
        {
            using (ISession s = HibernateUtil.GetSessionFactory().OpenSession())
            {
                var query = from t in s.Query<Tarefa>()
                            where t.DataHoraInicio >= Dataini &&
                                  t.DataHoraFim <= DataFim &&
                                  t.Usuario.IdUsuario == IdUsuario
                            orderby t.DataHoraInicio ascending
                            select t;

                List<TarefaDto> lista = new List<TarefaDto>();
                foreach(var t in query.ToList())
                {
                    lista.Add(
                        new TarefaDto()
                        {
                            Codigo = t.IdTarefa,
                            Titulo = t.Titulo,
                            Descricao = t.Descricao,
                            DataHoraInicio = t.DataHoraInicio,
                            DataHoraFim = t.DataHoraFim,
                           // Categoria = t.Categoria.Nome,
                            Usuario = t.Usuario.Nome
                        }
                    );
                }
                return lista;
            }
        }
    }
}
