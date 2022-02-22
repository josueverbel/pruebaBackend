using ApiRest.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRest.Models
{
    public class Editorial : BaseModel
    {
       
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int MaxCount { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
