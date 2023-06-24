namespace MeterGram.Core.Behaviours;

public static class ValidationErrorCodes
{
    public const String GreaterThanOrEqualTo = "GreaterThanOrEqualValidator";
    public const String NotEqualTo = "NotEqualValidator";
    public const String GreaterThan = "GreaterThanValidator";
    public const String LessThan = "LessThanValidator";
    public const String MaximumLength = "MaximumLengthValidator";
    public const String Predicate = "PredicateValidator";
    public const String AsyncPredicate = "AsyncPredicateValidator";
    public const String NotEmpty = "NotEmptyValidator";
    public const String Enum = "EnumValidator";
    public const String Email = "EmailValidator";

    #region Custom error codes
    public const String NotFound = "NotFoundValidator";
    public const String NotUnique = "NotUniqueValidator";
    public const String TooLong = "TooLongValidator";
    public const String NotAvailable = "NotAvailableValidator";
    public const String NotValidContent = "NotValidContent";
    #endregion
}
