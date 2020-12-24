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
    public partial class FormDailyReport : Form
    {
        public FormDailyReport()
        {
            InitializeComponent();
            ParseCsv();
        }
        DataTable dataTable;
        public void ParseCsv()
        {
            dataTable = new DataTable();
            var lines = File.ReadAllLines("db.csv");
            dataTable.Columns.Add("Дата");
            dataTable.Columns.Add("Время");
            dataTable.Columns.Add("Задача");
            for (int i = 0; i < lines.Length; i++)
            {
                var split = lines[i].Split(',');
                if (DateTime.Parse(split[0]) == DateTime.Now.Date)
                {
                    string[] values = { split[0], split[1], split[2] };
                    dataTable.Rows.Add(values);
                }
            }
            dataGridViewReport.DataSource = dataTable;
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            var ofd = new SaveFileDialog();
            ofd.Filter = "(*.csv)|*.csv";
            ofd.FileName = DateTime.Now.ToShortDateString();
            var dlgResult = ofd.ShowDialog();
            var filename = ofd.FileName;
            if (dlgResult == DialogResult.Cancel)
            {
                return;
            }
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            using (var file = File.AppendText(filename))
            {
                foreach (var item in dataTable.Rows)
                {
                    var arr = (item as DataRow).ItemArray;
                    file.WriteLine(arr[0].ToString() + "," + arr[1].ToString() + "," +
                        arr[2].ToString());
                } 
            }
        }
    }
}
