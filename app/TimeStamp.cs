using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using timestamp_to_slack.Models;

namespace TimeStamper
{
    internal class TimeStamp
    {
        /// <summary>
        /// 始業時刻を記録する
        /// </summary>
        /// <param name="time"></param>
        public string StartTime(DateTime time)
        {
            var startTime = CurrentTime();

            var rslt = "Start time stamped. 始業打刻しました。";
            
            return rslt;
        }

        /// <summary>
        /// 終業時刻を記録する
        /// </summary>
        /// <param name="time"></param>
        public string EndTime(DateTime time)
        {
            // 終業時間を記録する。
            var EndTime = CurrentTime();

            var rslt = "End time stamped. 終業打刻しました。";
            return rslt;
        }

        /// <summary>
        /// 当日の日付、現在時刻を返す
        /// </summary>
        /// <returns></returns>
        private TimeStampModel CurrentTime()
        {
            var timeStamp = new TimeStampModel();

            timeStamp.Today = DateTime.Now.ToString("yyyy/MM/dd");
            timeStamp.Time = DateTime.Now.ToString("HH:mm:ss");

            return timeStamp;
        }
    }
}
