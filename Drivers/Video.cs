using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LunarOS.Drivers
{
    class Video
    {
        private static int screenX = 640;
        private static int screenY = 480;
        private static Color[] pixelBuffer = new Color[(screenX * screenY) + screenX];
        private static Color[] pixelBufferOld = new Color[(screenX * screenY) + screenX];
        private static Canvas canvas = FullScreenCanvas.GetFullScreenCanvas();
        public static void init()
        {
            canvas.Mode = new Mode(screenX, screenY, ColorDepth.ColorDepth32);
            Cosmos.System.MouseManager.ScreenWidth = Convert.ToUInt32(screenX);
            Cosmos.System.MouseManager.ScreenHeight = Convert.ToUInt32(screenY);
            Kernel.video = true;
        }
        public static void setPixel(int x, int y, Color c)
        {
            //if (x > screenX || y > screenY) return;
            pixelBuffer[(x * y) + x] = c;
        }
        public static void drawScreen()
        {
            Pen pen = new Pen(Color.Orange);
            for (int y = 0, h = screenY; y < h; y++)
            {
                for (int x = 0, w = screenX; x < w; x++)
                {
                    if (!(pixelBuffer[(y * x) + x] == pixelBufferOld[(y * y) + x]))
                    {
                        pen.Color = pixelBuffer[(y * screenX) + x];
                        canvas.DrawPoint(pen, x, y);
                    }
                }
            }
            for (int i = 0, len = pixelBuffer.Length; i < len; i++)
            {
                pixelBuffer[i] = pixelBufferOld[i];
            }
        }
        public static void clearScreen(Color c)
        {
            for (int i = 0, len = pixelBuffer.Length; i < len; i++)
            {
                pixelBuffer[i] = c;
            }
        }
        public static void update()
        {
            clearScreen(Color.Black);
            Drivers.Mouse.update();
            drawScreen();
        }
    }
}
