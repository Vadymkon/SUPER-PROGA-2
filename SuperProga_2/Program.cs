using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperProga_2
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(Exception1);
            Application.Run(new Form1());
        }

        static void Exception1(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("Somethink starts wrong.");
            MessageBox.Show(e.Exception.ToString());
            
            //это выполнится если что-то пойдёт не так
        }
    }
}
