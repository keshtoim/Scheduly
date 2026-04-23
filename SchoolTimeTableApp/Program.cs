using System;
using System.Windows.Forms;

namespace testing
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AuthForm auth = new AuthForm();
            if (auth.ShowDialog() == DialogResult.OK)
                Application.Run(new MainForm(auth.AuthenticatedUser));
        }
    }
}
