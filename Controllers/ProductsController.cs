using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductAanbod.Data;
using ProductAanbod.Models;

namespace ProductAanbod.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.Include("Categorie").Include("Verzekeraar").ToListAsync());
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ProductViewModel vmProduct = new ProductViewModel();
            vmProduct.Categorie = _context.Catogorie.ToList(); //get data out of database and populate the dropdowlists for the view
            vmProduct.Verzekeraar = _context.Verzekeraar.ToList();

            return View(vmProduct);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductNaam,PremiePerMaand,CategorieId,VerzekeraarId")] ProductViewModel vmProduct)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product();
                product.ProductNaam = vmProduct.ProductNaam;
                product.PremiePerMaand = vmProduct.PremiePerMaand;
                product.Categorie = _context.Catogorie.Find(vmProduct.CategorieId);
                product.Verzekeraar = _context.Verzekeraar.Find(vmProduct.VerzekeraarId);
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vmProduct);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Get data from database with the inclusion of the the chosen Categorie and Verzekeraar by using include statement.
            var product = await _context.Product.Include("Categorie").Include("Verzekeraar").FirstOrDefaultAsync(i => i.Id == id.Value);
            ProductViewModel vmProduct = new ProductViewModel
            {
                Id = product.Id,
                ProductNaam = product.ProductNaam,
                PremiePerMaand = product.PremiePerMaand,
                Actief = product.Actief,
                CategorieNaam = product.Categorie.CategorieNaam
            };

            // The first item in the dropdownlist will be the one that was selected when the product was created by the user.
            List<Verzekeraar> VerzekeraarList = new List<Verzekeraar>();
            VerzekeraarList = _context.Verzekeraar.ToList();
            var idx = VerzekeraarList.FindIndex(x => x.Id == product.Verzekeraar.Id);
            var item = VerzekeraarList[idx];
            VerzekeraarList.RemoveAt(idx);
            VerzekeraarList.Insert(0, item);
            vmProduct.Verzekeraar = VerzekeraarList;

            // Get current time of this computer and translate it to The Netherlands timezone.
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo timeInfo = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            vmProduct.GewijzigdDatum = TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeInfo);

            // Get current user email
            vmProduct.Email = User.Identity.Name;

            if (vmProduct == null)
            {
                return NotFound();
            }
            return View(vmProduct);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductNaam,PremiePerMaand,VerzekeraarId,GewijzigdDatum,Email")] ProductViewModel vmProduct)
        {
            if (id != vmProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Product product = new Product
                    {
                        Id = vmProduct.Id,
                        ProductNaam = vmProduct.ProductNaam,
                        PremiePerMaand = vmProduct.PremiePerMaand,
                        Verzekeraar = _context.Verzekeraar.Find(vmProduct.VerzekeraarId)
                    };


                    // Get current time of this computer and translate it to The Netherlands timezone.
                    DateTime utcTime = DateTime.UtcNow;
                    TimeZoneInfo timeInfo = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
                    product.GewijzigdDatum = TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeInfo);

                    // Get current user email
                    product.Email = User.Identity.Name;

                    _context.Update(product);
                    _context.Entry(product).Property("Actief").IsModified = false; // Using Property to exclude Actief off getting changed
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(vmProduct.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vmProduct);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> CheckboxChange(string id, string isChecked) //Ajax request when checkbox(Actief) is checkt or uncheckt.
        {
            int Id = Convert.ToInt32(id);
            bool IsChecked = Convert.ToBoolean(isChecked);

            if (ModelState.IsValid)
            {
                Product product = new Product();
                try
                {
                    

                    product.Id = Id;
                    product.Actief = IsChecked;

                    // Get current user email
                    product.Email = User.Identity.Name;

                    // Get current time of this computer and translate it to The Netherlands timezone.
                    DateTime utcTime = DateTime.UtcNow;
                    TimeZoneInfo timeInfo = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
                    product.GewijzigdDatum = TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeInfo);


                    _context.Attach(product);
                    _context.Entry(product).Property("Actief").IsModified = true;
                    _context.Entry(product).Property("Email").IsModified = true;
                    _context.Entry(product).Property("GewijzigdDatum").IsModified = true;//Include the ones that we want to be changed in the database
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { success = true, responseText = "Database changed" });
            } 
            return Json(new { success = false, responseText = "ModelState not valid" });
        }
    }
}
