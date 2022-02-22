using ApiRest.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiRest.DTOS.Requests
{
    public class AuthorUpdateRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Id must be a positive number")]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? Dob { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string City { get; set; }
        public Author ToAuthor()
        {
            var author = new Author();

            return author;
        }
    }
}
