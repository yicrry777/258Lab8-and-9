using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IntroMVC.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        [Display(Name="Category Name")]
        [Required(ErrorMessage ="Category name cannot be blank")]
        [StringLength(50, MinimumLength = 3, ErrorMessage ="Category name must be between 3 and 50 characters")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Please enter a category name beginning with a capital letter and enter only letters and spaces")]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}