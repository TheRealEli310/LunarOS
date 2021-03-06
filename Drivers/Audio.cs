using System;
using System.Collections.Generic;
using System.Text;

namespace LunarOS.Drivers
{
    class Audio
    {
        public static void Beep(int freq, int len)
        {
            Console.Beep(freq, len);
        }
        public static void PlayArray(int[][] notes)
        {
            foreach (var item in notes)
            {
                Beep(item[0], item[1]);
            }
        }
    }
}
