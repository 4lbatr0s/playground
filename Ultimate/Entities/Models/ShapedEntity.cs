

using Entities.Models;


/*
    INFO: We need this class to implement HATEOAS while using DATA SHAPING!
    WHY? because with data shaping we do not send any ids.
    But HATEOAS needs ID to create hyperlinks!

    We shoudl use ShapedEntity instead of Entity in IDataShaper. 
*/
public class ShapedEntity
{
    public ShapedEntity()
    {
        Entity = new Entity();
    }
    public Guid Id { get; set; }
    public Entity Entity { get; set; }
}