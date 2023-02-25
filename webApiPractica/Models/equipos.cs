using System.ComponentModel.DataAnnotations;

namespace webApiPractica.Models
{
    public class equipos
    {
        [Key]
        public int id_auto { get; set; } 
        public string nombre { get; set; }
        public string modelo { get; set; }

    }
}
