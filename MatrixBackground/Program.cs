using System;
using System.Windows.Forms;

namespace MatrixBackground
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            using (var game = new Game1())
            {
                Form frm = (Form)Control.FromHandle(game.Window.Handle);
                frm.FormBorderStyle = FormBorderStyle.None;
                game.Run();
            }
        }
    }
#endif
}
