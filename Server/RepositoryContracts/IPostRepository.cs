using Entities;

namespace RepositoryContracts;

public interface IPostRepository
{
    Task<Post> AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(int UserId);
    Task<Post> GetSingleAsync(int UserId);
    IQueryable<Post> GetMany();
}