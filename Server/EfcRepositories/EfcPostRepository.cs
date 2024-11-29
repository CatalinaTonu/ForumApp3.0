﻿using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcPostRepository : IPostRepository
{
    private readonly AppContext ctx;

    public EfcPostRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Post> AddAsync(Post post)
    {
        EntityEntry entityEntry = await ctx.Posts.AddAsync(post); 
        await ctx.SaveChangesAsync(); 
        return entityEntry.Entity;
        
    }

    public async Task  DeleteAsync(int id)
    {
        Post? existing = await ctx.Posts.SingleOrDefaultAsync(p => p.Id == id);
        if (existing == null) { throw new NotFoundException($"Post with id {id} not found"); } 
        ctx.Posts.Remove(existing); await ctx.SaveChangesAsync();
        
    }

    /*public async Task<List<Post>> GetPosts()
    {
        
    }*/

    private async Task WritePostsAsync(List<Post> posts)
    {
        
    }

    public async Task UpdateAsync(Post post)
    {
        if (!(await ctx.Posts.AnyAsync(p => p.Id == post.Id))) 
        
        { throw new NotFoundException("Post with id {post.Id} not found"); }
        ctx.Posts.Update(post); await ctx.SaveChangesAsync();
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        
    }

    public IQueryable<Post> GetMany()
    {
        return ctx.Posts.AsQueryable();
    }
    
    

}