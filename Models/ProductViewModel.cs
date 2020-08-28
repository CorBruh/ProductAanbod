using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProductAanbod.Models
{
    public class ProductViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Het veld 'Product naam' is vereist.")]
        [Display(Name = "Product naam")]
        public string ProductNaam { get; set; }

        [Required(ErrorMessage = "Het veld 'Premie' is vereist.")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "€ {0:n}")]
        public decimal Premie { get; set; }

        public bool Actief { get; set; }

        [Display(Name = "Categorie")]
        public int CategorieId { get; set; }
        public ICollection<Categorie> Categorie { get; set; }

        public virtual LaatstAangepast LaatstAangepast { get; set; }

        [Display(Name = "Verzekeraar")]
        public int VerzekeraarId { get; set; }
        public ICollection<Verzekeraar> Verzekeraar { get; set; }
    }
}