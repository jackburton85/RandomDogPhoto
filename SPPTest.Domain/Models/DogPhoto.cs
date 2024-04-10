using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SPPTest.Domain.Models
{
    public class DogPhoto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Breed { get; set; }
        public string PhotoUrl { get; set; }
    }
}
