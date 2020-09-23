using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace offreService.Models
{
    public class Client
    {
        [Key]
        public int idClient {get; set;}

        [Column(TypeName = "varchar(255)")]
        [Required]
        [StringLength(50, ErrorMessage = "nom cannot be longer than 50 characters.", MinimumLength=5)]
        public string nom {get; set;}

        [Column(TypeName = "varchar(255)")]
        [Required]
        [StringLength(50, ErrorMessage = "password cannot be longer than 50 characters.", MinimumLength=5)]
        public string password {get; set;}

        [Column(TypeName = "decimal(64)")]
        public int numero {get; set;}

        [Column(TypeName = "varchar(45)")]
        [DataType(DataType.EmailAddress)]
        public string mail {get; set;}

        public virtual ICollection<Article> Articles {get; set;}

        public virtual ICollection<Commentaire> Commentaires {get; set;}

        public virtual ICollection<Compte> Comptes {get; set;}

        public virtual ICollection<AdminClient> AdminClients {get; set;}

        public virtual ICollection<DiscussionClient> DiscussionClients {get; set;}
    }
}