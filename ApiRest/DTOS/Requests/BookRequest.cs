using ApiRest.DTOS.Requests.Validations;
using ApiRest.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRest.DTOS.Requests
{
    public class BookRequest
    {
        public string Title { get; set; }

        [Required]
        [Range(1900, 2022, ErrorMessage = "year must be between 1900 and 2022")]
        public int Year { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "page count must be a positive value")]
        public int PageCount { get; set; }

       
        [Range(1, int.MaxValue, ErrorMessage = "Editorial Id must be a positive value")]
        public int EditorialId { get; set; }

        
        [Range(1, int.MaxValue, ErrorMessage = "Author ID must be a positive value")]
        public int AuthorId { get; set; }
        public Book ToBook()
        {
            var book = new Book();
            book.Title = Title;
            book.Year =  Year.ToString();
            book.AuthorId = AuthorId;
            book.PageCount = PageCount;
            book.EditorialId = EditorialId;
            book.AuthorId = AuthorId;
            return book;
        }
    }
}
