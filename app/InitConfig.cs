using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TimeStamper
{
    [JsonObject]
    public class ConfigInfoJson
    {
        [JsonProperty("api_Url")]
        public string ApiKey { get; set; }
    }

    public class Config
    {
        private readonly string _apiKey;
        private readonly string configFileDir = "./";
        private readonly string configFileName = "config.json";

        public Config()
        {
            string configFilePath = Path.Combine(configFileDir, configFileName);

            FileIsExist(configFilePath);
            ConfigInfoJson configInfo = GetConfigInfo(configFilePath);

            _apiKey = configInfo.ApiKey;
        }

        public string ApiKey() { return _apiKey; }

        /// <summary>
        /// 設定ファイルがあるか確認し、無ければ作成する。
        /// </summary>
        private void FileIsExist(string configFilePath)
        {
            if (string.IsNullOrWhiteSpace(configFilePath))
            {
                Console.WriteLine("設定ファイルパスが不正です。");
                throw new ArgumentNullException(nameof(configFilePath));
            }

            if (!File.Exists(configFilePath))
            {
                Console.WriteLine("設定ファイルを作成します。");
                CreateConfigFile(configFilePath);
                Update(configFilePath);
            }
        }

        /// <summary>
        /// 設定ファイルを作成する。
        /// </summary>
        /// <param name="configFilePath"></param>
        private static void CreateConfigFile(string configFilePath)
        {
            using FileStream fs = File.Create(configFilePath);
        }

        /// <summary>
        /// 設定ファイルの内容を更新する。
        /// </summary>
        public void Update(string configFilePath)
        {
            // 更新情報を受け付ける
            var configInfo = new ConfigInfoJson();
            Console.WriteLine("設定ファイルを作成します。");
            Console.WriteLine("Slack Webhool URLを入力してください。");
            Console.Write("URL：");
            var apiKey = Console.ReadLine();
            if(apiKey is not null)
            {
                configInfo.ApiKey = apiKey;
            }

            // 設定ファイルを更新する。
            try
            {
                using (var sw = new StreamWriter(configFilePath, false, Encoding.UTF8))
                {
                    var jsonData = JsonConvert.SerializeObject(configInfo);
                    sw.Write(jsonData);
                }

                Console.WriteLine("設定ファイルが更新されました。");
            }
            catch (Exception ex)
            {
                Debug.Write($"failed: {ex.Message}");
            }
        }

        /// <summary>
        /// 設定ファイルから設定情報を取得する。
        /// </summary>
        /// <returns name="Info">設定内容</returns>
        private static ConfigInfoJson GetConfigInfo(string configFilePath)
        {
            var jsonString = File.ReadAllText(configFilePath);
            var configJson = JsonConvert.DeserializeObject<ConfigInfoJson>(jsonString);

            return configJson;
        }
    }
}
