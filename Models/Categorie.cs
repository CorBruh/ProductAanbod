using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductAanbod.Models
{
    public class Categorie
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Het veld 'Categorie naam' is vereist.")]
        [Display(Name = "Categorie naam")]
        public string CategorieNaam { get; set; }
    }
}