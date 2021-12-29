using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect2.Data;
using Proiect2.Models;
//using Proiect2.Views.PhoneViewModels;
using Proiect2.Models.PhoneViewModels;

namespace Proiect2.Controllers
{
    [Authorize(Policy = "OnlyManagers")]
    public class StoresController : Controller
    {
        private readonly PhoneContext _context;

        public StoresController(PhoneContext context)
        {
            _context = context;
        }

        // GET: Stores
        [AllowAnonymous]

        public async Task<IActionResult> Index(int? id, int? phoneID)
        {
            var viewModel = new StoreIndexData();
            viewModel.Stores = await _context.Stores
            .Include(i => i.PhonesStores)
            .ThenInclude(i => i.Phone)
            .ThenInclude(i => i.Orders)
            .ThenInclude(i => i.Customer)
            .AsNoTracking()
            .OrderBy(i => i.StoreName)
            .ToListAsync();
            if (id != null)
            {
                ViewData["StoreID"] = id.Value;
                Store store = viewModel.Stores.Where(
                i => i.ID == id.Value).Single();
                viewModel.Phones = store.PhonesStores.Select(s => s.Phone);
            }
            if (phoneID != null)
            {
                ViewData["PhoooneID"] = phoneID.Value;
                viewModel.Orders = viewModel.Phones.Where(
                x => x.ID == phoneID).Single().Orders;
            }
            return View(viewModel);
        }








        // GET: Stores/Details/5
        [AllowAnonymous]  // sa poata fi vazute magazinele de toti utilizatorii

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StoreName,Adress")] Store store)
        {
            if (ModelState.IsValid)
            {
                _context.Add(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(store);
        }



        //lab4  ---------------------------------------------------------------------------

        // GET: Stores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var store = await _context.Stores
            .Include(i => i.PhonesStores).ThenInclude(i => i.Phone)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (store == null)
            {
                return NotFound();
            }
            PopulatePhonesStoreData(store);
            return View(store);

        }
        private void PopulatePhonesStoreData(Store store)
        {
            var allPhones = _context.Phones;
            var storePhones = new HashSet<int>(store.PhonesStores.Select(c => c.PhoneID));
            var viewModel = new List<PhonesStoreData>();
            foreach (var phone in allPhones)
            {
                viewModel.Add(new PhonesStoreData
                {
                    PhoneID = phone.ID,
                    Title = phone.Title,
                    IsStored = storePhones.Contains(phone.ID)
                });
            }
            ViewData["Phones"] = viewModel;
        }







        //lab4

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedPhones)
        {
            if (id == null)
            {
                return NotFound();
            }
            var storeToUpdate = await _context.Stores
            .Include(i => i.PhonesStores)
            .ThenInclude(i => i.Phone)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Store>(storeToUpdate, "",
            i => i.StoreName, i => i.Adress))
            {
                UpdateStoredPhones(selectedPhones, storeToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateStoredPhones(selectedPhones, storeToUpdate);
            PopulatePhonesStoreData(storeToUpdate);
            return View(storeToUpdate);
        }
        private void UpdateStoredPhones(string[] selectedPhones, Store storeToUpdate)
        {
            if (selectedPhones == null)
            {
                storeToUpdate.PhonesStores = new List<PhonesStore>();
                return;
            }
            var selectedPhonesHS = new HashSet<string>(selectedPhones);
            var storedPhones = new HashSet<int>
            (storeToUpdate.PhonesStores.Select(c => c.Phone.ID));
            foreach (var phone in _context.Phones)
            {
                if (selectedPhonesHS.Contains(phone.ID.ToString()))
                {
                    if (!storedPhones.Contains(phone.ID))
                    {
                        storeToUpdate.PhonesStores.Add(new PhonesStore
                        {
                            StoreID = storeToUpdate.ID,
                            PhoneID = phone.ID
                        });
                    }
                }
                else
                {
                    if (storedPhones.Contains(phone.ID))
                    {
                        PhonesStore phoneToRemove = storeToUpdate.PhonesStores.FirstOrDefault(i
                       => i.PhoneID == phone.ID);
                        _context.Remove(phoneToRemove);
                    }
                }
            }
        }



        // pana aici la lab 4 pct 20-21 --------------------------------------------------------------------------



        // GET: Stores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExists(int id)
        {
            return _context.Stores.Any(e => e.ID == id);
        }
    }
}
