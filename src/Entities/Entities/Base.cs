using Entities.Notifications;

namespace Entities.Entities;

public class Base : Notify
{
    public int Id { get; set; }
    public string Name { get; set; }
}
