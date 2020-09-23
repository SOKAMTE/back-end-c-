using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace offreService.Models
{
    public class Commentaire
    {
        [Key]
        public int idCommentaire {get; set;}

        [Column(TypeName = "varchar(255)")]
        public string message {get; set;}

        public int ClientID {get; set;}

        public virtual Client Client {get; set;}
    }
}