using FoodNet.Model.Domain;
using Microsoft.Practices.Prism.PubSubEvents;

namespace FoodNet.UI.Event
{
    public class RecipeUpdatedEvent: PubSubEvent<RecipeUpdatedEventArgs>
    {
    }

    public class RecipeUpdatedEventArgs
    {
        public Recipe UpdatedRecipe { get; set; }
    }
}
