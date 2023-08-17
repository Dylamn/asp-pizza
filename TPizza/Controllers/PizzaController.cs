﻿using Microsoft.AspNetCore.Mvc;
using BO;

namespace TPizza.Controllers;

public class PizzaController : Controller
{
    public static List<Ingredient> IngredientsDisponibles => new List<Ingredient>
    {
        new Ingredient { Id = 1, Nom = "Mozzarella" },
        new Ingredient { Id = 2, Nom = "Jambon" },
        new Ingredient { Id = 3, Nom = "Tomate" },
        new Ingredient { Id = 4, Nom = "Oignon" },
        new Ingredient { Id = 5, Nom = "Cheddar" },
        new Ingredient { Id = 6, Nom = "Saumon" },
        new Ingredient { Id = 7, Nom = "Champignon" },
        new Ingredient { Id = 8, Nom = "Poulet" }
    };

    public static List<Pate> PatesDisponibles => new List<Pate>
    {
        new Pate { Id = 1, Nom = "Pate fine, base crême" },
        new Pate { Id = 2, Nom = "Pate fine, base tomate" },
        new Pate { Id = 3, Nom = "Pate épaisse, base crême" },
        new Pate { Id = 4, Nom = "Pate épaisse, base tomate" }
    };

    // GET
    public IActionResult Index()
    {
        return View();
    }
}