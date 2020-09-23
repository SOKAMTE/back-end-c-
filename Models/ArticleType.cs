using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace offreService.Models
{
    public class ArticleType
    {
        [Key]
        public int idArticleType {get; set;}

        [Column(TypeName = "varchar(255)")]
        [Required]
        [StringLength(30, ErrorMessage = "nomType cannot be longer than 50 characters.", MinimumLength=5)]
        public string nomType {get; set;}

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dateDebut {get; set;}

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dateFin {get; set;}

        public virtual ICollection<Article> Articles {get; set;}
    }
}