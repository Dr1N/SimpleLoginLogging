using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("tblEvents")]
    public class Event
    {
        [Key]
        [Column("evID")]
        public int Id { get; set; }

        [Column("ev_drvID")]
        [ForeignKey("Driver")]
        public int DriverId { get; set; }

        [Column("evTime")]
        [Required]
        public DateTime Time { get; set; }
    }
}
