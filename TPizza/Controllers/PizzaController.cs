using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using BO;
using TPizza.ViewModels;

namespace TPizza.Controllers;

public class PizzaController : Controller
{
    public static List<Ingredient> IngredientsDisponibles => new List<Ingredient>
    {
        new() { Id = 1, Nom = "Mozzarella" },
        new() { Id = 2, Nom = "Jambon" },
        new() { Id = 3, Nom = "Tomate" },
        new() { Id = 4, Nom = "Oignon" },
        new() { Id = 5, Nom = "Cheddar" },
        new() { Id = 6, Nom = "Saumon" },
        new() { Id = 7, Nom = "Champignon" },
        new() { Id = 8, Nom = "Poulet" }
    };

    public static List<Pate> PatesDisponibles => new()
    {
        new() { Id = 1, Nom = "Pate fine, base crême" },
        new() { Id = 2, Nom = "Pate fine, base tomate" },
        new() { Id = 3, Nom = "Pate épaisse, base crême" },
        new() { Id = 4, Nom = "Pate épaisse, base tomate" }
    };


    public static List<Pizza> Pizzas = new()
    {
        new Pizza("Calzone", PatesDisponibles[1])
        {
            Id = 1,
            Ingredients = IngredientsDisponibles
                .Where(i => i.Nom is "Mozzarella" or "Jambon").ToList()
        },
        new Pizza("Reine", PatesDisponibles[0])
        {
            Id = 2,
            Ingredients = IngredientsDisponibles
                .Where(i => i.Nom is "Jambon" or "Champignon" or "Tomate").ToList()
        }
    };

    public IActionResult Index()
    {
        return View(Pizzas);
    }

    [HttpGet("[controller]/[action]")]
    public IActionResult Create()
    {
        CreatePizzaViewModel vm = new()
        {
            Pates = PatesDisponibles,
            Ingredients = IngredientsDisponibles
        };

        return View(vm);
    }

    [HttpPost("[controller]/[action]")]
    [ValidateAntiForgeryToken]
    public IActionResult Create([FromForm] CreatePizzaViewModel pvm)
    {
        var pizza = new Pizza
        {
            Id = Pizzas.Max(p => p.Id) + 1,
            Nom = pvm.Name,
            Pate = PatesDisponibles.Single(p => p.Id == pvm.SelectedPateId),
            Ingredients = IngredientsDisponibles
                .Where(i => pvm.SelectedIngredientsIds.Contains(i.Id))
                .ToList()
        };

        Pizzas.Add(pizza);

        return RedirectToAction("Index");
    }

    public IActionResult Edit([FromRoute] int id)
    {
        Pizza? pizza = Pizzas.SingleOrDefault(p => p.Id == id);

        if (pizza is null)
        {
            return NotFound();
        }

        UpdatePizzaViewModel vm = new(
            pizza.Id, pizza.Nom, pizza.Pate, pizza.Ingredients
        )
        {
            Pates = PatesDisponibles,
            Ingredients = IngredientsDisponibles
        };

        return View(vm);
    }

    [HttpPost("[controller]/[action]/{id:int}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit([FromRoute] int id, [FromForm] UpdatePizzaViewModel pvm)
    {
        var pizzaToUpdate = Pizzas.SingleOrDefault(p => p.Id == pvm.Id);

        if (pvm.Id != id) return Forbid();
        if (pizzaToUpdate is null) return NotFound();

        pizzaToUpdate.Nom = pvm.Name;

        if (pvm.SelectedPateId != pizzaToUpdate.Pate.Id)
        {
            pizzaToUpdate.Pate = PatesDisponibles.Single(p => p.Id == pvm.SelectedPateId);
        }

        pizzaToUpdate.Ingredients = IngredientsDisponibles
            .Where(i => pvm.SelectedIngredientsIds.Contains(i.Id))
            .ToList();

        return RedirectToAction("Index");
    }

    [HttpGet("[controller]/[action]/{id:int}")]
    public IActionResult ConfirmDelete([FromRoute] int id)
    {
        Pizza? pizza = Pizzas.SingleOrDefault(p => p.Id == id);

        return View("Delete", pizza);
    }

    [HttpPost("[controller]/[action]/{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        Pizza pizza = Pizzas.Single(p => p.Id == id);
        Pizzas.Remove(pizza);

        return Redirect("/Pizza");
    }

    [HttpGet("[controller]/{id:int}")]
    public IActionResult Details([FromRoute] int id)
    {
        Pizza? pizza = Pizzas.SingleOrDefault(p => p.Id == id);

        return View(pizza);
    }
}