using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Entities;

namespace WebApi.Entities
{

    public class Author:IEntity {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public string? Name { get; set; }
        public string? Surname {get; set;}
        public DateTime BirthDate {get; set;}
    }
}