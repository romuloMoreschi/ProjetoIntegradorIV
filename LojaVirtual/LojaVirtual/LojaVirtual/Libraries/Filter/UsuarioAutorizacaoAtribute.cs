﻿using Microsoft.AspNetCore.Mvc.Filters;
using System;
using LojaVirtual.Models;
using LojaVirtual.Libraries.Login;
using Microsoft.AspNetCore.Mvc;
using LojaVirtual.Models.Constantes;

namespace LojaVirtual.Libraries.Filter
{

    //Ordens do Papiline
    /* autorizaocao
     * recurso
     * Acao
     * Execao
     * Resultado
     */

    public class UsuarioAutorizacaoAtribute : Attribute, IAuthorizationFilter
    {
        LoginUsuario _loginUsuario;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginUsuario = (LoginUsuario)context.HttpContext.RequestServices.GetService(typeof(LoginUsuario));

            Usuario usuario = _loginUsuario.GetUsuario();

            if (usuario == null)
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);

            }
            else
            {
                context.Result = new ContentResult() { Content = "Acesso negado" };
            }
        }
    }
}
