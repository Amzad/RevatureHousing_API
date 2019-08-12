using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RHEntities
{
    [Table("Locations")]
    public class Location
    {
        [Key]
        public int LocationID { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "Training Center")]
        public string TrainingCenter { get; set; }

        public virtual Provider Provider { get; set; }
        public string ProviderID { get; set; }
   }
}
