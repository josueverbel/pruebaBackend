using ApiRest.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiRest.DTOS.Requests
{
    public class AuthorRequest
    {
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
           
            author.FirstName = FirstName;
            author.LastName = LastName;
            author.Dob = Dob.Value;
            author.Email = Email;
            author.City = City;
            return author;
        }
    }
}
