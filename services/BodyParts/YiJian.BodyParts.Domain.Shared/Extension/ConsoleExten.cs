using System;
using System.Drawing;
using Console = Colorful.Console;

namespace YiJian.BodyParts
{
    /// <summary>
    /// 控制台输出拓展帮助
    /// </summary>
    public static class ConsoleExten
    {
        public static void WriteLine(dynamic msg)
        {
            WriteLine(msg, Color.Green);
        }

        public static void WriteLine(dynamic msg, Color c)
        {
            if (msg == null) return;

            var now = "[" + DateTime.Now.ToString("HH:mm:sss") + " SZYJ] ";

            if (msg is string)
                Console.WriteLine(now + msg, c);
            else
            {
                Console.WriteLine(now, c);
                Console.WriteLine(JsonHelper.SerializeObject(msg), c);
            }
        }
    }
}