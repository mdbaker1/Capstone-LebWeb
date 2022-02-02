using System.Text;

namespace LebWeb.Helpers
{
    public class StringUtilities
    {
        public static string CustomToLower(string value ) 
        { 
            return value.ToLower().Trim(); 
        }

        public static string CustomToUpper(string value) 
        {
            string result = "";
            value = value.Trim().ToLower();

            // Capitalize 1st and 3rd letter
            if (value.StartsWith("mc")
                || value.Equals("degette")
                || value.Equals("defazio")
                || value.Equals("delauro")
                || value.Equals("desaulnier")
                )
            {
                char[] chars = value.ToCharArray();
                for(int i = 0; i < 4; i += 2)
                {
                    chars[i] = char.ToUpper(chars[i]);
                }
                result = new string(chars);
                return result;
            }

            // Capitalize 1st and 4th letter
            if (value.Equals("macdonald")
                || value.Equals("delbene")
                || value.Equals("desjarlais")
                )
            {
                char[] chars = value.ToCharArray();
                for (int i = 0; i < 4; i += 3)
                {
                    chars[i] = char.ToUpper(chars[i]);
                }
                result = new string(chars);
                return result;
            }

            // Capitalize letter after single quote / space / dash
            if (value.Contains('-') || value.Contains(' ') || value.Contains('\''))
            {
                value = value.ToLower();
                char[] chars = new[] { '-', ' ', '\'' };
                StringBuilder specialCase = new StringBuilder(value.Length);
                bool makeUpper = true;
                foreach(var i in value)
                {
                    if (makeUpper)
                    {
                        specialCase.Append(Char.ToUpper(i));
                        makeUpper = false;
                    }
                    else
                    {
                        specialCase.Append(i);
                    }
                    if (chars.Contains(i))
                    {
                        makeUpper = true;
                    }
                }
                return specialCase.ToString();
            }

            return value.First().ToString().ToUpper() + value.Substring(1).ToLower();
        }
    }
}
