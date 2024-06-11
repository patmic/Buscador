using System.Security.Cryptography;
using System.Text;

namespace WebApp.Service
{
    public class Md5Service : IMd5Service
    {
        public string GenerateMd5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] data = Encoding.UTF8.GetBytes(input);
                byte[] hash = md5.ComputeHash(data);
                
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}