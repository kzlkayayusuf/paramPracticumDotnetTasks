namespace Entities.RequestFeatures;

public abstract class RequestParameters
{
    const int maxPageSize = 50;

    // auto-implemented property
    public int PageNumber { get; set; }

    // full-property
    private int pageSize;
    public int PageSize
    {
        get { return pageSize; }
        set { pageSize = value > maxPageSize ? maxPageSize : value; }
    }

    public String? OrderBy { get; set; }
    public String? Fields { get; set; }
}