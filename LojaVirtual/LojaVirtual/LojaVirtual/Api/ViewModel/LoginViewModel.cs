using System.ComponentModel.DataAnnotations;

namespace ApiInterpares.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "Login")]
        [Required(ErrorMessage = "Login é Obrigatório")]
        public string Login { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Senha é Obrigatório")]
        public string Password { get; set; }

        [Display(Name = "Lembrar")]
        public bool RememberMe { get; set; }
    }
}
