using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping; //mapeamento
using ProjetoData.Entities; //classes de entidade

namespace ProjetoData.Mapping
{
    /// <summary>
    /// Classe de mapeamento da entidade Usuario
    /// </summary>
    public class UsuarioMap : ClassMap<Usuario>
    {
        //construtor
        public UsuarioMap()
        {
            //nome da tabela
            Table("Usuario");

            //chave primaria..
            Id(u => u.IdUsuario, "IdUsuario").GeneratedBy.Identity();
            //outros atributos...
            Map(u => u.Nome, "Nome").Length(50).Not.Nullable();
            Map(u => u.Login, "Login").Length(20).Not.Nullable().Unique();
            Map(u => u.Senha, "Senha").Length(40).Not.Nullable();
            Map(u => u.DataCadastro, "DataCadastro").Not.Nullable();

            //Relacionamentos

            HasMany(u => u.Tarefas).KeyColumn("IdUsuario").Inverse();
        }
    }
}
