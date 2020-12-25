using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DailyTasksTracking
{
    public partial class TaskRecord : Form
    {
        public TaskRecord()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //using (var file = File.AppendAllText("db.csv", ,))
            //{
            var dtNow = DateTime.Now;
            var msg = dtNow.ToShortDateString() + ";" + dtNow.ToShortTimeString() + ";" + textBoxTaskDescription.Text;
            var list = new List<string>();
            list.Add(msg);
            File.AppendAllLines("db.csv", list, Encoding.GetEncoding(1251));//file.WriteLine(msg, Encoding.GetEncoding(1251));
            //}
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
