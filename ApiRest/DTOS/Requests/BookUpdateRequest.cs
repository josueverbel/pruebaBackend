using ApiRest.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiRest.DTOS.Requests
{
    public class BookUpdateRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Id must be a positive number")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public int PageCount { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Page Count must be a positive number")]
        public int EditorialId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Editorial Id must be a positive number")]
        public int AuthorId { get; set; }


        public Book ToBook()
        {
            //Solo campos editables
            Book book = new Book();
            book.Id = Id;
            book.Title = Title;
            book.Year = Year;
            book.PageCount = PageCount;
            book.EditorialId = EditorialId;
            book.AuthorId = AuthorId;
            return book;

        }
    }
}
