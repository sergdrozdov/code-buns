namespace CodeBuns.Dotnet.Common.Validators
{
    public interface IValidators
    {
        /// <summary>
        /// Validates the format of the specified email address.
        /// </summary>
        /// <param name="email">The email address to validate. Cannot be null or empty.</param>
        /// <returns><see langword="true"/> if the email address is in a valid format; otherwise, <see langword="false"/>.</returns>
        bool ValidateEmail(string email);
    }
}
