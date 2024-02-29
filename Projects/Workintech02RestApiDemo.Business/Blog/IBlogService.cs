namespace Workintech02RestApiDemo.Business.Blog
{
    public interface IBlogService:IBaseService
    {
        Task<Domain.Entities.Blog> CreateBlogAsync(Domain.Entities.Blog blog);
        Task DeleteBlogAsync(int id);
        Task<Domain.Entities.Blog> GetBlogAsync(int id);
        Task<List<Domain.Entities.Blog>> GetBlogsAsync();
        Task UpdateBlogAsync(Domain.Entities.Blog blog);
    }
}