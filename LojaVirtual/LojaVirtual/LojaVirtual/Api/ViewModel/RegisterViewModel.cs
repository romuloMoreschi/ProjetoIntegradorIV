using ApiInterpares.ViewModel;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LoginApi.Models
{
    public class RegisterViewModel : LoginViewModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Nome é Obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Função")]
        [Required(ErrorMessage = "Função é Obrigatório")]
        public string RoleName { get; set; }

        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Por favor digite seu email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}