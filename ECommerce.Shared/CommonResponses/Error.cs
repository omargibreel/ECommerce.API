namespace ECommerce.Shared.CommonResponses
{
    public record Error(string Code, string Description, ErrorType ErrorType)
    {
        public static Error Failure(
            string code = "General.Failure",
            string description = "A general failure has occurred"
        ) =>
            new(code, description, ErrorType.Failure);

        public static Error Validation(
            string code = "General.Validation",
            string description = "Validation error has occurred"
        ) =>
            new(code, description, ErrorType.Validation);

        public static Error NotFound(
            string code = "General.NotFound",
            string description = "The requested resource was not found"
        ) =>
            new(code, description, ErrorType.NotFound);

        public static Error Unauthorized(
            string code = "General.Unauthorized",
            string description = "You are not authorized to perform this action"
        ) =>
            new(code, description, ErrorType.Unauthorized);

        public static Error Forbidden(
            string code = "General.Forbidden",
            string description = "You don't have access to this resource. Access denied"
        ) =>
            new(code, description, ErrorType.Forbidden);

        public static Error InvalidCredentials(
            string code = "General.InvalidCredentials",
            string description = "Your credentials are not valid to reach this resource"
        ) =>
            new(code, description, ErrorType.InvalidCredentials);
    }
}