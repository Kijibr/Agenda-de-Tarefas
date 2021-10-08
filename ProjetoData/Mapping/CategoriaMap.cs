using FluentNHibernate.Mapping;
using ProjetoData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoData.Mapping
{
    public class CategoriaMap : ClassMap<Categoria>
    {
        public CategoriaMap()
        {
            Table("Categoria");

            Id(c => c.IdCategoria, "IdCategoria").GeneratedBy.Identity();

            Map(c => c.Nome, "Nome").Length(50).Not.Nullable();

            HasMany(c => c.Tarefas).KeyColumn("IdCategoria").Inverse();
        }
    }
}
