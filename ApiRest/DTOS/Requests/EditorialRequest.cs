using ApiRest.DTOS.Requests.Validations;
using ApiRest.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiRest.DTOS.Requests
{
    public class EditorialRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxCountEditorialValidation(ErrorMessage = "need a positive number, bigger than 0 or -1")]
        public int MaxCount { get; set; }

       public  Editorial ToEditorial()
        {
           var Editorial = new Editorial();
            Editorial.Name = Name;
            Editorial.Address = Address;
            Editorial.Phone = Phone;
            Editorial.Email = Email;
            Editorial.MaxCount = MaxCount;
            return Editorial;
           
        }
    }
}
