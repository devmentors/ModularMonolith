using System.ComponentModel.DataAnnotations;

namespace ModularMonolith.Modules.Conferences.Core.DTO
{
    public class HostDetailsDto : HostDto
    {
        [Required]
        [StringLength(1000, MinimumLength = 3)]
        public string Description { get; set; }
    }
}