using OrdersFiltration.Attributes.Translation;
using OrdersFiltration.Enums;
using System.Reflection;

namespace OrdersFiltration.Tools
{
    public static class DistrictTools
    {
        private static readonly Lazy<Dictionary<string, string>> _translations = new Lazy<Dictionary<string, string>>(GetTranslations);

        public static District TryParse(string value)
        {
            if (!Enum.TryParse(value, true, out District _district) && _translations.Value.ContainsValue(value))
            {
                _district = Enum.Parse<District>(_translations.Value.FirstOrDefault(pair => pair.Value == value).Key);
            }

            return _district;
        }

        private static Dictionary<string, string> GetTranslations()
        {
            var dict = new Dictionary<string, string>();
            var enumItems = typeof(District).GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);

            foreach (var item in enumItems)
            {
                if (item.CustomAttributes.Count() != 0)
                    dict.Add(item.Name, item.GetCustomAttribute<TranslationAttribute>().Translation);
            }

            return dict;
        }
    }
}
