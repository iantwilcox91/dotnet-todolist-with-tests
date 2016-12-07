using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Models.Repositories;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoList.Controllers
{
    public class ItemsController : Controller
    {
        private IItemRepository db;

        public ItemsController(IItemRepository thisRepo = null)
        {
            if (thisRepo == null)
            {
                this.db = new EFItemRepository();
            }
            else
            {
                this.db = thisRepo;
            }
        }
        public IActionResult Index()
        {
            return View(db.Items.ToList());
            //This will be the model for the index view 
        }
        public IActionResult Details(int id)
        {
            var thisItem = db.Items.FirstOrDefault(items => items.ItemId == id);
            return View(thisItem);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {

            db.Save(item);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisItem = db.Items.FirstOrDefault(Items => Items.ItemId == id);
            return View(thisItem);
        }

        [HttpPost]
        public IActionResult Edit(Item item)
        {
            db.Edit(item);
            return RedirectToAction("Index");
        }
    }
}
