using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private readonly string filePath = "posts.json";
//writes to a list the comment
    public PostFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<Post> AddAsync(Post post)
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson);
        int maxId=posts.Count > 0 ? posts.Max(c => c.Id) : 1;
        post.Id = maxId + 1;
        posts.Add(post);
        postsAsJson= JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsAsJson);
        return post;
    }
    //retrieve from list
    public async Task<List<Post>> GetPosts()
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Post>>(postsAsJson);
    }
    private async Task WritePostsAsync(List<Post> posts)
    {
        string postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsAsJson);
    }
    public async Task DeleteAsync(int id)
    {
        var posts = await GetPosts();
        Post postToRemove = posts.FirstOrDefault(c => c.Id == id);
        posts.Remove(postToRemove);
        await WritePostsAsync(posts);
        
    }
    public async Task UpdateAsync(Post post)
    {
        var posts = await GetPosts();
        Post postToUpdate = posts.FirstOrDefault(c => c.Id == post.Id);
        posts.Remove(postToUpdate);
        posts.Add(post);
        await WritePostsAsync(posts);
    }
    public async Task<Post> GetSingleAsync(int id)
    {
        var posts = await GetPosts();
        Post post = posts.FirstOrDefault(c => c.Id == id);
        await WritePostsAsync(posts);
        return post;
    }
    public IQueryable<Post> GetMany()
    {
        string postsAsJson = File.ReadAllText(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson);
        return posts.AsQueryable();
    }

    

}