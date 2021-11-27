using System.ComponentModel.DataAnnotations;

namespace FlasherWebApi.Models
{
    public class Subscriptor
    {
        [Key]
        public int id { get; set; }
        public string EndPoint { get; set; }
        public string P256dh { get; set; }
        public string Auth { get; set; }
    }
}
