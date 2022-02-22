using ApiRest.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRest.Models
{
    public class Author : BaseModel
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string FullName
        {
            get {
                    return FirstName + " " + LastName;
                }
        }
    }
}
