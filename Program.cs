using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;

namespace TimeStamptoSlack
{
    public class TimeStamp
    {
        public int Index { get; set; } = 0;
        public string UserName { get; set; } = string.Empty;
        public string Today { get; set; } = DateTime.Today.ToString("yyyy/MM/dd");
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
    }

    public class Config
    {
        [JsonProperty("user_Name")]
        public string? UserName { get; set; } = string.Empty;
        [JsonProperty("time_Stamp_File_Path")]
        public string? TimeStampFilePath { get; set; }
        [JsonProperty("api_Url")]
        public string? ApiUrl { get; set; }
    }

    class Program
    {
        /// <summary>
        /// 稼働開始と、終了時の時間を記録し、Slackに投稿する。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var currentTime = DateTime.Now.ToString("HH:mm:ss");

            var config = GetConfig();

            bool isStart = true;
            Console.WriteLine("始業または終業を入力してください。");
            Console.WriteLine("始業：s(start) | 終業：e(end)");
            var input = Console.ReadLine();
            Console.WriteLine(input);

            if (input != "s" && input != "start")
            {
                isStart = false;
            }
            
            Console.WriteLine(WriteCSV(isStart, config.UserName, config.TimeStampFilePath));
            Console.ReadKey();
        }

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

        /// <summary>
        /// SlackのIn coming webhookを使用し、任意のチャンネルに、始業、終業の投稿を行う。
        /// </summary>
        /// <returns></returns>
        static string PostSlack()
        {
            return "";
        }

        /// <summary>
        /// CSVファイルに打刻を記録する。
        /// </summary>
        /// <param name="isStart"></param>
        /// <param name="userName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        static string WriteCSV(bool isStart, string userName, string filePath)
        {
            var rslt = "";
            TimeStamp timeStamp = new TimeStamp()
            {
                UserName = userName,
                Today = DateTime.Today.ToString("yyyy/MM/dd"),
            };

            // ファイルが存在しない場合、作成する。
            if (!File.Exists(filePath))
            {
                var arr = new[] { "#", "UserName", "Date", "StartTime", "EndTime" };
                var csvHeader = string.Join(",", arr);
                using (StreamWriter csvWriter = File.CreateText(filePath))
                {
                    csvWriter.WriteLine(csvHeader);
                }
                timeStamp.Index = 1;
                rslt = "Time stamp file is created. ";
            }
            else
            {
                // 既存のCSVファイルの行数を取得する。
                timeStamp.Index = File.ReadAllLines(filePath).Length;
            }

            if (isStart)
            {
                // 始業時間を記録する。
                timeStamp.StartTime = DateTime.Now.ToString("HH:mm:ss");
                var timeStampArr = new[] {
                    timeStamp.Index.ToString(),
                    timeStamp.UserName,
                    timeStamp.Today,
                    timeStamp.StartTime,
                    timeStamp.EndTime,
                };
                var timeStampCsv = string.Join(",", timeStampArr);
                using (StreamWriter writer = new StreamWriter(filePath, true, Encoding.UTF8))
                {
                    writer.WriteLine(timeStampCsv);
                }
                Console.WriteLine(timeStampCsv);

                rslt = "Start time stamped. 始業打刻しました。";
            }
            else
            {
                // 終業時間を記録する。
                timeStamp.EndTime = DateTime.Now.ToString("HH:mm:ss");
                var timeStampArr = new[] {
                    timeStamp.Index.ToString(),
                    timeStamp.UserName,
                    timeStamp.Today,
                    timeStamp.StartTime,
                    timeStamp.EndTime,
                };
                var timeStampCsv = string.Join(",", timeStampArr);

                using (StreamWriter writer = new StreamWriter(filePath, true, Encoding.UTF8))
                {
                    writer.WriteLine(timeStampCsv);
                }
                Console.WriteLine(timeStampCsv);

                rslt = "End time stamped. 終業打刻しました。";
            }

            return rslt;
        }
    }
}