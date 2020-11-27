using System.ComponentModel.DataAnnotations;

namespace ArquivoSilvaMagalhaes.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [Display(Name = "Nome de utilizador")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]    
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [DataType(DataType.Password)]
        [Display(Name = "Palavra-passe")]
        public string Password { get; set; }

        [Display(Name = "Manter sessão iniciada")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        //[Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        //[Display(ResourceType = typeof(UiStrings), Name = "User__RealName")]
        //public string RealName { get; set; }

        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
