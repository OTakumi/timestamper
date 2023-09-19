using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeStamper
{
    /// <summary>
    /// 打刻データを生成する
    /// </summary>
    public class TimeStamp
    {
        private readonly DateTime _timeStamp;
        private readonly string _timeStampTiming;
        private readonly string[] _header = new[] { "#", "Date", "StartTime", "EndTime", "TotalWorkTime" };

        /// <summary>
        /// 打刻
        /// </summary>
        /// <param name="time"></param>
        public TimeStamp()
        {
            _timeStampTiming = ReadCommand();
            _timeStamp = DateTime.Now;
        }

        /// <summary>
        /// 打刻時間を返す
        /// </summary>
        /// <returns>打刻時刻</returns>
        public DateTime Time() { return _timeStamp; }
        
        /// <summary>
        /// その打刻が、始業なのか終業なのかを返す
        /// </summary>
        /// <returns></returns>
        public string Timing() { return _timeStampTiming; }

        private static string ReadCommand()
        {
            while (true)
            {
                Console.WriteLine("始業または、終業を入力してください。");
                Console.Write("入力値：");
                var timeStampTiming = Console.ReadLine();

                if (timeStampTiming is "s" or "S" or "start" or "Start")
                {
                    return "StartTime";
                }
                else if (timeStampTiming is "e" or "E" or "end" or "End")
                {
                    return "EndTime";
                }
                else
                {
                    Console.WriteLine("不正な入力です。");
                    Console.WriteLine();
                }
            }
        }

        public List<string[]> NewRecord(List<string[]> timeStampLog)
        {
            string[] newTimeStampRecord = new string[_header.Length];

            if (_timeStampTiming is "StartTime")
            {
                newTimeStampRecord[0] = timeStampLog.Count.ToString();
                newTimeStampRecord[1] = _timeStamp.ToString("yyyy/MM/dd");
                newTimeStampRecord[2] = _timeStamp.ToString("HH:mm:ss");

                timeStampLog.Add(newTimeStampRecord);
            }

            if (_timeStampTiming is "EndTime")
            {
                // 打刻履歴ファイルの最終行を取得
                newTimeStampRecord = timeStampLog.Last();

                if (newTimeStampRecord[2] is not "" && newTimeStampRecord[3] is "")
                {
                    // 終業時刻と経過時間を追加
                    newTimeStampRecord[3] = _timeStamp.ToString("HH:mm:ss");
                    newTimeStampRecord[4] = (_timeStamp - DateTime.Parse(newTimeStampRecord[2])).ToString();

                    timeStampLog.RemoveAt(timeStampLog.Count - 1);
                    timeStampLog.Add(newTimeStampRecord);
                }
            }

            return timeStampLog;
        }
    }
}
