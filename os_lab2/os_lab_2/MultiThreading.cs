using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace os_lab_2
{
    class MultiThreading
    {
        internal static void BruteHash(string hash)
        {
            DateTime start = DateTime.Now;
            bool flag = false;
            Parallel.For(0, 26, a =>
            {
                byte[] password = new byte[5];
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
                                string hashed = Hash.GetHash(passwordString);
                                if (hash != hashed) continue;

                                Console.WriteLine($"Найден пароль {passwordString}, hash {hashed}");
                                Console.WriteLine(DateTime.Now - start);
                                flag = true;
                                break;
                            }

                            if (flag) break;
                        }

                        if (flag) break;
                    }

                    if (flag) break;
                }
            });
        }

        internal static void BruteHashFromFile()
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
