namespace SimplePostgresConnector.Common;

public static class Extensions
{
    /// <summary>
    /// Used to enforce PostgreSQL timestamp format correctness
    /// </summary>
    /// <param name="dateTime">The DateTime to enforce UTC time against</param>
    /// <returns>The DateTime with UTC time configured</returns>
    public static DateTime? SetDateTimeKindToUtc(this DateTime? dateTime)
    {
        if (dateTime.HasValue)
        {
            return dateTime.Value.SetDateTimeKindToUtc();
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Used to enforce PostgreSQL timestamp format correctness
    /// </summary>
    /// <param name="dateTime">The DateTime to enforce UTC time against</param>
    /// <returns>The DateTime with UTC time configured</returns>
    public static DateTime SetDateTimeKindToUtc(this DateTime dateTime)
    {
        if (dateTime.Kind == DateTimeKind.Utc) { return dateTime; }
        return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
    }
}
