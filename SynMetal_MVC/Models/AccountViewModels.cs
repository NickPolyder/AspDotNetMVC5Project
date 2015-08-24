using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SynMetal_MVC.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(ResourceType = typeof(ResourceFiles.Resources),Name ="Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "UserName", ResourceType = typeof(ResourceFiles.Resources))]
        [RegularExpression(@"[A-Za-z0-9]+",
            ErrorMessageResourceType = typeof(ERRMSG.ERRMSG),ErrorMessageResourceName = "UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(ResourceFiles.Resources))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(ResourceFiles.Resources))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "UserName", ResourceType = typeof(ResourceFiles.Resources))]
        [RegularExpression(@"[A-Za-z0-9]+",
            ErrorMessageResourceType = typeof(ERRMSG.ERRMSG), ErrorMessageResourceName = "UserName")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(ERRMSG.ERRMSG),
            ErrorMessageResourceName = "StringLength", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(ResourceFiles.Resources))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConPassword", ResourceType = typeof(ResourceFiles.Resources))]
        [Compare("Password",
            ErrorMessageResourceType = typeof(ERRMSG.ERRMSG), ErrorMessageResourceName = "PassWordCon")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(ERRMSG.ERRMSG),
            ErrorMessageResourceName = "StringLength", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(ResourceFiles.Resources))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConPassword", ResourceType = typeof(ResourceFiles.Resources))]
        [Compare("Password", 
            ErrorMessageResourceType = typeof(ERRMSG.ERRMSG), ErrorMessageResourceName = "PassWordCon")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
