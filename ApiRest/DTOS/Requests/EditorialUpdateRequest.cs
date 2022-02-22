using ApiRest.DTOS.Requests.Validations;
using ApiRest.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiRest.DTOS.Requests
{
    public class EditorialUpdateRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Id must be a positive number")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Required]
        [MaxCountEditorialValidation(ErrorMessage = "need a positive number, bigger than 0 or -1")]
        public int MaxCount { get; set; }

        public Editorial ToEditorial()
        {
            //Solo campos editables
            Editorial Editorial = new Editorial();
            Editorial.Id = Id;
            Editorial.Name = Name;
            Editorial.Address = Address;
            Editorial.Phone = Phone;
            Editorial.Email = Email;
            Editorial.MaxCount = MaxCount;
            return Editorial;

        }
    }
}
