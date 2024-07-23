using Iot.Device.Ws28xx.Esp32;
using System;
using System.Device.Gpio;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace HelloWorld_NanoFramework
{
    public class Program
    {
        private static GpioController s_GpioController;
        public static void Main()
        {
            s_GpioController = new GpioController();

            GpioPin led = s_GpioController.OpenPin(22, PinMode.Output);

            led.Write(PinValue.Low);

            Ws28xx neo = new Ws2812c(16, 1);

            BitmapImage img = neo.Image;

            int count = 0;
            while (true)
            {
                img.SetPixel(0, 0, Wheel(count++ & 255));
                neo.Update();
            }
        }

        static Color Wheel(int position)
        {
            position = 255 - position; // Reverse for a more typical rainbow (red at 0, blue at 255)
            if (position < 85)
            {
                return Color.FromArgb(255 - position * 3, 0, position * 3);
            }
            else if (position < 170)
            {
                position -= 85;
                return Color.FromArgb(0, position * 3, 255 - position * 3);
            }
            else
            {
                position -= 170;
                return Color.FromArgb(position * 3, 255 - position * 3, 0);
            }
        }
    }
}
