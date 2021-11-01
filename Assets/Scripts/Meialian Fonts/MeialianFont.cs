using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MeialianFonts
{
    public static class MeialianFont
    {
        public static string[] SplitToMeialianFont(this string str)
        {
            List<string> vs = new List<string>();
            string letterConverted = "";
            str += " ";
            for (int i = 0; i < str.Length - 1; i++)
            {
                char item = str[i];
                if (item == 'Q')
                {
                    if (str[i + 1] == 'u') { letterConverted += "Ku"; i++; }
                    else if (str[i + 1] == 'U') letterConverted += "K";
                    else letterConverted += "Ku";
                }
                else if (item == 'q')
                {
                    if (str[i + 1] == 'u') { letterConverted += "ku"; i++; }
                    else if (str[i + 1] == 'U') letterConverted += "k";
                    else letterConverted += "ku";
                }
                else letterConverted += item;
            }
            letterConverted += "  ";
            for (int i = 0; i < letterConverted.Length - 2; i++)
            {
                var c = letterConverted[i];
                var n1 = letterConverted[i + 1];
                var n2 = letterConverted[i + 1].ToString() + letterConverted[i + 2];
                if (c.isCapitalVowel())
                {
                    vs.Add(c.ToString());
                    continue;
                }
                else if (c.isLowerlVowel())
                {
                    if (n2 == "ng")
                    {
                        vs.Add(c.ToString() + "ng");
                        i += 2;
                    }
                    else if (n1.isLowerWritablelConsonant())
                    {
                        vs.Add(c.ToString() + letterConverted[i + 1]);
                        i++;
                    }
                    else if (n1.isLowerlVowel())
                    {
                        vs.Add(c.ToString() + letterConverted[i + 1]);
                        i++;
                    }
                    else vs.Add(c.ToString());
                }
                else if (c == n1)
                {
                    vs.Add(c.ToString() + n1);
                    i++;
                }
                else if (c + n1.ToString() == "Th" || c + n1.ToString() == "th")
                {
                    vs.Add(c.ToString() + n1);
                    i++;
                }
                else if (c + n1.ToString() == "Tr" || c + n1.ToString() == "tr")
                {
                    vs.Add(c.ToString() + n1);
                    i++;
                }
                else if (c + n1.ToString() == "Dr" || c + n1.ToString() == "dr")
                {
                    vs.Add(c.ToString() + n1);
                    i++;
                }
                else if (c + n1.ToString() == "St" || c + n1.ToString() == "st")
                {
                    vs.Add(c.ToString() + n1);
                    i++;
                }
                else if (c + n1.ToString() == "Sh" || c + n1.ToString() == "sh")
                {
                    vs.Add(c.ToString() + n1);
                    i++;
                }
                else if (c + n1.ToString() == "Ch" || c + n1.ToString() == "ch")
                {
                    vs.Add(c.ToString() + n1);
                    i++;
                }
                else
                {
                    vs.Add(c.ToString());
                }
            }
            foreach (var item in vs)
            {
                Debug.Log(item);
            }
            return vs.ToArray();
        }

        public static bool isCapitalVowel(this char item)
        {
            return item == 'A' || item == 'E' || item == 'I' || item == 'O' || item == 'U';
        }
        public static bool isLowerlVowel(this char item)
        {
            return item == 'a' || item == 'e' || item == 'i' || item == 'o' || item == 'u';
        }

        public static bool isLowerWritablelConsonant(this char item)
        {
            return item == 'l' || item == 'n' || item == 'm' || item == 'r' || item == 's';
        }
    }
}
