using FoodNet.Model.Domain;
using Microsoft.Practices.Prism.PubSubEvents;

namespace FoodNet.UI.Event
{
    public class NewRecipeAddedEvent: PubSubEvent<NewRecipeAddedEventArgs>
    {
    }

    public class NewRecipeAddedEventArgs
    {
        public Recipe NewRecipe { get; set; }
    }
}
