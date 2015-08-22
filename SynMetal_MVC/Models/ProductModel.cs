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
        [Display(Name ="Product Name")]
        [StringLength(30,MinimumLength = 2,ErrorMessage ="The Name must be from 2 to 30 length")]
        [RegularExpression(Variables.RegexForNames, ErrorMessage ="Name Must be a string in range of A-Z , a-z , 0-9 , '-', '_'")]
        public virtual string Name { get; set; }
        
        [DataType(DataType.MultilineText)]
        [RegularExpression(Variables.RegexForNames, ErrorMessage = "Description Must be a string in range of A-Z , a-z , 0-9 , '-', '_'")]
        public virtual string Description { get; set; }
        public virtual PhotoFile Photo { get; set; }
        [Required]
        public virtual ProductCategory Category { get; set; }
    }

    
    public class ProductCategory
    {
        [Key]
        public virtual int CategoryId { get; set; }
        [Required]
        [Display(Name = "Product Category")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "The Name must be from 2 to 30 length")]
        [RegularExpression(Variables.RegexForNames, ErrorMessage = "Name Must be a string in range of A-Z , a-z , 0-9 , '-', '_'")]
        public virtual string Name { get; set; }
    }

}