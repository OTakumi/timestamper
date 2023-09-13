using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeStamper
{
    internal class TimeStampFile
    {
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
            var lineNumber = 0;

            // ファイルが存在しない場合、作成する。
            if (!File.Exists(filePath))
            {
                var arr = new[] { "#", "UserName", "Date", "StartTime", "EndTime" };
                var csvHeader = string.Join(",", arr);
                using (StreamWriter csvWriter = File.CreateText(filePath))
                {
                    csvWriter.WriteLine(csvHeader);
                }
                rslt = "Time stamp file is created. ";
            }
            else
            {
                // 既存のCSVファイルの行数を取得する。
                lineNumber = File.ReadAllLines(filePath).Length;
            }

            return rslt;
        }
    }
}
