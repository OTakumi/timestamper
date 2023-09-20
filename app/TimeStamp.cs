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
        private readonly string _workStatus;
        private readonly string[] _header = new[] { "#", "date", "start_time", "end_time", "total_work_time" };

        /// <summary>
        /// 打刻
        /// </summary>
        /// <param name="time"></param>
        public TimeStamp()
        {
            _workStatus = ReadCommand();
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
        public string WorkStatus() { return _workStatus; }

        private static string ReadCommand()
        {
            while (true)
            {
                Console.WriteLine("始業または、終業を入力してください。");
                Console.Write("入力値：");
                var timeStampTiming = Console.ReadLine();

                if (timeStampTiming is "s" or "S" or "start" or "Start")
                {
                    return "start";
                }
                else if (timeStampTiming is "e" or "E" or "end" or "End")
                {
                    return "end";
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
            string[] newTimeStampRecord = Enumerable.Repeat("" ,_header.Length).ToArray();

            if (_workStatus is "start")
            {
                newTimeStampRecord[Array.IndexOf(_header, "#")] = timeStampLog.Count.ToString();
                newTimeStampRecord[Array.IndexOf(_header, "date")] = _timeStamp.ToString("yyyy/MM/dd");
                newTimeStampRecord[Array.IndexOf(_header, "start_time")] = _timeStamp.ToString("HH:mm:ss");

                timeStampLog.Add(newTimeStampRecord);
            }

            if (_workStatus is "end")
            {
                // 打刻履歴ファイルの最終行を取得
                newTimeStampRecord = timeStampLog.Last();

                if (newTimeStampRecord[Array.IndexOf(_header, "start_time")] is not ""
                    && newTimeStampRecord[Array.IndexOf(_header, "end_time")] is "")
                {
                    // 終業時刻と経過時間を追加
                    newTimeStampRecord[Array.IndexOf(_header, "end_time")] = _timeStamp.ToString("HH:mm:ss");
                    newTimeStampRecord[Array.IndexOf(_header, "total_work_time")]
                        = (_timeStamp - DateTime.Parse(newTimeStampRecord[2])).ToString();

                    timeStampLog.RemoveAt(timeStampLog.Count - 1);
                    timeStampLog.Add(newTimeStampRecord);
                }
            }

            return timeStampLog;
        }
    }
}
