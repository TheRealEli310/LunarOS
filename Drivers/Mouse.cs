using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Cosmos.System;

namespace LunarOS.Drivers
{
    class Mouse
    {
        public static void update()
        {
            Drivers.Video.setPixel(Convert.ToInt32(Cosmos.System.MouseManager.X), Convert.ToInt32(Cosmos.System.MouseManager.Y), Color.White);
            Drivers.Video.setPixel(Convert.ToInt32(Cosmos.System.MouseManager.X + 1), Convert.ToInt32(Cosmos.System.MouseManager.Y + 1), Color.White);
            Drivers.Video.setPixel(Convert.ToInt32(Cosmos.System.MouseManager.X + 2), Convert.ToInt32(Cosmos.System.MouseManager.Y + 2), Color.White);
            Drivers.Video.setPixel(Convert.ToInt32(Cosmos.System.MouseManager.X + 3), Convert.ToInt32(Cosmos.System.MouseManager.Y + 3), Color.White);
            Drivers.Video.setPixel(Convert.ToInt32(Cosmos.System.MouseManager.X + 4), Convert.ToInt32(Cosmos.System.MouseManager.Y + 4), Color.White);
        }
    }
}
