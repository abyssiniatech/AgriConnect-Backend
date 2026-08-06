using AgriConnect.Domain.Entities;
using AgriConnect.Persistence.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriConnect.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FarmersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public FarmersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetFarmers()
    {
        var farmers = await _context.Farmers.ToListAsync();

        return Ok(farmers);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFarmer(Farmer farmer)
    {
        _context.Farmers.Add(farmer);

        await _context.SaveChangesAsync();

        return Ok(farmer);
    }
}