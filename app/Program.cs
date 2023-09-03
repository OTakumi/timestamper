using System;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;

namespace TimeStamper
{
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

            // 始業、終業のスタンプを投稿する。
            // テキストの設定はそれぞれで
            if (input == "s" || input == "start")
            {
                PostSlack(config.ApiUrl, ":shigyo:");
            }
            else if(input == "e" || input == "end") {
                isStart = false;
                PostSlack(config.ApiUrl, ":syugyo:");
            }
            
            Console.WriteLine(WriteCSV(isStart, config.UserName, config.TimeStampFilePath));
            Console.ReadKey();
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