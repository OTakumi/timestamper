using System;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
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
            // Display title
            Console.WriteLine("=========================");
            Console.WriteLine("===== Timer Stamper =====");
            Console.WriteLine("=========================");

            // 設定情報をよみこむ
            var config = new Config();

            // 打刻ファイルがあるかチェックする
            var timeStampFile = new TimeStampFile();

            // 打刻
            var timeStamp = new TimeStamp();
            var newTimeStampRecord = timeStamp.NewRecord(timeStampFile.ReadData());

            Console.WriteLine("打刻時間: " + timeStamp.Time().ToString());
            Console.WriteLine();

            // 打刻ファイルを更新する
            timeStampFile.Write(newTimeStampRecord);

            // Slackに投稿する
            var message = new Message(timeStamp.WorkStatus());
            message.PostToSlack(config.ApiKey());

            Console.WriteLine("=========================");

            // 終了
            Console.ReadKey();
        }
    }
}