using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string filePath = "comments.json";
//writes to a list the comment
    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
//add to list
    public async Task<Comment> AddAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson);
        int maxId=comments.Count > 0 ? comments.Max(c => c.Id) : 1;
        comment.Id = maxId + 1;
        comments.Add(comment);
        commentsAsJson= JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
        return comment;

    }
    //retrieve from list
    public async Task<List<Comment>> GetComments()
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Comment>>(commentsAsJson);
    }
//
    private async Task WriteCommentsAsync(List<Comment> comments)
    {
        string commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }

    public async Task DeleteAsync(int id)
    {
        var comments = await GetComments();
        Comment commentToRemove = comments.FirstOrDefault(c => c.Id == id);
        comments.Remove(commentToRemove);
        await WriteCommentsAsync(comments);
        
    }

    public async Task UpdateAsync(Comment comment)
    {
        var comments = await GetComments();
        Comment commentToUpdate = comments.FirstOrDefault(c => c.Id == comment.Id);
        comments.Remove(commentToUpdate);
        comments.Add(comment);
        await WriteCommentsAsync(comments);
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        var comments = await GetComments();
        Comment comment = comments.FirstOrDefault(c => c.Id == id);
        await WriteCommentsAsync(comments);
        return comment;
    }

    public IQueryable<Comment> GetMany()
    {
        string commentsAsJson = File.ReadAllText(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson);
        return comments.AsQueryable();
    }
    
    
}