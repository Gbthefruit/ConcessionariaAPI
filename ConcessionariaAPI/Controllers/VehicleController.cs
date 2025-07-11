using ConcessionariaAPI.Context;
using ConcessionariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Controllers {

	[Route("[Controller]")]
	[ApiController]
	public class VehicleController : Controller {

		private readonly ConcessDbContext _context;

		public VehicleController(ConcessDbContext context) {
			_context = context;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Vehicle>> GetAll() {
			try {
				var vehicles = _context.Vehicles.Take(5).AsNoTracking().ToList();

				if (vehicles is null) {
					return NotFound("Nenhum veículo com esse ID foi encontrado...");
				}
			
				return Ok(vehicles);
			}
			catch (Exception) {
				return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro com a sua solicitação.");
			}
		}

		[HttpGet("{id:int}", Name = "ObterVeiculo")]
		public ActionResult<Vehicle> Get(int id) {
			try {
				var vehicle = _context.Vehicles.FirstOrDefault(x => x.Id == id);

				if (vehicle.Id != id) {
					return NotFound("Nenhum veículo com esse ID foi encontrado...");
				}

				return Ok(vehicle);
			}
			catch (Exception) {
				return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro com a sua solicitação.");
			}
		}

		[HttpPost]
		public ActionResult Post(Vehicle v) {
			try {
				if (v is null) {
					return BadRequest("Criação de veículo inválida.");
				}
				_context.Vehicles.Add(v);
				_context.SaveChanges();
				return new CreatedAtRouteResult("ObterVeiculo", new {id = v.Id }, v);
			}
			catch (Exception ) {
				return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro com a sua solicitação.");
			}
		}

		[HttpPut("{id:int}")]
		public ActionResult Put(int id, Vehicle v) {
			try {
				if (v.Id != id) {
					return NotFound("Nenhum veículo com esse ID foi encontrado...");
				}
				_context.Vehicles.Entry(v).State = EntityState.Modified;
				_context.SaveChanges();

				return Ok(v);
			}
			catch (Exception) {
				return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro com a sua solicitação.");
			}
		}

		[HttpDelete("{id:int}")]
		public ActionResult Delete(int id) {
			try {
				var vehicle = _context.Vehicles.FirstOrDefault(v => v.Id == id);
				if (vehicle == null) {
					return NotFound("Nenhum veículo com esse ID foi encontrado...");
				}
				_context.Vehicles.Remove(vehicle);
				_context.SaveChanges();

				return Ok("Veículo Removido.");
			}
			catch (Exception) {
				return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro com a sua solicitação.");
			}
		}
	}
}
