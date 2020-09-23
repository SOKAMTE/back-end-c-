using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace offreService.Models
{
    public class Compte
    {
        [Key]
        public int idCompte {get; set;}

        [Column(TypeName = "varchar(255)")]
        [Required]
        [StringLength(50, ErrorMessage = "username cannot be longer than 50 characters.", MinimumLength=5)]
        public string username {get; set;}

        [Column(TypeName = "varchar(255)")]
        [Required]
        [StringLength(50, ErrorMessage = "password cannot be longer than 50 characters.", MinimumLength=5)]
        public string password {get; set;}

        [Column(TypeName = "varchar(255)")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dateCreation {get; set;}

        [Column(TypeName = "varchar(255)")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime dateConnexion {get; set;}

        public int ClientID {get; set;}

        public virtual Client Client {get; set;}
    }
}