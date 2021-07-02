namespace SmallRetail.Web.Resources
{
    public record LinkedResource(string Href);

    public enum LinkedResourceType
    {
        None,
        Prev,
        Next
    }
}