using System.ComponentModel.DataAnnotations;

namespace ApiRest.DTOS.Requests.Validations
{
    public class MaxCountEditorialValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }
            int getal;
            if (int.TryParse(value.ToString(), out getal))
            {

                if (getal == 0)
                    return false;

                if (getal > 0)
                    return true;
                if (getal == -1)
                    return true;
            }
            return false;

        }
    }
}
