
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexus_Communication_System.Models;
using Nexus_Communication_System.Data;

public class ServiceCategoriesController : Controller
{
    private readonly Bridge _context;

    public ServiceCategoriesController(Bridge context)
    {
        _context = context;
    }

    // GET: SERVICECATEGORYS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.ServiceCategories.ToListAsync());
    }

    // GET: SERVICECATEGORYS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var servicecategory = await _context.ServiceCategories
            .FirstOrDefaultAsync(m => m.Category_ID == id);
        if (servicecategory == null)
        {
            return NotFound();
        }

        return View(servicecategory);
    }

    // GET: SERVICECATEGORYS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: SERVICECATEGORYS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Category_ID,Category_Name,Description")] ServiceCategory servicecategory)
    {
        if (ModelState.IsValid)
        {
            _context.Add(servicecategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(servicecategory);
    }

    // GET: SERVICECATEGORYS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var servicecategory = await _context.ServiceCategories.FindAsync(id);
        if (servicecategory == null)
        {
            return NotFound();
        }
        return View(servicecategory);
    }

    // POST: SERVICECATEGORYS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? category_id, [Bind("Category_ID,Category_Name,Description")] ServiceCategory servicecategory)
    {
        if (category_id != servicecategory.Category_ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(servicecategory);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceCategoryExists(servicecategory.Category_ID))
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
        return View(servicecategory);
    }

    // GET: SERVICECATEGORYS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if ( id == null)
        {
            return NotFound();
        }

        var servicecategory = await _context.ServiceCategories
            .FirstOrDefaultAsync(m => m.Category_ID == id);
        if (servicecategory == null)
        {
            return NotFound();
        }

        return View(servicecategory);
    }

    // POST: SERVICECATEGORYS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? category_id)
    {
        var servicecategory = await _context.ServiceCategories.FindAsync(category_id);
        if (servicecategory != null)
        {
            _context.ServiceCategories.Remove(servicecategory);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ServiceCategoryExists(int? category_id)
    {
        return _context.ServiceCategories.Any(e => e.Category_ID == category_id);
    }
}
