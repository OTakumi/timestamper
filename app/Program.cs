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

            bool isStart = true;
            Console.WriteLine("始業または終業を入力してください。");
            Console.WriteLine("始業：s(start) | 終業：e(end)");
            var input = Console.ReadLine();
            Console.WriteLine(input);
            var apiUrl = "";

            // 始業、終業のスタンプを投稿する。
            // テキストの設定はそれぞれで
            var postMessage = new PostMessage();
            if (input == "s" || input == "start")
            {
                postMessage.ToSlack(apiUrl, ":shigyo:");
            }
            else if(input == "e" || input == "end") {
                isStart = false;
                postMessage.ToSlack(apiUrl, ":syugyo:");
            }
            
            Console.ReadKey();
        }
    }
}