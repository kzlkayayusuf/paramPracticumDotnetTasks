namespace Entities.LinkModels;

public class LinkResourceBase
{
    // for serialize
    public LinkResourceBase()
    {

    }

    public List<Link> Links { get; set; } = new List<Link>();

}
