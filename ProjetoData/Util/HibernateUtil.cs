using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using ProjetoData.Mapping;
using NHibernate.Tool.hbm2ddl;
using MySql.Data.MySqlClient;

namespace ProjetoData.Util
{
    /// <summary>
    /// Classe para configuração da SessionFactory
    /// </summary>
    public class HibernateUtil
    {
        private static string ConnectionString = "";
        private static ISessionFactory factory; //null
        public static ISessionFactory GetSessionFactory()
        {
            if (factory == null) //singleton...
            {
                IPersistenceConfigurer configDB = PostgreSQLConfiguration.Standard.ConnectionString(ConnectionString);

                factory = Fluently.Configure().Database(configDB).Mappings(m => m.FluentMappings.
                AddFromAssemblyOf<UsuarioMap>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                .Execute(false, true,false)).BuildSessionFactory();
            }
            return factory;
        }
    }
}

