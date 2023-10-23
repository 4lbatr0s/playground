namespace Entities.LinkModels;

/*
INFO: What is a LINK?
According to RFC5988, a link is "a typed connection between two
resources that are identified by Internationalised Resource Identifiers
(IRIs)". Simply put, we use links to traverse the internet or rather the
resources on the internet.
Our responses contain an array of links, which consist of a few properties
according to the RFC:
• href - represents a target URI.
• rel - represents a link relation type, which means it describes how the current context is related to the target resource.
• method - we need an HTTP method to know how to distinguish the same target URIs.
*/
public class Link
{
    public string? Href {get; set;}
    public string? Rel {get; set;}
    public string? Method {get; set;}

    //TIP: We need this empty constructor for XML serialization process!
    public Link()
    {
        
    }

    public Link(string? href, string? rel, string? method)
    {
        Href = href;
        Rel = rel;
        Method = method;
    }

}
