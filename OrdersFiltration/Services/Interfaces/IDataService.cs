using OrdersFiltration.Models;

namespace OrdersFiltration.Services.Interfaces
{
    public interface IDataService
    {
		public List<Order> Orders { get; set; }
		public Task<bool> LoadDataAsync();
		public Task<bool> SaveDataAsync(List<Order> orders, string path);

	}
}