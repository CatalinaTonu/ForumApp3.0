using ApiContracts;
using Entities;

namespace BlazorApp.Services;

public interface IUserService
{
    Task<CreateUserDto> AddUserAsync(CreateUserDto userDto); 
    Task<User> GetUserAsync(int id);                        
    Task<IEnumerable<User>> GetUsersAsync(); 
    Task UpdateUserAsync(int id, CreateUserDto userDto);    
    Task DeleteUserAsync(int id);                           
}
