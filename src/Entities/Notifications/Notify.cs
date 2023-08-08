using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Notifications;

public class Notify
{
    public Notify()
    {
        Notifications = new List<Notify>();
    }

    [NotMapped]
    public string PropertyName { get; set; }

    [NotMapped]
    public string Message { get; set; }

    [NotMapped]
    public List<Notify> Notifications;

    public bool ValidateStringProperty(string value, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(propertyName))
        {
            Notifications.Add(new Notify
            {
                Message = "Obrigatory field",
                PropertyName = propertyName
            });

            return false;
        }

        return true;
    }

    public bool ValidateIntegerProperty(int value, string propertyName)
    {
        if (value < 1 || string.IsNullOrWhiteSpace(propertyName))
        {
            Notifications.Add(new Notify
            {
                Message = "Obrigatory field",
                PropertyName = propertyName
            });

            return false;
        }

        return true;
    }
}
