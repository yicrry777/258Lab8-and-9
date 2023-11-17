using System.ComponentModel.DataAnnotations;

namespace IntroMVC.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Display(Name="Product Name")]
        [Required(ErrorMessage = "Product name cannot be blank")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Product name must be between 3 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9,\s]*$", ErrorMessage = "Please enter a product name made up of only letters, numbers and spaces")]

        public string Name { get; set; }
        [Required(ErrorMessage = "The Product price cannot be blank")]
        [Range(0.10, 10000, ErrorMessage = "Please enter a product price between 0.10 and 10000")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Product description cannot be blank")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Product description must be between 10 and 200 characters")]
        [RegularExpression(@"^[a-zA-Z0-9,\s]*$", ErrorMessage = "Please enter a product description made up of only letters, numbers and spaces")]

        public string Description { get; set; }
        public int? CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}