using System;
using System.Collections.Generic;
using System.Linq;
namespace minimizedfa
{
    class Program
    {
        static bool checkfinalstate(string[] finalstate, string x)
        {
            for (int i = 0; i < finalstate.Length; i++)
            {
                if (finalstate[i] == x)
                {
                    return true;
                }
            }
            return false;
        }

        static bool reachable(string[,] rules, string x, int rulesnum)
        {
            for (int i = 0; i < rulesnum; i++)
            {
                if (rules[i, 2] == x)
                {
                    return true;
                }
            }
            return false;
        }

        static bool twoequalstate(string[,] numstate, string[,] rules, string[] alphabet, int numstate1, int k, int j, int rulesnum)
        {
            for (int l = 0; l < alphabet.Length; l++)
            {
                bool t = checkequalalphabet(alphabet[l], numstate, numstate1, rules, k, j, rulesnum);
                if (t == false)
                {
                    return false;
                }

            }
            return true;
        }
        static bool checkequalalphabet(string x, string[,] numstate, int num, string[,] rules, int k, int j, int rulesnum)
        {
            string x1 = "";
            string x2 = "";
            //Console.WriteLine(k + " "+ j);
            for (int h = 0; h < rulesnum; h++)
            {
                if (rules[h, 0] == numstate[k, 0] && rules[h, 1] == x)
                {
                    x1 = rules[h, 2];
                }
                if (rules[h, 0] == numstate[j, 0] && rules[h, 1] == x)
                {
                    x2 = rules[h, 2];
                }
            }

            string y1 = " ";
            string y2 = "  g";
            for (int h = 0; h < num; h++)
            {
                if (numstate[h, 0] == x1)
                {
                    y1 = numstate[h, 1];
                }
                if (numstate[h, 0] == x2)
                {
                    y2 = numstate[h, 1];
                }
            }
            if (y1 == y2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void Main(string[] args)
        {
            string s1 = Console.ReadLine();
            s1 = s1.Substring(1, s1.Length - 2);
            string[] state = s1.Split(',');

            string s2 = Console.ReadLine();
            s2 = s2.Substring(1, s2.Length - 2);
            string[] alphabet = s2.Split(',');

            string s3 = Console.ReadLine();
            s3 = s3.Substring(1, s3.Length - 2);
            string[] finalstate = s3.Split(',');

            int n = int.Parse(Console.ReadLine());
            string[,] rules = new string[n, 3];
            for (int i = 0; i < n; i++)
            {
                string[] s4 = Console.ReadLine().Split(',');
                rules[i, 0] = s4[0];
                rules[i, 1] = s4[1];
                rules[i, 2] = s4[2];
            }

            List<string> a = new List<string>();
            List<string> b = new List<string>();
            for (int i = 1; i < state.Length; i++)
            {
                a = state.ToList();
                bool t = reachable(rules, state[i], n);
                if (t == false)
                {
                    b.Add(state[i]);
                }
            }
            for (int i = 0; i < b.Count; i++)
            {
                a.Remove(b[i]);
            }

            state = a.ToArray();

            string[,] numstate = new string[state.Length, 2];
            string[,] numstate2 = new string[state.Length, 2];
            bool g = true;
            for (int i = 0; i < state.Length; i++)
            {
                numstate[i, 0] = state[i];
                numstate2[i, 0] = state[i];
                if (checkfinalstate(finalstate, state[i]) == true)
                {
                    numstate[i, 1] = "2";
                    numstate2[i, 1] = "2";
                }
                else
                {
                    g = false;
                    numstate[i, 1] = "1";
                    numstate2[i, 1] = "1";
                }
            }



            int num = 2;
            int y = 0;
            //Console.WriteLine(twoequalstate(numstate2, rules, alphabet, state.Length, 2, 3, n));
            if (g == true)
            {
                num = 1;
            }
            else
            {
                num = 2;
            }
            while (num != y)
            {
                y = num;
                for (int i = 0; i < state.Length; i++)
                {
                    string x = numstate[i, 1];
                    bool t = false;
                    bool m = false;
                    for (int j = 0; j < i; j++)
                    {
                        if (numstate2[j, 1] == x)
                        {
                            m = true;
                            t = twoequalstate(numstate2, rules, alphabet, state.Length, i, j, n);
                            //Console.WriteLine(numstate2[i,0]+" "+ numstate2[j, 0]+" "+t);
                            if (t == true)
                            {
                                numstate[i, 1] = numstate[j, 1];
                                break;
                            }
                        }
                    }
                    if (t == false && m == true)
                    {
                        num++;
                        numstate[i, 1] = num.ToString();
                        //Console.WriteLine(numstate[i, 1]);
                    }
                }
                for (int i = 0; i < state.Length; i++)
                {
                    numstate2[i, 1] = numstate[i, 1];
                }
            }
            if (state.Length > 1)
            {
                Console.WriteLine(num);
            }
            else
            {
                Console.WriteLine(1);
            }

            //for(int i = 0; i < state.Length; i++)
            //{
            //    Console.WriteLine(numstate[i, 0]+" "+numstate[i,1]);
            //}

        }
    }
}
