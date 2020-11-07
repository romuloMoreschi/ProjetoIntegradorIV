using LojaVirtual.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Categoria
    {
        [Display(Name = "Código")]

        public int Id { get; set; }

        
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(4, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E002")]
        [Display(Name = "")]
        //TODO - Criar validacao - Nome unico no Banco de dados.
        public string Nome { get; set; }

        /*o que é Slug - é tudo o que vem depois da última “/” no caminho e geralmente identifica o arquivo ou a página da Web
         * exemplo
         * www.lojavirutal.com.br/categoria/5 // URL normal
         * com slug - deixando a url amigavel
         * www.lojavirutal.com.br/categoria/informatica
         */
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(4, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E002")]
        public string Slug { get; set; }

        /*Subniveis de categoria, se ultiliza Auto relacionamento
         * Ex:
         * 1-Informatica - Categoria Pai=null
         * 2 -Mouse - P: 1
         * -- 3 mouse sem fio - P: 2
         * -- 4 mouse com fio- P:2
         * -- 5 mouse gamer - P:2
         */
        [Display(Name = "Categoria Pai")]
        public int? CategoriaPaiId { get; set; }
        
        
        
        /*ORM - Entity FrameWorkCore
         * Para fazer o relaciomento das categorias ultiliza-se o metodo a baixo
         *podendo assim fazer a assoriacao
         */
        [ForeignKey("CategoriaPaiId")] // Vintuclo das tabelas fazendo o auto relaciomento
        public virtual Categoria CategoriaPai { get; set; }

    }
}
