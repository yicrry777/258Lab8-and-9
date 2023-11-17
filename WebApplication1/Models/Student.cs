using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public int? CampusID { get; set; }
        public virtual Campus Campus { get; set; }
    }
}