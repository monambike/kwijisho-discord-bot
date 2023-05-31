using Newtonsoft.Json;

namespace KWIJisho
{
    internal struct ConfigJson
    {
        [JsonProperty("token")]
        internal string Token { get; set; }

        [JsonProperty("prefix")]
        internal string Prefix { get; set; }

        [JsonProperty("activity")]
        internal string Activity { get; set; }
    }
}
