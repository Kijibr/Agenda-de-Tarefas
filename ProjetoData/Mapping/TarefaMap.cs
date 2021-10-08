using FluentNHibernate.Mapping;
using ProjetoData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoData.Mapping
{
    public class TarefaMap : ClassMap<Tarefa>
    {
        public TarefaMap()
        {
            Table("Tarefa");

            Id(t => t.IdTarefa).GeneratedBy.Identity();

            Map(t => t.Titulo, "Titulo").Length(50).Not.Nullable();
            Map(t => t.Descricao, "Descricao").Not.Nullable();
            Map(t => t.DataHoraInicio, "DataHoraInicio").Not.Nullable();
            Map(t => t.DataHoraFim, "DataHoraFim").Not.Nullable();

            //chaves estrangeiras
            References(f => f.Categoria).Column("IdCategoria");
            References(f => f.Usuario).Column("IdUsuario");
        }
    }
}
