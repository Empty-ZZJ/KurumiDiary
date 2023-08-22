using System;
using System.Security.Cryptography;
using System.Text;

namespace API
{
    public static class EncodingAction
    {
        public static string GetMd5Hash (string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // �������ַ���ת��Ϊ�ֽ����鲢���� MD5 ��ϣֵ
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // ����һ�� StringBuilder �����Ա�洢��ϣ�������
                StringBuilder sBuilder = new StringBuilder();

                // ������ϣ���ݵ�ÿ���ֽڲ������ʽ��Ϊʮ�������ַ���
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // ���ظ�ʽ����� MD5 ��ϣֵ
                return sBuilder.ToString();
            }
        }

        /// <summary>
        /// ����һ���ַ���������
        /// </summary>
        /// <param name="_num"></param>
        /// <returns></returns>
        public static int GetNum (string _num)
        {
            int _temp;
            int.TryParse(_num, out _temp);
            return _temp;
        }

        /// <summary>
        /// �����ַ�����sha256ֵ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetShA256 (string input)
        {
            return GetMd5Hash(input);
            //�����Ȳ�ʹ��sha256
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}