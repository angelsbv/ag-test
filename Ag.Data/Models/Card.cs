using System.ComponentModel.DataAnnotations;
using System;

namespace Ag.BLL.Models;

public partial class Card
{
    public int CardId { get; set; }
    public int ClientId { get; set; }

    [Display(Name = "Type")]
    public byte CardType { get; set; }

    public string Bank { get; set; } = null!;

    [Display(Name = "Number")]
    [MinLength(19, ErrorMessage = "Please provide a valid card number")]
    public string CardNumber { get; set; } = null!;

    [Display(Name = "Expiry Month")]
    [Range(1, 12, ErrorMessage = "Please provide a valid month (1-12)")]
    public byte ExpiryMonth { get; set; }

    [Display(Name = "Expiry Year")]
    public int ExpiryYear { get; set; }

    public virtual Client? Client { get; set; } = null!;
}

public enum CardTypeCode
{
    Credit = 1,
    Debit = 2
}