using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models;

public class CartoonCharacter
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    public string Name { get; set; }
    public int CartoonID { get; set; }
    public Cartoon Cartoon { get; set; }
}