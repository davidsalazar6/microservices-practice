using Microsoft.AspNetCore.Mvc;
using OrderService.Data;
using OrderService.Models;
using OrderService.Services;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserServiceClient _userService;

    public OrdersController(AppDbContext context, UserServiceClient userService)
    {
        _context = context;
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Order order)
    {
        var exists = await _userService.UserExists(order.UserId);
        if (!exists)
            return BadRequest("UserId not found in UserService");

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), null, order);
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_context.Orders.ToList());
}