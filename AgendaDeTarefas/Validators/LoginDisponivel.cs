using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ProjetoData.Persistence;


namespace AgendaDeTarefas.Validators
{
    public class LoginDisponivel : ValidationAttribute
    {
        public override bool IsValid(Object value)
        {
            //value -> representa o valor do elemento que está sendo validado
            string Login = (string)value;
            //acessar a base de dados
            UsuarioData d = new UsuarioData();
            return !d.HasLogin(Login);
        }
    }
}