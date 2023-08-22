using System;

namespace API
{
    /// <summary>
    /// �����ṩʱ�����DateTime���ת����
    /// </summary>
    public static class TimestampConverter
    {
        /// <summary>
        /// ��DateTimeת��Ϊʱ�����
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long DateTimeToTimestamp (DateTime dateTime)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan timeSpan = dateTime.ToUniversalTime() - unixEpoch;
            return (long)timeSpan.TotalSeconds;
        }

        /// <summary>
        /// ��ʱ���ת��ΪDateTime��
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime TimestampToDateTime (long timestamp)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return unixEpoch.AddSeconds(timestamp).ToLocalTime();
        }
    }
}