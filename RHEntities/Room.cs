using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RHEntities
{
    [Table("Rooms")]
    public class Room
    {
        [Key]
        public int RoomID { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Number of Beds")]
        public int MaxOccupancy { get; set; }

        [Required]
        [Display(Name = "Room Number")]
        public string RoomNumber { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required]
        [Display(Name = "Current Occupancy")]
        public int CurrentOccupancy { get; set; }

        public bool IsActive { get; set; } = true;

        public string Description { get; set; } = "";

        public virtual Location Location { get; set; }
        public int LocationID { get; set; }

    }
}
