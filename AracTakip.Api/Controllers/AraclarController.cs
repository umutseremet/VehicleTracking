using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AracTakip.Api.Data;
using AracTakip.Api.Models;

namespace AracTakip.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AraclarController : ControllerBase
    {
        private readonly AracTakipDbContext _context;
        private readonly ILogger<AraclarController> _logger;

        public AraclarController(AracTakipDbContext context, ILogger<AraclarController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/araclar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Arac>>> GetAraclar()
        {
            try
            {
                var araclar = await _context.Araclar.ToListAsync();
                return Ok(araclar);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Araçlar getirilirken hata oluştu");
                return StatusCode(500, "İç sunucu hatası");
            }
        }

        // GET: api/araclar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Arac>> GetArac(int id)
        {
            try
            {
                var arac = await _context.Araclar.FindAsync(id);

                if (arac == null)
                {
                    return NotFound($"ID {id} ile araç bulunamadı.");
                }

                return Ok(arac);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Araç {Id} getirilirken hata oluştu", id);
                return StatusCode(500, "İç sunucu hatası");
            }
        }
    }
}