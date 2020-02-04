using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("tblDrivers")]
    public class Driver
    {
        [Key]
        [Column("drvID")]
        public int Id { get; set; }

        [Required]
        [Column("drvNumber")]
        public string Number { get; set; }

        [Required]
        [Column("[drvNameFirst]")]
        public string FirstName { get; set; }
        
        [Required]
        [Column("[drvNameLast]")]
        public string LastName { get; set; }
    }
}