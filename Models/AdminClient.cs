namespace offreService.Models
{
    public class AdminClient
    {
        public int idClient {get; set;}
        public Client Client {get; set;}
        public int idAdmin {get; set;}
        public Admin Admin {get; set;}
    }
}