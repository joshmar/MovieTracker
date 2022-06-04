namespace MovieTracker.Extension;

public static class StringExtensions
{
    /// <summary>
    /// Uses string.IsNullOrEmpty to test the given string if it meets the condition.
    /// </summary>
    /// <param name="string">String to validate.</param>
    /// <returns>True if the passed string is null or empty.</returns>
    public static bool IsNullOrEmpty(this string @string) =>
        string.IsNullOrEmpty(@string);

    /// <summary>
    /// Uses string.IsNullOrWhiteSpace to test the given string if it meets the condition.
    /// </summary>
    /// <param name="string">String to validate.</param>
    /// <returns>True if the passed string is null, empty or contains white spaces only.</returns>
    public static bool IsNullOrWhiteSpace(this string @string) =>
        string.IsNullOrWhiteSpace(@string);
}