using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Genre:IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}
    public string? Name {get; set;}
    public bool isActive { get; set; } = true;
}