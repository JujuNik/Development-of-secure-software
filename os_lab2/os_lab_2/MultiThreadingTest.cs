using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace os_lab_2
{
    class MultiThreadingTest
    {
        internal static void CalculateHash(string hashOut)
        {
            DateTime start = DateTime.Now;
            bool flag = false;
            int length = Data.alphabet.Length;
            Parallel.For(0, length, p =>
            {
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
                                        //ch1 = ch2 = ch3 = ch4 = ch5 = length;
                                        flag = true;
                                        break;
                                        
                                    }
                                }
                                if (flag) break;
                            }
                            if (flag) break;
                        }
                        if (flag) break;

                    }
                    if (flag) break;
                }
            });
        }


        internal static void CalculateHashFromFile()
        {
            bool flag = false;
            DateTime start = DateTime.Now;
            Parallel.For(0, 26, a =>
            {
                byte[] password = new byte[5];
                int count = 0;
                password[0] = (byte)(97 + a);
                for (password[1] = 97; password[1] < 123; password[1]++)
                {
                    for (password[2] = 97; password[2] < 123; password[2]++)
                    {
                        for (password[3] = 97; password[3] < 123; password[3]++)
                        {
                            for (password[4] = 97; password[4] < 123; password[4]++)
                            {
                                string passwordString = Encoding.ASCII.GetString(password);
                                string hash = Hash.GetHash(passwordString);
                                foreach (string line in File.ReadLines(Data.pathFile, Encoding.Default))
                                {
                                    if (!line.ToUpper().Contains(hash)) continue;

                                    Console.WriteLine($"Найден пароль {passwordString}, hash {hash}");
                                    Console.WriteLine(DateTime.Now - start);
                                    count++;
                                    if (count == File.ReadAllLines(Data.pathFile).Length) flag = true;
                                    break;
                                }

                                if (flag) break;
                            }

                            if (flag) break;
                        }

                        if (flag) break;
                    }

                    if (flag) break;
                }
            });
        }
    }
}
