using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace os_lab_2
{
    class SingleThread
    { 
        internal static void CalculateHash(string hashOut)
        {
            DateTime start = DateTime.Now;
            int length = Data.alphabet.Length;
            for (int ch1 = 0; ch1 < length; ch1++)
            {
                string a = Convert.ToString(Data.alphabet[ch1]);
                for (int ch2 = 0; ch2 < length; ch2++)
                {
                    string b = Convert.ToString(Data.alphabet[ch2]);
                    for (int ch3 = 0; ch3 < length; ch3++)
                    {
                        string c = Convert.ToString(Data.alphabet[ch3]);
                        for (int ch4 = 0; ch4 < length; ch4++)
                        {
                            string d = Convert.ToString(Data.alphabet[ch4]);
                            for (int ch5 = 0; ch5 < length; ch5++)
                            {
                                string e = Convert.ToString(Data.alphabet[ch5]);

                                string password = a + b + c + d + e;
                                string hashIn = Hash.GetHash(password);
                                //Console.WriteLine(hashIn);
                                //Console.WriteLine(hashOut);

                                if (hashOut == hashIn)
                                {
                                    Console.WriteLine($"Найденный пароль: {password} ");
                                    Console.WriteLine($"Хэш: {hashIn}");
                                    Console.WriteLine(DateTime.Now - start);
                                    ch1 = ch2 = ch3 = ch4 = ch5 = length;
                                }
                            }
                        }
                    }
                }
            }
        }

        internal static void CalculateHashFromFile()
        {
            DateTime start = DateTime.Now;
            int length = Data.alphabet.Length;
            int count = 0;
            string[] readHash = File.ReadAllLines(Data.pathFile);
            for (int ch1 = 0; ch1 < length; ch1++)
            {
                string a = Convert.ToString(Data.alphabet[ch1]);
                for (int ch2 = 0; ch2 < length; ch2++)
                {
                    string b = Convert.ToString(Data.alphabet[ch2]);
                    for (int ch3 = 0; ch3 < length; ch3++)
                    {
                        string c = Convert.ToString(Data.alphabet[ch3]);
                        for (int ch4 = 0; ch4 < length; ch4++)
                        {
                            string d = Convert.ToString(Data.alphabet[ch4]);
                            for (int ch5 = 0; ch5 < length; ch5++)
                            {
                                string e = Convert.ToString(Data.alphabet[ch5]);
                                string password = a + b + c + d + e;
                                string hash = Hash.GetHash(password);

                                foreach (string s in readHash)
                                {
                                    //Console.WriteLine(s);
                                    //Console.WriteLine(hash);
                                    if (!s.ToUpper().Contains(hash)) continue;

                                    Console.WriteLine($"Найденный пароль: {password}");
                                    Console.WriteLine($"Хеш: {hash}");
                                    Console.WriteLine(DateTime.Now - start);
                                    count++;
                                    break;
                                }

                                if (count == readHash.Length)
                                {
                                    ch1 = ch2 = ch3 = ch4 = ch5 = length;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
