using OrdersFiltration.Attributes.Translation;
using System.Text.Json.Serialization;

namespace OrdersFiltration.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum District
    {
        None,

        [Translation("Центр")]
        Centre,

        [Translation("Северный")]
        Severny,

        [Translation("Советский")]
        Sovetsky,

        [Translation("Октябрьский")]
        Oktyabrsky,

        [Translation("Набережный")]
        Naberezhny
    }

}
