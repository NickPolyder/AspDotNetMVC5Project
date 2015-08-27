using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace SynMetal_MVC.Models
{
  public class NewsModel
    {
        [Key]
        public int NewsId { get; set; }
        [Required]
        [Display(ResourceType = typeof(ResourceFiles.Resources), Name = "NewsName")]
        [StringLength(30, MinimumLength = 2,
            ErrorMessageResourceType = typeof(ERRMSG.ERRMSG), ErrorMessageResourceName = "StringMinMax")]
        [RegularExpression(Variables.RegexForNames,
            ErrorMessageResourceType = typeof(ERRMSG.ERRMSG), ErrorMessageResourceName = "StringRegex")]
        public string Name { get; set; }
        [Required]
        [Display(ResourceType = typeof(ResourceFiles.Resources), Name = "Description")]
        [DataType(DataType.MultilineText)]
        [RegularExpression(Variables.RegexForNames,
            ErrorMessageResourceType = typeof(ERRMSG.ERRMSG), ErrorMessageResourceName = "StringRegex")]
        public string Description { get; set; }
    }
}
