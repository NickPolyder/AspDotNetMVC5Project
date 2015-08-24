using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace SynMetal_MVC.Models
{
 
    public class Product
    {
        
        [Key]
        public virtual int ProductId { get; set; }
        [Required]
        [Display(ResourceType =typeof(ResourceFiles.Resources),Name = "ProdName")]
        [StringLength(30,MinimumLength = 2,
            ErrorMessageResourceType = typeof(ERRMSG.ERRMSG), ErrorMessageResourceName = "StringMinMax")]
        [RegularExpression(Variables.RegexForNames, 
            ErrorMessageResourceType = typeof(ERRMSG.ERRMSG), ErrorMessageResourceName = "StringRegex")]
        public virtual string Name { get; set; }
        [Display(ResourceType = typeof(ResourceFiles.Resources), Name = "Description")]
        [DataType(DataType.MultilineText)]
        [RegularExpression(Variables.RegexForNames,
            ErrorMessageResourceType = typeof(ERRMSG.ERRMSG), ErrorMessageResourceName = "StringRegex")]
        public virtual string Description { get; set; }
        [Display(ResourceType = typeof(ResourceFiles.Resources), Name = "Photo")]
        public virtual PhotoFile Photo { get; set; }
        [Required]
        [Display(ResourceType = typeof(ResourceFiles.Resources), Name = "ProdCategory")]
        public virtual ProductCategory Category { get; set; }
    }

    
    public class ProductCategory
    {
        [Key]
        public virtual int CategoryId { get; set; }
        [Required]
        [Display(ResourceType = typeof(ResourceFiles.Resources), Name = "ProdCategory")]
        [StringLength(30, MinimumLength = 2, 
            ErrorMessageResourceType = typeof(ERRMSG.ERRMSG), ErrorMessageResourceName = "StringMinMax")]
        [RegularExpression(Variables.RegexForNames,
            ErrorMessageResourceType = typeof(ERRMSG.ERRMSG), ErrorMessageResourceName = "StringRegex")]
        public virtual string Name { get; set; }
    }

}