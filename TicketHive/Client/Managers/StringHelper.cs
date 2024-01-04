namespace TicketHive.Client.Managers;

public static class StringHelper
{
    public static string ReplaceUnderScoreWithSpace(string input)
    {
        return input.Replace('_', ' ');
    }
}
