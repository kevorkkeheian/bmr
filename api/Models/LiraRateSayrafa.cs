using System.Text.Json.Serialization;

namespace Application.Models;


public class LiraRateSayrafa
{
    [JsonPropertyName("sayrafa")]
    public List<List<Int64>> Sayrafa { get; set; }

}
