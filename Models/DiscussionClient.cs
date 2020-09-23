namespace offreService.Models
{
    public class DiscussionClient
    {
        public int idClient {get; set;}
        public Client Client {get; set;}
        public int idDiscussion {get; set;}
        public Discussion Discussion {get; set;}
    }
}