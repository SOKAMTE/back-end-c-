using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace offreService.Models
{
    public class Article
    {
        [Key]
        public int IdArticle {get; set;}

        [Column(TypeName = "varchar(255)")]
        [Required]
        [StringLength(30, ErrorMessage = "nom cannot be longer than 50 characters.", MinimumLength=5)]
        public string nom {get; set;}

        [Column(TypeName = "decimal(64)")]
        [Required]
        [StringLength(30, ErrorMessage = "prix cannot be longer than 50 characters.", MinimumLength=5)]
        public int prix {get; set;}

        public int ClientID {get; set;}

        public int ArticleTypeID {get; set;}

        public virtual Client Client {get; set;}

        public virtual ArticleType ArticleType {get; set;}
        
    }
}