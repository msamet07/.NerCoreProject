

namespace Workintech02RestApiDemo.Domain.Entities
{
    public class City:BaseEntity
    {  
        public string Name { get; set; }

        public List<Town> Towns { get; set; }
    }
}
