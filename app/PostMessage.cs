using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeStamper
{
    [JsonObject]
    public class Slack
    {
        [JsonProperty("text")]
        public string message { get; set; } = string.Empty;
    }

    /// <summary>
    /// Slackの指定のチャンネルに、"始業"、"終業"をポストする
    /// </summary>
    public class PostMessage
    {
        public async void ToSlack(string apiKey, string messageBody)
        {
            Slack slack = new Slack()
            {
                message = messageBody,
            };

            var json = JsonConvert.SerializeObject(slack);

            using (var client = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiKey, content);
            };
        }
    }
}
