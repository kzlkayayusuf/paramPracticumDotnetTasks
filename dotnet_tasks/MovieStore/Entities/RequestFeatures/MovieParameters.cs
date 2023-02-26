namespace Entities.RequestFeatures;

public class MovieParameters : RequestParameters
{
    public uint MinPrice { get; set; }
    public uint MaxPrice { get; set; } = 1000;
    public bool ValidPriceRange => MaxPrice > MinPrice;
    public String? SearchTerm { get; set; }

    public MovieParameters()
    {
        OrderBy = "id";
    }
}
