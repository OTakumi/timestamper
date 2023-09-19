using System.Runtime.Serialization;
using TimeStamper;

namespace TimeStamper.Test
{
    public class TimeStampTest
    {
        /// <summary>
        /// �ғ����Ԃ��Z�o�����������Ƃ��m�F����
        /// </summary>
        [Fact]
        public void CorrectCalculationElapsedTime()
        {
            var timeStamp = new TimeStamp();
            var startTime = new DateTime(2023, 4, 1, 12, 00, 00);
            var endTime = new DateTime(2023, 4, 1, 13, 00, 00);
            var elapsedTime = endTime - startTime;
            var expected = new TimeSpan(1, 0, 0);
            Assert.Equal(expected.ToString(), elapsedTime.ToString());
        }
    }
}