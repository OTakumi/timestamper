using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace timestamp_to_slack
{
    [JsonObject]
    public class Config
    {
        [JsonProperty("user_Name")]
        public string? UserName { get; set; } = string.Empty;
        [JsonProperty("time_Stamp_File_Path")]
        public string? TimeStampFilePath { get; set; }
        [JsonProperty("api_Url")]
        public string ApiUrl { get; set; }
    }

    public class InitConfig
    {
        /// <summary>
        /// config.jsonから打刻ファイルのパスを取得する。
        /// </summary>
        /// <returns></returns>
        public static Config GetConfig()
        {
            var configFilePath = "./config.json";
            Config config = new Config()
            {
                TimeStampFilePath = "./Dakoku.csv",
            };

            if (!File.Exists(configFilePath))
            {
                Console.WriteLine("打刻ファイルを作成します。");
                Console.WriteLine("ユーザー名を入力してください。");
                config.UserName = Console.ReadLine();
                Console.WriteLine("Slack Webhool URLを入力してください。");
                config.ApiUrl = Console.ReadLine();

                using (StreamWriter file = File.CreateText(configFilePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, config);
                }
            }
            else
            {
                var jsonString = File.ReadAllText(configFilePath);
                var configJson = JsonConvert.DeserializeObject<Config>(jsonString);

                if (configJson != null)
                {
                    config = configJson;
                }
            }

            Console.WriteLine("File path: " + config.TimeStampFilePath);
            return config;
        }
    }
}
