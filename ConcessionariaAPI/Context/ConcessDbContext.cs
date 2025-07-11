using ConcessionariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Context {
	public class ConcessDbContext : DbContext {

		public ConcessDbContext(DbContextOptions<ConcessDbContext> options) : base(options) { }

		public DbSet<Brand> Brands { get; set; }
		public DbSet<Vehicle> Vehicles { get; set; }
	}
}
