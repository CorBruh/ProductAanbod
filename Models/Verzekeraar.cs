using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductAanbod.Models
{
    public class Verzekeraar
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Het veld 'Verzekeraar naam' is vereist.")]
        [Display(Name = "Verzekeraar naam")]
        public string VerzekeraarNaam { get; set; }
    }
}