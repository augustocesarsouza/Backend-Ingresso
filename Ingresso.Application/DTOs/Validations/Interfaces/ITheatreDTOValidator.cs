namespace Ingresso.Application.DTOs.Validations.Interfaces
{
    public interface ITheatreDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(TheatreDTO theatreDTO);
    }
}
