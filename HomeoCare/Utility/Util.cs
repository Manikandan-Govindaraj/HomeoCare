using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeoCare.Utility
{
    public static class Util
    {
        public static string GetOrderUFID()
        {
            string numbers = "1234567890";
            int length = 7;
            string id = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character;
                do
                {
                    int index = new Random().Next(0, numbers.Length);
                    character = numbers.ToCharArray()[index].ToString();
                } while (id.IndexOf(character) != -1);
                id += character;
            }
            return "OR" + id;
        }
    }
}
