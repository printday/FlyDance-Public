using System.Security.Cryptography;

namespace Common.Utils
{
    public class CodeUtil
    {
        /// <summary>
        /// 生成6位验证码
        /// </summary>
        /// <returns></returns>
        public static string GetCodeSix()
        {
            byte[] randomNumberBytes = new byte[4];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumberBytes);
            }
            uint randomNumber = BitConverter.ToUInt32(randomNumberBytes, 0) % 900000 + 100000;
            return randomNumber.ToString();
        }
    }
}
