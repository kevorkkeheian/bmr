using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models;


public class BlackMarketRate
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    // timestamp
    public Int64 TimeStamp { get; set; }
    public string FromCurrency { get; set; }
    public string ToCurrency { get; set; }
    public Int64 Buy { get; set; }
    public Int64 Sell { get; set; }
}

