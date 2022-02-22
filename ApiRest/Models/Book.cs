using ApiRest.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRest.Models
{
    public class Book : BaseModel
    {
        
        public string Title { get; set; }
        public string Year { get; set; }
        public int PageCount { get; set; }
     
        public int EditorialId { get; set; }
        public Editorial Editorial { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
