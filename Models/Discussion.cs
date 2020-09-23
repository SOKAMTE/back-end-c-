using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace offreService.Models
{
    public class Discussion
    {
        [Key]
        public int idDiscussion {get; set;}

        [Column(TypeName = "varchar(255)")]
        public string message {get; set;}

        public virtual ICollection<DiscussionClient> DiscussionClients {get; set;}
    }
}