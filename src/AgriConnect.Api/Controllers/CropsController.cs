using AgriConnect.Domain.Entities;
using AgriConnect.Persistence.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriConnect.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CropsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CropsController(ApplicationDbContext context)
    {
        _context = context;
    }


    // GET: api/Crops
    [HttpGet]
    public async Task<IActionResult> GetCrops()
    {
        var crops = await _context.Crops
            .Include(c => c.Farm)
            .ToListAsync();

        return Ok(crops);
    }


    // GET: api/Crops/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCrop(int id)
    {
        var crop = await _context.Crops
            .Include(c => c.Farm)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (crop == null)
        {
            return NotFound();
        }

        return Ok(crop);
    }


    // POST: api/Crops
    [HttpPost]
    public async Task<IActionResult> CreateCrop(Crop crop)
    {
        var farmExists = await _context.Farms
            .AnyAsync(f => f.Id == crop.FarmId);

        if (!farmExists)
        {
            return BadRequest(
                "Farm does not exist.");
        }


        _context.Crops.Add(crop);

        await _context.SaveChangesAsync();


        return CreatedAtAction(
            nameof(GetCrop),
            new { id = crop.Id },
            crop);
    }


    // PUT: api/Crops/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCrop(
        int id,
        Crop crop)
    {
        if (id != crop.Id)
        {
            return BadRequest();
        }


        _context.Entry(crop).State =
            EntityState.Modified;


        await _context.SaveChangesAsync();

        return NoContent();
    }


    // DELETE: api/Crops/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCrop(int id)
    {
        var crop = await _context.Crops
            .FindAsync(id);

        if (crop == null)
        {
            return NotFound();
        }


        _context.Crops.Remove(crop);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}