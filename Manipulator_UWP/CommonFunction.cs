using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PointSpase;
using Windows.UI;

namespace Manipulator_UWP
{

    delegate void SendMessage(string message,  Color colors);

    static class CommonFunction
    {
        static  SendMessage sendMessage;
        static Point commonPoint = new Point();


        static public Point CommonPoint
            {
                get{ return commonPoint; }
                set{ commonPoint = value; }         
            }
        static void Home()
        {

        }
        /// <summary>
        /// Установить функцию для отправки сообщений в консоль
        /// </summary>
        /// <param name="send"></param>
        static public void SetsendMessage(SendMessage send)
        {
            sendMessage = send;
        }

        static public void CommonConsoleWrite(string message, Color colors)
        {
            if (sendMessage != null) sendMessage(message, colors);
        }
        static public void CommonConsoleWrite(string message)
        {
            if (sendMessage != null) sendMessage(message,Colors.Black);
        }
    }

   
}
