using System.ComponentModel.DataAnnotations;

namespace Ag.BLL.Models;

public partial class Client
{
    public int Code { get; set; }

    [Display(Name = "First Name")]
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name")]
    [MaxLength(100)]
    public string LastName { get; set; } = null!;

    [Display(Name = "Contact Number")]
    [MaxLength(20)]
    public string ContactNumber { get; set; } = null!;

    [Display(Name = "Occupation")]
    [MaxLength(100)]
    public string? Occupation { get; set; }

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}

public enum ClientSideLoad
{
    Card
}
