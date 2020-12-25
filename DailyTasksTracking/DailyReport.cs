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
    public partial class DailyReport : Form
    {
        public DailyReport()
        {
            InitializeComponent();
            ParseCsv();
        }

        DataTable dataTable;
        public void ParseCsv()
        {
            dataTable = new DataTable();
            var lines = File.ReadAllLines("db.csv", Encoding.GetEncoding(1251));
            dataTable.Columns.Add("Дата");
            dataTable.Columns.Add("Время");
            dataTable.Columns.Add("Задача");
            for (int i = 0; i < lines.Length; i++)
            {
                var split = lines[i].Split(';');
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
            Encoding win1251 = Encoding.GetEncoding(1251);
            //using (var file = File.AppendText(filename))
            //{
            foreach (var item in dataTable.Rows)
            {
                var arr = (item as DataRow).ItemArray;
                var row = arr[0].ToString() + ";" + arr[1].ToString() + ";" + arr[2].ToString();
                //var utf8Bytes = Encoding.Default.GetBytes(row);
                //var win1251Bytes = Encoding.Convert(Encoding.Default, win1251, utf8Bytes);
                //var rowWin1251 = win1251.GetString(win1251Bytes);
                var list = new List<string>();
                list.Add(row);
                File.AppendAllLines(filename, list, Encoding.GetEncoding(1251)); //file.WriteLine(row);//Win1251);
            }
            //}
        }
    }
}
