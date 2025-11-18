using System.ComponentModel.DataAnnotations;

namespace ApiProject.DTOs;

public class UpdateItemDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
}

