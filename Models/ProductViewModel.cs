﻿using System;
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
        [Display(Name = "Premie per maand")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "€ {0:n}")]
        public decimal PremiePerMaand { get; set; }

        public bool Actief { get; set; }

        [Display(Name = "Categorie")]
        public int CategorieId { get; set; }
        [Display(Name = "Categorie")]
        public string CategorieNaam { get; set; }
        public virtual ICollection<Categorie> Categorie { get; set; }

        [Display(Name = "Verzekeraar")]
        public int VerzekeraarId { get; set; }
        public virtual ICollection<Verzekeraar> Verzekeraar { get; set; }

        [Display(Name = "Aangepast door")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Laatst aangepast")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public DateTime GewijzigdDatum { get; set; }
    }
}