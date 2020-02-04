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
        [MaxLength(20)]
        public string Number { get; set; }

        [Required]
        [Column("drvNameFirst")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        
        [Required]
        [Column("drvNameLast")]
        [MaxLength(50)]
        public string LastName { get; set; }
    }
}