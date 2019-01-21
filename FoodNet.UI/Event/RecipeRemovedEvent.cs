using FoodNet.Model.Domain;
using Microsoft.Practices.Prism.PubSubEvents;

namespace FoodNet.UI.Event
{
    public class RecipeRemovedEvent: PubSubEvent<RecipeRemovedEventArgs>
    {
    }

    public class RecipeRemovedEventArgs
    {
        public Recipe RemovedRecipe { get; set; }
    }
}
