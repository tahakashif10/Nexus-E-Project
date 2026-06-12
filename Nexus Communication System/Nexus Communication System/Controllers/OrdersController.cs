using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexus_Communication_System.Models;
using Nexus_Communication_System.Data;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class OrdersController : Controller
{
    private readonly Bridge _context;

    public OrdersController(Bridge context)
    {
        _context = context;
    }

    // GET: ORDERS
    // GET: ORDERS
    public async Task<IActionResult> Index()
    {
        return View(await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Service)
            .ToListAsync());
    }

    // GET: ORDERS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await _context.Orders
            .FirstOrDefaultAsync(m => m.Order_ID == id);
        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }

    // GET: ORDERS/Create
    public IActionResult Create()
    {
        ViewBag.Categories = _context.ServiceCategories.ToList();
        ViewBag.Services = _context.Services.ToList();

        return View();
    }

    // POST: ORDERS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Order order)
    {
        if (ModelState.IsValid)
        {
            var service = await _context.Services
                .FirstOrDefaultAsync(s => s.Service_ID == order.Service_ID);

            if (service == null)
            {
                ModelState.AddModelError("", "Service not found.");
            }
            else
            {
                var email = User.Identity.Name;

                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.Email == email);

                if (customer == null)
                {
                    ModelState.AddModelError("", "Customer not found.");
                }
                else
                {
                    order.Customer_ID = customer.Customer_ID;
                    order.Amount = service.Service_Charge;
                    order.Status = "Pending";
                    order.Payment_Status = "Pending";
                    order.Order_Date = DateTime.Now;

                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
        }

        ViewBag.Categories = _context.ServiceCategories.ToList();
        ViewBag.Services = _context.Services.ToList();

        return View(order);
    }
    // GET: ORDERS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        return View(order);
    }

    // POST: ORDERS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? order_id, [Bind("Order_ID,Customer_ID,Customer,Service_ID,Service,Employee_ID,Order_Date,Status,Payment_Status,Amount")] Order order)
    {
        if (order_id != order.Order_ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(order);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.Order_ID))
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
        return View(order);
    }

    // GET: ORDERS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await _context.Orders
            .FirstOrDefaultAsync(m => m.Order_ID == id);
        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }

    // POST: ORDERS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? order_id)
    {
        var order = await _context.Orders.FindAsync(order_id);
        if (order != null)
        {
            _context.Orders.Remove(order);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool OrderExists(int? order_id)
    {
        return _context.Orders.Any(e => e.Order_ID == order_id);
    }

    public IActionResult AssignEmployee(int id)
    {
        var order = _context.Orders.Find(id);

        ViewBag.Employees = _context.Employees.ToList();

        return View(order);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AssignEmployee(Order order)
    {
        var existingOrder = await _context.Orders
            .FindAsync(order.Order_ID);

        if (existingOrder == null)
        {
            return NotFound();
        }

        existingOrder.Employee_ID = order.Employee_ID;
        existingOrder.Status = "Assigned";

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}

