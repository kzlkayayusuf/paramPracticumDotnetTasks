using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models;

public class Cartoon
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    public string Name { get; set; }
    public Genre Genre { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Topic { get; set; }
    public List<CartoonCharacter> Characters { get; set; }
}
