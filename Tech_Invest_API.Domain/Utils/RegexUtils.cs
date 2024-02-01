using System.Text.RegularExpressions;

namespace Tech_Invest_API.Domain.Utils;

public class RegexUtils
{
    private static bool ValidaPadrao(string? input, string pattern)
    {
        var regex = new Regex(pattern);
        return input is not null && regex.IsMatch(input);
    }

    public static bool ValidaFormatoEmail(string? email)
    {
        string pattern = @"^[a-zA-Z][a-zA-Z0-9._]*@[a-zA-Z]+(\.[a-zA-Z]+)+$";
        return ValidaPadrao(email, pattern);
    }
}
