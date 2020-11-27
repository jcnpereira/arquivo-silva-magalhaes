using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.Translations;
using ArquivoSilvaMagalhaes.Web.I18n;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [RegularExpression("[0-9A-Za-z]+", ErrorMessageResourceType = typeof(UserViewModelStrings), ErrorMessageResourceName = "UserName_LettersAndDigitsOnly")]
        [Display(ResourceType = typeof(UserViewModelStrings), Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(UserViewModelStrings), Name = "Password")]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceType = typeof(AuthStrings), ErrorMessageResourceName = "PasswordsMustMatch")]
        [Display(ResourceType = typeof(UserViewModelStrings), Name = "PasswordConfirm")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [Display(ResourceType = typeof(UserViewModelStrings), Name = "RealName")]
        public string RealName { get; set; }

        [Display(ResourceType = typeof(UserViewModelStrings), Name = "EmailAddress")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [Display(ResourceType = typeof(UserViewModelStrings), Name = "Role")]
        public IList<string> Roles { get; set; }

        public IEnumerable<SelectListItem> AvailableRoles { get; set; }

        [Display(ResourceType = typeof(UserViewModelStrings), Name = "Picture")]
        public HttpPostedFileBase Picture { get; set; }
    }

    public class UserChangePasswordModel
    {
        [Display(ResourceType = typeof(UserViewModelStrings), Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(UserViewModelStrings), Name = "Password")]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceType = typeof(AuthStrings), ErrorMessageResourceName = "PasswordsMustMatch")]
        [Display(ResourceType = typeof(UserViewModelStrings), Name = "PasswordConfirm")]
        public string ConfirmPassword { get; set; }
    }

    public class UserChangeRoleModel
    {
        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [RegularExpression("[0-9A-Za-z]+", ErrorMessageResourceType = typeof(UserViewModelStrings), ErrorMessageResourceName = "UserName_LettersAndDigitsOnly")]
        [Display(ResourceType = typeof(UserViewModelStrings), Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [Display(ResourceType = typeof(UserViewModelStrings), Name = "Role")]
        public IList<string> Roles { get; set; }

        public IEnumerable<SelectListItem> AvailableRoles { get; set; }
    }
}