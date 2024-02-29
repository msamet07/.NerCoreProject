using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workintech02RestApiDemo.Domain.Entities;
using Workintech02RestApiDemo.Infrastructure;

namespace Workintech02RestApiDemo.Business.Post
{
    public class PostService : BaseService, IPostService
    {
        private readonly WorkintechBlogDemoContext _context;

        public PostService(WorkintechBlogDemoContext context)
        {
            _context = context;
        }

        public async Task<List<Workintech02RestApiDemo.Domain.Entities.Post>> GetPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Workintech02RestApiDemo.Domain.Entities.Post> GetPostAsync(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task<Workintech02RestApiDemo.Domain.Entities.Post> CreatePostAsync(Workintech02RestApiDemo.Domain.Entities.Post post)
        {
            using(
                var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Posts.Add(post);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task UpdatePostAsync(Workintech02RestApiDemo.Domain.Entities.Post post)
        {
            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }
    }
}
