using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace SynMetal_MVC.Models
{
 
    public class PhotoFile
    {
        [Key]
        public virtual int PhotoId { get; set; }
        public virtual string FilePath { get; set; }
        [Display(ResourceType = typeof(ResourceFiles.Resources), Name = "FileName")]
        [RegularExpression(Variables.RegexForNames,
            ErrorMessageResourceType = typeof(ERRMSG.ERRMSG), ErrorMessageResourceName = "StringRegex")]
        public virtual string Name { get; set; }
        [Display(ResourceType = typeof(ResourceFiles.Resources), Name = "Description")]
        [DataType(DataType.MultilineText)]
        [RegularExpression(Variables.RegexForNames, 
            ErrorMessageResourceType = typeof(ERRMSG.ERRMSG), ErrorMessageResourceName = "StringRegex")]
        public virtual string Description { get; set; }

    }
  
    public class PdfFile
    {
        [Key]
        public virtual int PdfId { get; set; }
        public virtual string FilePath { get; set; } // File Path on the server
        [Display(ResourceType = typeof(ResourceFiles.Resources), Name = "FileName")]
        [RegularExpression(Variables.RegexForNames, 
            ErrorMessageResourceType = typeof(ERRMSG.ERRMSG), ErrorMessageResourceName = "StringRegex")]
        public virtual string FileName { get; set; } // File Name
        [Display(ResourceType = typeof(ResourceFiles.Resources), Name = "Description")]
        [DataType(DataType.MultilineText)]
        [RegularExpression(Variables.RegexForNames,
            ErrorMessageResourceType = typeof(ERRMSG.ERRMSG), ErrorMessageResourceName = "StringRegex")]
        public virtual string Description { get; set; } // The Description of the file
        [Required]
        [Display(ResourceType = typeof(ResourceFiles.Resources), Name = "PDFCat")]
        public virtual PdfCategory Category { get; set; } // Category of pdf
    }

    
    public class PdfCategory
    {
        [Key]
        public virtual int CategoryId { get; set; }
        [Required]
        [Display(ResourceType = typeof(ResourceFiles.Resources), Name = "Category")]
        [StringLength(30, MinimumLength = 2,
            ErrorMessageResourceType = typeof(ERRMSG.ERRMSG), ErrorMessageResourceName = "StringMinMax")]
        [RegularExpression(Variables.RegexForNames, 
            ErrorMessageResourceType = typeof(ERRMSG.ERRMSG), ErrorMessageResourceName = "StringRegex")]

        public virtual string Name { get; set; }
    }
}