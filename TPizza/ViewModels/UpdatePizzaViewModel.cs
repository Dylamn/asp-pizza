using BO;

namespace TPizza.ViewModels;

public class UpdatePizzaViewModel : CreatePizzaViewModel
{
    public int Id { get; set; }

    public UpdatePizzaViewModel()
    {
    }

    public UpdatePizzaViewModel(int id, string name, Pate pate, List<Ingredient> ingredients)
    {
        Id = id;
        Name = name;
        SelectedPateId = pate.Id;
        SelectedIngredientsIds = ingredients.Select(i => i.Id).ToList();
    }
}