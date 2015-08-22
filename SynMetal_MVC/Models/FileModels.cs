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
        [RegularExpression(Variables.RegexForNames, ErrorMessage = "Description Must be a string in range of A-Z , a-z , 0-9 , '-', '_'")]
        public virtual string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [RegularExpression(Variables.RegexForNames, ErrorMessage = "Description Must be a string in range of A-Z , a-z , 0-9 , '-', '_'")]
        public virtual string Description { get; set; }

    }
  
    public class PdfFile
    {
        [Key]
        public virtual int PdfId { get; set; }
        public virtual string FilePath { get; set; } // File Path on the server
        [RegularExpression(Variables.RegexForNames, ErrorMessage = "Description Must be a string in range of A-Z , a-z , 0-9 , '-', '_'")]
        public virtual string FileName { get; set; } // File Name
        [DataType(DataType.MultilineText)]
        [RegularExpression(Variables.RegexForNames, ErrorMessage = "Description Must be a string in range of A-Z , a-z , 0-9 , '-', '_'")]
        public virtual string Description { get; set; } // The Description of the file
        [Required]
        public virtual PdfCategory Category { get; set; } // Category of pdf
    }

    
    public class PdfCategory
    {
        [Key]
        public virtual int CategoryId { get; set; }
        [Required]
        [Display(Name = "PDF Category Name")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "The Name must be from 2 to 30 length")]
        [RegularExpression(Variables.RegexForNames, ErrorMessage = "Name Must be a string in range of A-Z , a-z , 0-9 , '-', '_'")]

        public virtual string Name { get; set; }
    }
}