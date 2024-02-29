using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workintech02RestApiDemo.Infrastructure;

namespace Workintech02RestApiDemo.Business.Movie
{
    public class MovieService : BaseService, IMovieService
    {
        private readonly Workintech02CodeFirstContext _context;

        public MovieService(Workintech02CodeFirstContext context)
        {
            _context = context;
        }

        public async Task<List<Workintech02RestApiDemo.Domain.Entities.Movie>> GetMoviesAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Workintech02RestApiDemo.Domain.Entities.Movie> GetMovieAsync(int id)
        {
            var result = await _context.Movies.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(result==null)
            {
                throw new ArgumentException("Movie not found");
            }
            return result;
        }

        public async Task<Workintech02RestApiDemo.Domain.Entities.Movie> CreateMovieAsync(Workintech02RestApiDemo.Domain.Entities.Movie movie)
        {
            movie.IsActive = true;
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task UpdateMovieAsync(Workintech02RestApiDemo.Domain.Entities.Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x=>x.Id==id);
            if (movie != null)
            {
                movie.IsActive = false;
                _context.Entry(movie).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}
