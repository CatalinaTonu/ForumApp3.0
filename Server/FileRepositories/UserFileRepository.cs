using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "users.json";

//writes to a list the comment
    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<User> AddAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson);
        int maxId = users.Count > 0 ? users.Max(c => c.Id) : 1;
        user.Id = maxId + 1;
        users.Add(user);
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
        return user;
    }
    public async Task<List<User>> GetUsers()
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<User>>(usersAsJson);
    }
    private async Task WriteUsersAsync(List<User> users)
    {
        string usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }
    public async Task DeleteAsync(int id)
    {
        var users = await GetUsers();
        User userToRemove = users.FirstOrDefault(c => c.Id == id);
        users.Remove(userToRemove);
        await WriteUsersAsync(users);
        
    }
    public async Task UpdateAsync(User user)
    {
        var users = await GetUsers();
        User userToUpdate = users.FirstOrDefault(c => c.Id == user.Id);
        users.Remove(userToUpdate);
        users.Add(user);
        await WriteUsersAsync(users);
    }
    public async Task<User> GetSingleAsync(int id)
    {
        var users = await GetUsers();
        User user = users.FirstOrDefault(c => c.Id == id);
        await WriteUsersAsync(users);
        return user;
    }
    public IQueryable<User> GetMany()
    {
        string usersAsJson = File.ReadAllText(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson);
        return users.AsQueryable();
    }

}

