namespace Workintech02RestApiDemo.Business.Post
{
    public interface IPostService:IBaseService
    {
        Task<Domain.Entities.Post> CreatePostAsync(Domain.Entities.Post post);
        Task DeletePostAsync(int id);
        Task<Domain.Entities.Post> GetPostAsync(int id);
        Task<List<Domain.Entities.Post>> GetPostsAsync();
        Task UpdatePostAsync(Domain.Entities.Post post);
    }
}