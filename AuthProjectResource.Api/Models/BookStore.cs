using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthProjectResource.Api.Models
{
    public class BookStore
    {
        public List<Book> Books = new List<Book>
        {
            new Book { Id = 1, Author = "J. K. Rowling", Title = "Harry Potter and Philosopher's Stone", Price = 10.45M },
            new Book { Id = 2, Author = "Hermal Melville", Title = "Moby Dick", Price = 8.25M },
            new Book { Id = 3, Author = "Jules Vren", Title = "The Mysterious Island", Price = 7.11M },
            new Book { Id = 4, Author = "Carlo Collodi", Title = "The Adventires of Pinocchio", Price = 6.42M }
        };

        public Dictionary<Guid, int[]> Orders => new Dictionary<Guid, int[]>
        {
            {Guid.Parse("e2371dc9-a849-4f3c-9001-df8fc921c13a"), new int[] {1, 3} },
            {Guid.Parse("e2371dc9-a849-4f3c-9003-df8fc921c13a"), new int[] {2, 3, 4} }
        };
    }
}
