using CRUDelicious.Models;
using Microsoft.AspNetCore.Mvc;

public class DishController : Controller
{
    // allows to connect with db 
    private MyContext db;

    public DishController(MyContext context)
    {
        db = context;
    }

    [HttpGet("/dishes/new")]
    public IActionResult New()
    {
        return View("New");
    }
    [HttpPost("/dishes/create")]
    public IActionResult Create(Dish newDish)
    {
        if (ModelState.IsValid == false)
        {
            return New();
        }
        db.Dishes.Add(newDish);
        db.SaveChanges();
        Console.WriteLine(newDish.DishId);
        return RedirectToAction("allDishes");
    }

    [HttpGet("/dishes/all")]
    public IActionResult allDishes()
    {
        List<Dish> allDishes = db.Dishes.OrderByDescending(info => info.Name).ToList();
        return View("allDishes", allDishes);
    }

    [HttpGet("dishes/{DishId}")]
    public IActionResult ViewOne(int DishId)
    {
        Dish? oneDish = db.Dishes.FirstOrDefault(dish => dish.DishId == DishId);

        if (oneDish == null)
        {
            return RedirectToAction("allDishes");
        }
        return View("ViewOne", oneDish);
    }

    [HttpPost("/dishes/{id}/delete")]
    public IActionResult DeletePost(int id)
    {
        Dish? oneDishToDelete = db.Dishes.FirstOrDefault(dish => dish.DishId == id);

        if (oneDishToDelete != null)
        {
            db.Dishes.Remove(oneDishToDelete);
            db.SaveChanges();
            return RedirectToAction("allDishes");
        }
        return View("allDishes");
    }

    [HttpGet("/dishes/{DishId}/edit")]
    public IActionResult EditDish (int DishId)
    {
        Dish? dishToEdit = db.Dishes.FirstOrDefault(dish => dish.DishId == DishId);
        if(dishToEdit == null)
        {
            return RedirectToAction("allDishes");
        }
        return View("editDish", dishToEdit);
    }

    [HttpPost("/dishes/{DishId}/update")]
    public IActionResult Update(int DishId, Dish dishToEdit)
    {
        if(!ModelState.IsValid)
        {
            return EditDish(DishId);
        }
        Dish? dbUpdatedDish = db.Dishes.FirstOrDefault(dish => dish.DishId == DishId);

        if(dbUpdatedDish == null)
        {
            return RedirectToAction("allDishes");
        }
        dbUpdatedDish.Name = dishToEdit.Name;
        dbUpdatedDish.Chef = dishToEdit.Chef;
        dbUpdatedDish.Calories = dishToEdit.Calories;
        dbUpdatedDish.Tastiness = dishToEdit.Tastiness;
        dbUpdatedDish.Description = dishToEdit.Description;
        dbUpdatedDish.UpdatedAt = DateTime.Now;

        db.Update(dbUpdatedDish);
        db.SaveChanges();
        return RedirectToAction("ViewOne", new {DishId = DishId});
    }
}