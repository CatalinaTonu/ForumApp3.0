using ApiContracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public class HttpUserService : IUserService
    {
        private readonly HttpClient client;

        public HttpUserService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<CreateUserDto> AddUserAsync(CreateUserDto userDto)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("user", userDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CreateUserDto>();
        }

        public async Task<User> GetUserAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"user/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<User>();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await client.GetFromJsonAsync<IEnumerable<User>>("user");
        }
        

        /*public async Task<IEnumerable<User>> GetUsersAsync(string username)
        {
            var url = String.IsNullOrEmpty(username) ? "user" : $"user?userUsername={username}";
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<User>>();
        }*/
        

        public async Task UpdateUserAsync(int id, CreateUserDto userDto)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"user/{id}", userDto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteUserAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"user/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}