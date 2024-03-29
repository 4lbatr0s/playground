

using Entities.Models;

namespace Entities.LinkModels;

public class LinkResponse 

{   //TIP:
    // With this class, we are going to know whether our response has links. If it
    // does, we are going to use the LinkedEntities property. Otherwise, we
    // are going to use the ShapedEntities property.
    public bool HasLinks { get;set;} 
    public List<Entity> ShapedEntities { get;set;}
    public LinkCollectionWrapper<Entity> LinkedEntities { get;set;}
    public LinkResponse()
    {
        LinkedEntities = new LinkCollectionWrapper<Entity>();
        ShapedEntities = new List<Entity>();
    }
}