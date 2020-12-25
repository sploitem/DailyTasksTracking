using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DailyTasksTracking
{
    static class Program
    {
        static Timer timer;
        static bool formIsShowing;
        static TaskRecord form;
        static public int timerInterval;

        const int SECOND = 1000;
        const int MINUTE = 60 * SECOND;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            timer = new Timer();
            timerInterval = 60;
            timer.Interval = timerInterval * MINUTE;
            timer.Tick += Timer_Tick;
            formIsShowing = false;
            using (NotifyIcon icon = new NotifyIcon())
            {
                icon.Icon = System.Drawing.Icon.ExtractAssociatedIcon("employee.ico");// Application.ExecutablePath);
                icon.ContextMenu = new ContextMenu(
                    new MenuItem[] {
                        new MenuItem("Открыть запись задачи", (s, e) => { ShowForm(s, e); }),
                        new MenuItem("Отчет за день", (s, e) => { ShowReportForm(s, e); }),
                        new MenuItem("Настройки", (s, e) => { SetTimerInterval(s, e); }),
                        new MenuItem("Выход", (s, e) => { Application.Exit(); }),
                    }
                );
                icon.Visible = true;
                timer.Start();
                Application.Run();
                timer.Stop();
                icon.Visible = false;
            }
        }

        private static void RestartTimer(object sender, EventArgs e)
        {
            timer?.Stop();
            timer?.Start();
        }

        private static void SetTimerInterval(object sender, EventArgs e)
        {
            var form = new FormTimerIntervalSet();
            form.ShowDialog();
            timer?.Stop();
            timer.Interval = timerInterval * MINUTE;
            RestartTimer(sender, e);
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            ShowForm(null, null);
            if (DateTime.Now.Hour == 13 && DateTime.Now.Minute == 00)
            {
                ShowForm(null, null);
                RestartTimer(null, null);
            }
        }

        private static void ShowForm(object sender, EventArgs e)
        {
            if (formIsShowing == false)
            {
                form = new TaskRecord();
                form.FormClosed += Form_FormClosed;
                form.Text = form.Text + " " + DateTime.Now.ToShortTimeString();
                form.TopLevel = true;
                form.Show();
                formIsShowing = true;
            }
        }

        private static void ShowReportForm(object sender, EventArgs e)
        {
            var form = new DailyReport();
            form.Text = form.Text + " " + DateTime.Now.ToShortDateString();
            form.Show();
        }

        private static void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            formIsShowing = false;
        }
    }
}
