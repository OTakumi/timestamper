using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeStamper
{
    /// <summary>
    /// 打刻ファイル
    /// </summary>
    public class TimeStampFile
    {
        private readonly string _filePath;
        private readonly string basePath = "./TimeStamp/";
        private readonly string fileName = "TimeStamp.csv";
        private readonly string[] _header = new[] { "#", "Date", "StartTime", "EndTime", "TotalWorkTime" };

        public TimeStampFile()
        {
            _filePath = SetFilePath(basePath, fileName);
            IsExist(_header);
        }

        // public string FilePath() { return _filePath; }

        private static string SetFilePath(string fileDir, string fileName)
        {
            var today = DateTime.Now;
            fileName = today.ToString("yyyyMM") + "_" + fileName;
            var filePath = Path.Combine(fileDir, fileName);

            if (!Directory.Exists(fileDir)) { Directory.CreateDirectory(fileDir); }

            return filePath;
        }

        /// <summary>
        /// ファイルが存在することを確認し、ファイルが存在しない場合は作成する。ファイルが既に存在する場合、書き込み可能な状態か確認する
        /// </summary>
        private void IsExist(string[] header)
        {
            if (!File.Exists(_filePath))
            {
                CreateFile();
                WriteHeader(header);
            }
        }

        /// <summary>
        /// ファイルを作成する。
        /// </summary>
        private void CreateFile()
        {
            using FileStream fs = File.Create(_filePath);
        }

        /// <summary>
        /// ヘッダーを追加する
        /// </summary>
        /// <param name="filePath"></param>
        private void WriteHeader(string[] header)
        {
            var csvHeader = string.Join(",", header);

            try
            {
                using StreamWriter sw = new(_filePath, false, Encoding.UTF8);
                sw.WriteLine(csvHeader);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public List<string[]> ReadData()
        {
            List<string[]> lines = new List<string[]>();

            // 既存のCSVファイルの行数を取得する。
            try
            {
                using StreamReader sr = new StreamReader(_filePath, Encoding.UTF8);

                while (!sr.EndOfStream)
                {
                    string? csvLine = sr.ReadLine();

                    if (csvLine != null)
                        lines.Add(csvLine.Split(','));
                }
            } catch (Exception ex) { Console.WriteLine(ex); }
            
            return lines;
        }

        /// <summary>
        /// 打刻を記録する。
        /// </summary>
        /// <param name="timeStampDataList"></param>
        /// <returns></returns>
        public void Write(List<string[]> timeStampDataList)
        {
            try
            {
                using StreamWriter sw = new(_filePath, false, Encoding.UTF8);

                foreach (var timeStampDataLine in timeStampDataList)
                {
                    sw.WriteLine(string.Join(',', timeStampDataLine));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            Console.WriteLine("打刻しました。");
        }
    }
}
