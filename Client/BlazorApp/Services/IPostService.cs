using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiContracts;

namespace BlazorApp.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPostsAsync(); 
       
        Task<CreatePostDto> AddPostAsync(CreatePostDto postDto);
        //Task<CreateUserDto> AddUserAsync(CreateUserDto userDto); 
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(int id);
    }
}