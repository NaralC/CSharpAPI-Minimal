// DTO = Data Transfer Object

using System.ComponentModel.DataAnnotations;

namespace Dtos
{
    public class CommandCreateDto
    {
        // We do not need this since our database, by default, creates Id's for us
        // [Key]
        // public int Id { get; set; }

        [Required]
        public string? HowTo { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Platform { get; set; }

        [Required]
        public string? CommandLine { get; set; }
    }
}