namespace SalesManagementLibrary.Repo.Interfaces
{
    public interface IEntityRepository<TReturn, UDto>
    {
        // Create
        Task<TReturn?> CreateAsync(UDto createDto);

        // Read
        Task<List<TReturn?>> GetAllAsync();
        Task<TReturn?> GetByIdAsync(int id);

        // Update
        Task UpdateAsync(int id, UDto createDto);

        // Delete
        Task DeleteAsync(int id);
    }
}
