using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DailyTasksTracking
{
    public partial class FormTimerIntervalSet : Form
    {
        public FormTimerIntervalSet()
        {
            InitializeComponent();
            textBoxInterval.Text = "60";
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Program.timerInterval = int.Parse(textBoxInterval.Text);
            this.Close();
        }
    }
}
