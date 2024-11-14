using Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ApiContracts;

namespace BlazorApp.Services
{
    public class HttpPostService : IPostService
    {
        private readonly HttpClient _httpClient;

        public HttpPostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Post>>("post");
        }

       /* public async Task<Post> CreatePostAsync(Post post)
        {
            var response = await _httpClient.PostAsJsonAsync("api/posts", post);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Post>();
        }*/
       
       public async Task<CreatePostDto> AddPostAsync(CreatePostDto postDto)
       {
           HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Post", postDto);
           response.EnsureSuccessStatusCode();
           return await response.Content.ReadFromJsonAsync<CreatePostDto>();
       }

        public async Task UpdatePostAsync(Post post)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/posts/{post.Id}", post);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePostAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/posts/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}