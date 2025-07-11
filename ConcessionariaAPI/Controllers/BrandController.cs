using ConcessionariaAPI.Context;
using ConcessionariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;

namespace ConcessionariaAPI.Controllers {
	[Route("[Controller]")]
	[ApiController]
	public class BrandController : Controller {

		private readonly ConcessDbContext _context;

		public BrandController(ConcessDbContext context) {
			_context = context;
		}

		[HttpGet("Veiculos")]
		public ActionResult<IEnumerable<Brand>> GetBrandVehicle() {
			try {
				return _context.Brands.Include(b => b.Vehicles).Where(b => b.Id <= 5).ToList();
			}
			catch (Exception) {
				return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro com a sua solicitação.");
			}	
		}

		[HttpGet]
		public ActionResult<IEnumerable<Brand>> GetAll() {
			try {
				var brands = _context.Brands.Take(5).AsNoTracking().ToList();
				if (brands is null) {
					return NotFound("Nenhuma marca com esse ID foi encontrada...");
				}
				return Ok(brands);
			}
			catch (Exception) {
				return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro com a sua solicitação.");
			}
		}

		[HttpGet("{id:int}", Name = "ObterMarca")]
		public ActionResult<Brand> Get(int id) {
			try {
				var brand = _context.Brands.FirstOrDefault(b => b.Id == id);
				if (brand is null) {
					return NotFound("Nenhuma marca com esse ID foi encontrada...");
				}
				return Ok(brand);
			}
			catch (Exception) {
				return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro com a sua solicitação.");
			}
		}

		[HttpPost]
		public ActionResult Post(Brand b) {
			try {
				if (b is not Brand) {
					return BadRequest("Marca inválida.");
				}

				_context.Brands.Add(b);
				_context.SaveChanges();

				return new CreatedAtRouteResult("ObterMarca", new { id = b.Id }, b);
			}
			catch (Exception) {
				return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro com a sua solicitação.");
			}
		}

		[HttpPut("{id:int}")]
		public ActionResult Put(int id, Brand b) {
			try {
				var brand = _context.Brands.FirstOrDefault(b => b.Id == id);
				if (brand is null) {
					return NotFound("Nenhuma marca com esse ID foi encontrada...");
				}
				_context.Brands.Entry(brand).State = EntityState.Modified;
				_context.SaveChanges();

				return Ok(brand);
			}
			catch (Exception) {
				return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro com a sua solicitação.");
			}
		}
	}
}
