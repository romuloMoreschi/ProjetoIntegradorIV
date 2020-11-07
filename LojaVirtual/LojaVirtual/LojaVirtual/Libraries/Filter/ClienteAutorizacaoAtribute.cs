using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using LojaVirtual.Models;
using LojaVirtual.Libraries.Login;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Libraries.Filter
{

    //Ordens do Papiline
    /* autorizaocao
     * recurso
     * Acao
     * Execao
     * Resultado
     */

    public class ClienteAutorizacaoAtribute : Attribute, IAuthorizationFilter
    {
        LoginCliente _loginCliente;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginCliente = (LoginCliente)context.HttpContext.RequestServices.GetService(typeof(LoginCliente));

            Cliente cliente = _loginCliente.GetCLiente();

            if (cliente == null)
            {
                context.Result = new ContentResult() { Content = "Acesso negado" };
            }
        }
    }
}
