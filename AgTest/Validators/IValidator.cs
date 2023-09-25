namespace Ag.Web.Validators
{
    public interface IValidator<T> where T : class
    {
        (bool IsValid, string ErrorMessage) Validate(T value);
    }
}
