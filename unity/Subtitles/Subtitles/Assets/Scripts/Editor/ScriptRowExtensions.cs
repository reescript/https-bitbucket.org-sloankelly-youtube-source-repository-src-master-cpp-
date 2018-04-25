using System;

public static class ScriptRowExtensions
{
    public static VoiceOverLine English(this ScriptRow row)
    {
        return new VoiceOverLine()
        {
            key = row.key,
            line = row.en.Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
        };
    }

    public static VoiceOverLine French(this ScriptRow row)
    {
        return new VoiceOverLine()
        {
            key = row.key,
            line = row.fr.Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
        };
    }

    public static VoiceOverLine Russian(this ScriptRow row)
    {
        return new VoiceOverLine()
        {
            key = row.key,
            line = row.ru.Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
        };
    }
}
