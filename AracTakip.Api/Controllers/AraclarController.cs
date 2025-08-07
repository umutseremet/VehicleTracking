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

        // PUT: api/araclar/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArac(int id, Arac arac)
        {
            if (id != arac.Id)
            {
                return BadRequest("ID uyuşmazlığı");
            }

            try
            {
                var existingArac = await _context.Araclar.FindAsync(id);
                if (existingArac == null)
                {
                    return NotFound($"ID {id} ile araç bulunamadı.");
                }

                // Güncelleme
                existingArac.Plaka = arac.Plaka;
                existingArac.Marka = arac.Marka;
                existingArac.Model = arac.Model;
                existingArac.Yil = arac.Yil;
                existingArac.VIN = arac.VIN;
                existingArac.SirketAdi = arac.SirketAdi;
                existingArac.MuayeneTarihi = arac.MuayeneTarihi;
                existingArac.KaskoTrafik = arac.KaskoTrafik;
                existingArac.KaskoTrafikTarihi = arac.KaskoTrafikTarihi;
                existingArac.SonBakimTarihi = arac.SonBakimTarihi;
                existingArac.GuncelKm = arac.GuncelKm;
                existingArac.YakitTuketimi = arac.YakitTuketimi;
                existingArac.LastikDurumu = arac.LastikDurumu;
                existingArac.RuhsatBilgisi = arac.RuhsatBilgisi;
                existingArac.SirketKiralama = arac.SirketKiralama;
                existingArac.KullaniciAdi = arac.KullaniciAdi;
                existingArac.KullaniciTelefon = arac.KullaniciTelefon;
                existingArac.Konum = arac.Konum;
                existingArac.AracResimUrl = arac.AracResimUrl;

                await _context.SaveChangesAsync();
                return Ok(existingArac);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Araç {Id} güncellenirken hata oluştu", id);
                return StatusCode(500, "İç sunucu hatası");
            }
        }

        // PATCH: api/araclar/5/ruhsat - Ruhsat bilgilerini güncelle
        [HttpPatch("{id}/ruhsat")]
        public async Task<IActionResult> UpdateRuhsatBilgileri(int id, [FromBody] RuhsatUpdateDto ruhsatDto)
        {
            try
            {
                var arac = await _context.Araclar.FindAsync(id);
                if (arac == null)
                {
                    return NotFound($"ID {id} ile araç bulunamadı.");
                }

                // Sadece ruhsat bilgilerini güncelle
                arac.Plaka = ruhsatDto.Plaka;
                arac.VIN = ruhsatDto.VIN;
                arac.Marka = ruhsatDto.Marka;
                arac.Model = ruhsatDto.Model;
                arac.Yil = ruhsatDto.Yil;

                await _context.SaveChangesAsync();
                return Ok(arac);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Araç {Id} ruhsat bilgileri güncellenirken hata oluştu", id);
                return StatusCode(500, "İç sunucu hatası");
            }
        }

        // PATCH: api/araclar/5/kullanici - Kullanıcı bilgilerini güncelle
        [HttpPatch("{id}/kullanici")]
        public async Task<IActionResult> UpdateKullaniciBilgileri(int id, [FromBody] KullaniciUpdateDto kullaniciDto)
        {
            try
            {
                var arac = await _context.Araclar.FindAsync(id);
                if (arac == null)
                {
                    return NotFound($"ID {id} ile araç bulunamadı.");
                }

                // Sadece kullanıcı bilgilerini güncelle
                arac.KullaniciAdi = kullaniciDto.KullaniciAdi;
                arac.KullaniciTelefon = kullaniciDto.KullaniciTelefon;

                await _context.SaveChangesAsync();
                return Ok(arac);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Araç {Id} kullanıcı bilgileri güncellenirken hata oluştu", id);
                return StatusCode(500, "İç sunucu hatası");
            }
        }

        // PATCH: api/araclar/5/arac-bilgileri - Araç bilgilerini güncelle
        [HttpPatch("{id}/arac-bilgileri")]
        public async Task<IActionResult> UpdateAracBilgileri(int id, [FromBody] AracBilgileriUpdateDto aracDto)
        {
            try
            {
                var arac = await _context.Araclar.FindAsync(id);
                if (arac == null)
                {
                    return NotFound($"ID {id} ile araç bulunamadı.");
                }

                // Araç bilgilerini güncelle (Kasko ve Yakıt hariç)
                arac.MuayeneTarihi = aracDto.MuayeneTarihi;
                arac.SonBakimTarihi = aracDto.SonBakimTarihi;
                arac.GuncelKm = aracDto.GuncelKm;
                arac.LastikDurumu = aracDto.LastikDurumu;
                arac.KaskoTrafikTarihi = aracDto.KaskoTarihi;

                await _context.SaveChangesAsync();
                return Ok(arac);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Araç {Id} araç bilgileri güncellenirken hata oluştu", id);
                return StatusCode(500, "İç sunucu hatası");
            }
        }
    }

    // Ruhsat güncelleme için DTO
    public class RuhsatUpdateDto
    {
        public string Plaka { get; set; } = string.Empty;
        public string VIN { get; set; } = string.Empty;
        public string Marka { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Yil { get; set; }
    }

    // Kullanıcı güncelleme için DTO
    public class KullaniciUpdateDto
    {
        public string KullaniciAdi { get; set; } = string.Empty;
        public string KullaniciTelefon { get; set; } = string.Empty;
    }

    // Araç bilgileri güncelleme için DTO
    public class AracBilgileriUpdateDto
    {
        public DateTime MuayeneTarihi { get; set; }
        public DateTime KaskoTarihi { get; set; }
        public DateTime SonBakimTarihi { get; set; }
        public int GuncelKm { get; set; }
        public string LastikDurumu { get; set; } = string.Empty;
    }
}