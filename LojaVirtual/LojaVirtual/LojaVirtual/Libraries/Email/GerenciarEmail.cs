using LojaVirtual.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Email
{
    public class GerenciarEmail
    {
        private SmtpClient _smtp;
        private IConfiguration _configuration;
        public static void EnviarContatoPorEmail(Contato contato)
        {
            /*
             * SMTP -> Servidor que vai enviar a mensagem.
             */
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("andr3y.lim1@gmail.com", "");
            smtp.EnableSsl = true;

            string corpoMsg = string.Format("<h2>Contato - MegaLimp</h2>" +
                "<b>Nome: </b> {0} <br />" +
                "<b>E-mail: </b> {1} <br />" +
                "<b>Texto: </b> {2} <br />" +
                "<br /> E-mail enviado automaticamente do site LojaVirtual.",
                contato.Nome,
                contato.Email,
                contato.Texto
            );


            /*
             * MailMessage -> Construir a mensagem
             */
            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress("andr3y.lim1@gmail.com");
            mensagem.To.Add("andr3y.lim1@gmail.com");
            mensagem.Subject = "Contato - MegaLimp - E-mail: " + contato.Email;
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            //Enviar Mensagem via SMTP
            smtp.Send(mensagem);
        }

        public void EnviarSenhaParaColaboradorPorEmail(Usuario usuario)
        {
            string corpoMsg = string.Format("<h2>Colaborador - MegaLimp</h2>" +
                "Sua senha é:" +
                "<h3>{0}</h3>", usuario.Senha);
            /*
             * MailMessage -> Construir a mensagem
             */
            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
            mensagem.To.Add(usuario.Email);
            mensagem.Subject = "Colaborador - MegaLimp - Senha do colaborador - " + usuario.Nome;
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            //Enviar Mensagem via SMTP
            _smtp.Send(mensagem);
        }

    }
}
