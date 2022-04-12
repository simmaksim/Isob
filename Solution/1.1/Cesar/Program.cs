using System;
using System.IO;


namespace Cesar
{
    class Cesar
    {
        static int[] ConvertationToIntArray(string tmp)
        {
            char[] s = new char[tmp.Length];
            int[] b = new int[s.Length];
            for (int i = 0; i < tmp.Length; i++)
            {

                s[i] = Convert.ToChar(tmp[i]);
                b[i] = s[i] - '0' - 48;
            }
            return b;
        }

        static string Scramlber(int[] tmp, int k)
        {
            char[] result = new char[tmp.Length];
            string re = null;
            for (int i = 0; i < tmp.Length; i++)
            {
                if (tmp[i] != -64)
                    tmp[i] += k; 
                if (tmp[i] > 26)
                    tmp[i] -= 26;
                result[i] = Convert.ToChar(tmp[i] + '0' + 48);
                re += result[i].ToString();
            }
            return re; 
        }

        static void Main(string[] args)
        {
            string path = @"C:\Users\Dell\Desktop\ISOB\Solution\1.1\tmp.txt";
            string keyPath = @"C:\Users\Dell\Desktop\ISOB\Solution\1.1\key.txt";
            string outPath = @"C:\Users\Dell\Desktop\ISOB\Solution\1.1\result.txt";
            int key;
            try
            {
                string str = null;
                string res = null;
                using (StreamReader sr = new StreamReader(path))
                    str = sr.ReadToEnd();
                using (StreamReader sr = new StreamReader(keyPath))
                    key = Convert.ToInt32(sr.ReadToEnd());
                int[] resInt = ConvertationToIntArray(str);
                res = Scramlber(resInt, key);
                using (StreamWriter sw = new StreamWriter(outPath, false, System.Text.Encoding.Default))
                    sw.WriteLine(res);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}