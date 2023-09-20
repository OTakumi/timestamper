using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeStamper
{
    [JsonObject]
    public class TextJson
    {
        [JsonProperty("text")]
        public string Text { get; set; } = string.Empty;
    }

    public class Message
    {
        private readonly string _workStatus;

        public Message(string workStatus)
        {
            _workStatus = workStatus;
        }

        /// <summary>
        /// Slackの指定のチャンネルに、"始業"、"終業"のスタンプをポストする
        /// </summary>
        public async void PostToSlack(string apiKey)
        {
            var slackMessage = new TextJson();

            if (_workStatus is "start")
            {
                slackMessage.Text = ":shigyo:";
            }
            else
            {
                slackMessage.Text = ":syugyo:";
            }

            var json = JsonConvert.SerializeObject(slackMessage);

            using (var client = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiKey, content);
            };
        }
    }
}
