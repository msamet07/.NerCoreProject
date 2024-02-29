using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Workintech02RestApiDemo.Domain.Entities;

namespace Workintech02RestApiDemo.Infrastructure
{
    public static class DataSeeder
    {
        public static void SeedCodeFirst(IApplicationBuilder app)
        {

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<Workintech02CodeFirstContext>();

                if (context == null)
                    return;

                context.Database.Migrate();
                context.Database.EnsureCreated();

                #region CitySeed
                if (!context.Cities.Any())
                {
                    var cities = new List<City>()
                    {
                        new City(){Name="Ankara"},
                        new City(){Name="İstanbul"},
                        new City(){Name="İzmir"},
                        new City(){Name="Eskişehir"},
                        new City(){Name="Bursa"},
                    };
                    context.Cities.AddRange(cities);
                }
                #endregion

                #region UserSeed
                if (!context.Users.Any())
                {

                    var users = new List<User>()
                    {
                        //"$2a$11$OcrGyHGYDRTtz0lq71.l.O8Eh5vKHZy5ugWdJPexDoPDYc8Tg6PPC" ==> 12345
                        new User(){Name="User 1",UserName="username1",Email="a@b.com",Password="$2a$11$OcrGyHGYDRTtz0lq71.l.O8Eh5vKHZy5ugWdJPexDoPDYc8Tg6PPC"},
                        new User(){Name="User 2",UserName="username2",Email="a@b.com",Password="$2a$11$OcrGyHGYDRTtz0lq71.l.O8Eh5vKHZy5ugWdJPexDoPDYc8Tg6PPC"},
                        new User(){Name="User 3",UserName="username3",Email="a@b.com",Password="$2a$11$OcrGyHGYDRTtz0lq71.l.O8Eh5vKHZy5ugWdJPexDoPDYc8Tg6PPC"},
                        new User(){Name="User 4",UserName="username4",Email="a@b.com",Password="$2a$11$OcrGyHGYDRTtz0lq71.l.O8Eh5vKHZy5ugWdJPexDoPDYc8Tg6PPC"},
                        new User(){Name="User 5",UserName="username5",Email="a@b.com",Password="$2a$11$OcrGyHGYDRTtz0lq71.l.O8Eh5vKHZy5ugWdJPexDoPDYc8Tg6PPC"},
                    };
                    context.Users.AddRange(users);
                };
                #endregion

                #region MovieSeed
                List<Movie> movies = new List<Movie>
                {
                    new Movie { IsActive=true,Title = "The Shawshank Redemption", ReleaseYear = 1994 },
                    new Movie { IsActive=true,Title = "The Godfather", ReleaseYear = 1972 },
                    new Movie { IsActive=true,Title = "The Dark Knight", ReleaseYear = 2008 },
                    new Movie { IsActive=true,Title = "Pulp Fiction", ReleaseYear = 1994 },
                    new Movie { IsActive=true,Title = "Fight Club", ReleaseYear = 1999 },
                    new Movie { IsActive=true,Title = "Forrest Gump", ReleaseYear = 1994 },
                    new Movie { IsActive=true,Title = "Inception", ReleaseYear = 2010 },
                    new Movie { IsActive=true,Title = "The Matrix", ReleaseYear = 1999 },
                    new Movie { IsActive=true,Title = "Interstellar", ReleaseYear = 2014 },
                    new Movie { IsActive=true,Title = "Gladiator", ReleaseYear = 2000 },
                };
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(movies);
                }
                #endregion

                #region RoleSeed
                if (!context.Roles.Any())
                {
                    var roles = new List<Role>()
                    {
                        new Role(){Name="admin"},
                        new Role(){Name="employee"},
                        new Role(){Name="manager"},
                        new Role(){Name="hradmin"},
                    };
                    context.Roles.AddRange(roles);
                }
                #endregion

                context.SaveChanges();
            }
        }

        public static void Seed(IApplicationBuilder app)
        {

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<WorkintechBlogDemoContext>();
                if (context != null && !context.Blogs.Any())
                {
                    var blogList = new List<Blog>()
                {
                    new Blog(){Description="Blog 1 Description",Title="Blog Title 1",Url="https://www.blogtitle1.com"},
                    new Blog(){Description="Blog 2 Description",Title="Blog Title 2",Url="https://www.blogtitle2.com"},
                    new Blog(){Description="Blog 3 Description", Title="Blog Title 3",Url="https://www.blogtitle3.com"},
                };
                    context.Blogs.AddRange(blogList);
                    context.SaveChanges();
                }
            }
        }
    }
}
