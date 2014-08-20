using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.Translations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public enum UserRole
    {
        [Display(ResourceType = typeof(UserViewModelStrings), Name = "Role_Admin")]
        Admin,
        [Display(ResourceType = typeof(UserViewModelStrings), Name = "Role_ContentManager")]
        ContentManager,
        [Display(ResourceType = typeof(UserViewModelStrings), Name = "Role_ArchiveManager")]
        ArchiveManager,
        [Display(ResourceType = typeof(UserViewModelStrings), Name = "Role_SiteManager")]
        SiteManager
    }

    public class UserRegistrationModel
    {
        [Required]
        [RegularExpression("[0-9A-Za-z]+", ErrorMessageResourceType = typeof(UserViewModelStrings), ErrorMessageResourceName = "UserName_LettersAndDigitsOnly")]
        [Display(ResourceType = typeof(UserViewModelStrings), Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(UserViewModelStrings), Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(ResourceType = typeof(UserViewModelStrings), Name = "RealName")]
        public string RealName { get; set; }

        [Display(ResourceType = typeof(UserViewModelStrings), Name = "EmailAddress")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [Display(ResourceType = typeof(UserViewModelStrings), Name = "Role")]
        public UserRole? Role { get; set; }

        [Display(ResourceType = typeof(UserViewModelStrings), Name = "Picture")]
        public HttpPostedFileBase Picture { get; set; }
    }
}