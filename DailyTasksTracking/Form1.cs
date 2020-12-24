using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DailyTasksTracking
{
    public partial class FormCapture : Form
    {
        public FormCapture()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (var file = File.AppendText("db.csv"))
            {
                var dtNow = DateTime.Now;
                var msg = dtNow.ToShortDateString() + "," + dtNow.ToShortTimeString() + "," + textBoxTaskText.Text;
                //var bytes = Encoding.UTF8.GetBytes(msg);
                //file.Write(bytes, 0, bytes.Length);
                file.WriteLine(msg);
            }
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
