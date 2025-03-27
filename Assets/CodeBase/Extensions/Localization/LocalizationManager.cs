using System.Collections.Generic;

namespace Extensions.Localization
{
    public static class LocalizationManager
    {
        private static readonly Dictionary<string, string> _localizedTexts = new Dictionary<string, string>
        {
            { LocalizationKeys.CUBE_THROWN_MESSAGE, "<color=red>The cube is thrown out!</color>" },
            { LocalizationKeys.CUBE_MISSING_MESSAGE, "<color=red>The cube is missing!</color>" },
            { LocalizationKeys.LIMIT_HAS_BEEN_EXCEEDED, "<color=red>Screen height limit exceeded!</color>" },
            { LocalizationKeys.CUBE_IS_INSTALLED, "<color=#FF00FF>The cube is installed</color>" },
            { LocalizationKeys.COLOR_DOES_NOT_MATCH, "<color=red>The color of the cube does not match the color of the previous one!</color>" },
            { LocalizationKeys.SAVE_TOWER_MESSAGE, "<color=#FF00FF>Save tower!</color>" }
        };

        public static string GetText(string key)
        {
            return _localizedTexts.TryGetValue(key, out var value) ? value : key;
        }
    }
}
