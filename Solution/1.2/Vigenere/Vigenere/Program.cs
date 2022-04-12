using System;
using System.IO;

namespace Vigenere
{
    class Vigenere
    {
        const int n = 26;

        static char[,] BuildSquare()
        {
            char[,] square = new char[n, n];
            char first = 'a';
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    square[i, j] = first;
                    first++;
                    if (first == 'z' + 1)
                    {
                        first = 'a';
                    }
                }
                first = (char)('a' + i + 1);
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(square[i, j] + " ");
                }
                Console.WriteLine("\n");
            }
            return square;
        }

        static int[] ConvertationToIntArray(string tmp)
        {
            char[] s = new char[tmp.Length];
            int[] b = new int[s.Length];
            for (int i = 0; i < tmp.Length; i++)
            {

                s[i] = Convert.ToChar(tmp[i]);
                b[i] = s[i] - '0' - 49;
            }
            return b;
        }
        static void Main(string[] args)
        {
            char[,] square = BuildSquare();
            string path = @"C:\Users\Dell\Desktop\ISOB\Solution\1.2\Input.txt";
            string keyPath = @"C:\Users\Dell\Desktop\ISOB\Solution\1.2\KeyWord.txt";
            string outPath = @"C:\Users\Dell\Desktop\ISOB\Solution\1.2\Result.txt";
            try
            {
                string str = null, res = null, key = null;
                using (StreamReader sr = new StreamReader(path))
                    str = sr.ReadToEnd();
                using (StreamReader sr = new StreamReader(keyPath))
                    key = sr.ReadToEnd();
                if (str.Length != key.Length)
                {
                    throw new Exception("Incorrect input");
                }
                int[] InputInt = ConvertationToIntArray(str);
                int[] KeyInt = ConvertationToIntArray(key);
                for (int i = 0; i < str.Length; i++)
                    res += square[InputInt[i], KeyInt[i]];
                using (StreamWriter sw = new StreamWriter(outPath, false, System.Text.Encoding.Default))
                    sw.WriteLine(res);
                Console.WriteLine(res);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
    }
}
