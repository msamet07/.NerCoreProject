using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workintech02RestApiDemo.Infrastructure;

namespace Workintech02RestApiDemo.Business.Test
{
    public static class TestHelper
    {
        public static Workintech02CodeFirstContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<Workintech02CodeFirstContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            var mockDb= new Workintech02CodeFirstContext(options);
            return mockDb;
        }
    }
}
