using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class Hash
    {
        public string hash(string value)
        {
            byte[] bytePassword = Encoding.ASCII.GetBytes(value);
            byte[] ByteHashPassword = Encrypt(ref bytePassword);
            string base64Password = Convert.ToBase64String(ByteHashPassword);
            return base64Password;
        }
        public byte[] Encrypt(ref byte[] password)
        {
            DateTime dateTime = DateTime.Now;
            //string code = dateTime.Hour.ToString() + dateTime.Second.ToString() + dateTime.Minute.ToString();
            string code = "mahdi";
            byte[] key = Encoding.ASCII.GetBytes(code);

            string ch = "";
            Byte[] s = new Byte[256];
            Byte[] k = new Byte[256];
            Byte temp;
            int i, j;
            for (i = 0; i < 256; i++)
            {
                s[i] = (Byte)i; k[i] = key[i % key.GetLength(0)];
            }
            //Console.WriteLine(key.GetLength(0).ToString());
            j = 0;
            for (i = 0; i < 256; i++)
            {
                j = (j + s[i] + k[i]) % 256;
                temp = s[i]; s[i] = s[j];
                s[j] = temp;
            }
            i = j = 0;
            for (int x = 0; x < password.GetLength(0); x++)
            {
                i = (i + 1) % 256;
                j = (j + s[i]) % 256;
                temp = s[i]; s[i] = s[j];
                s[j] = temp;
                int t = (s[i] + s[j]) % 256;
                password[x] ^= s[t];
                ch = ch + Convert.ToChar(password[x]);
            }
            //byte[] bytes1 = new byte[bytes.Length + key.Length];
            //for (int k1 = 0; k1 < key.Length; k1++)
            //{
            //    bytes1[bytes.Length + k1] = key[k1];
            //}
            //for (int k1 = 0; k1 < bytes.Length; k1++)
            //{
            //    bytes1[k1] = bytes[k1];
            //}
            return password;
        }

        public byte[] Decrypt(ref string text)
        {
            DateTime dateTime = DateTime.Now;
            string code = text.Substring(8, text.Length-8);
            text = text.Substring(0, 8);
            
            byte[] bytes = Encoding.ASCII.GetBytes(text);
            byte[] key = Encoding.ASCII.GetBytes(code);
            string ch = "";
            Byte[] s = new Byte[256];
            Byte[] k = new Byte[256];
            Byte temp;
            int i, j;
            for (i = 0; i < 256; i++)
            {
                s[i] = (Byte)i; k[i] = key[i % key.GetLength(0)];
            }
            //Console.WriteLine(key.GetLength(0).ToString());
            j = 0;
            for (i = 0; i < 256; i++)
            {
                j = (j + s[i] + k[i]) % 256;
                temp = s[i]; s[i] = s[j];
                s[j] = temp;
            }
            i = j = 0;
            for (int x = 0; x < bytes.GetLength(0); x++)
            {
                i = (i + 1) % 256;
                j = (j + s[i]) % 256;
                temp = s[i]; s[i] = s[j];
                s[j] = temp;
                int t = (s[i] + s[j]) % 256;
                bytes[x] ^= s[t];
                ch = ch + Convert.ToChar(bytes[x]);
            }
            
            return bytes;
        }


    }
}
