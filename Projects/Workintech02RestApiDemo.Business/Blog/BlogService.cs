using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workintech02RestApiDemo.Infrastructure;
using Workintech02RestApiDemo.Domain.Entities;

namespace Workintech02RestApiDemo.Business.Blog
{
    public class BlogService : BaseService, IBlogService
    {
        private readonly WorkintechBlogDemoContext _context;

        public BlogService(WorkintechBlogDemoContext context)
        {
            _context = context;
        }

        public async Task<List<Workintech02RestApiDemo.Domain.Entities.Blog>> GetBlogsAsync()
        {
            return await _context.Blogs.Include(x=>x.Posts).ToListAsync();
        }

        public async Task<Workintech02RestApiDemo.Domain.Entities.Blog> GetBlogAsync(int id)
        {
            return await _context.Blogs.Where(x=>x.Id==id).Include(x => x.Posts).FirstOrDefaultAsync();
        }

        public async Task<Workintech02RestApiDemo.Domain.Entities.Blog> CreateBlogAsync(Workintech02RestApiDemo.Domain.Entities.Blog blog)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return blog;
        }

        public async Task UpdateBlogAsync(Workintech02RestApiDemo.Domain.Entities.Blog blog)
        {
            _context.Entry(blog).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBlogAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();
            }
        }
    }
}
