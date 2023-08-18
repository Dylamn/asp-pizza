using System.ComponentModel.DataAnnotations;
using BO;

namespace TPizza.ViewModels;

public class CreatePizzaViewModel
{
    [Required]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "Le nom de la pizza doit être entre {2} et {1} caractères")]
    public string Name { get; set; } = "";

    public int SelectedPateId { get; set; }

    public List<Pate> Pates { get; set; } = new();
    public List<Ingredient> Ingredients { get; set; } = new();
    
    public List<int> SelectedIngredientsIds { get; set; } = new();
}