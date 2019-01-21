using FoodNet.Model.Domain;
using Microsoft.Practices.Prism.PubSubEvents;

namespace FoodNet.UI.Event
{
    public class NewProductAddedEvent: PubSubEvent<NewProductAddedEventArgs>
    {
    }

    public class NewProductAddedEventArgs
    {
        public Product NewProduct { get; set; }
    }

}
