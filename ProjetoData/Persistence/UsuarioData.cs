﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;
using ProjetoData.Entities;
using ProjetoData.Generics;
using ProjetoData.Util;

namespace ProjetoData.Persistence
{
    /// <summary>
    /// Classe de persistencia para a entidade Usuario
    /// </summary>
    public class UsuarioData : GenericData<Usuario>
    {
        public bool HasLogin(string Login)
        {
            using (ISession s = HibernateUtil.GetSessionFactory().OpenSession())
            {
                //SQL -> select count(*) from Usuario where Login = ?
                var query = from u in s.Query<Usuario>()
                            where u.Login.Equals(Login)
                            select u;
                //retornar a quantidade obtida...
                return query.Count() > 0;
            }
        }

        public Usuario Authenticate(string Login, string Senha)
        {
            using (ISession s = HibernateUtil.GetSessionFactory().OpenSession())
            {
                //SQL -> select * from Usuario where Login=? and Senha=?
                var query = from u in s.Query<Usuario>()
                            where u.Login.Equals(Login)
                            && u.Senha.Equals(Senha)
                            select u;
                //retornar o primeiro registro encontrado
                return query.FirstOrDefault();
            }
        }

        public void Insert(Usuario u)
        {
            using (ISession s = HibernateUtil.GetSessionFactory().OpenSession())
            {
                ITransaction t = s.BeginTransaction();
                s.Save(u);
                t.Commit();
            }
        }
    }
}
