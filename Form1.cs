using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGeneratorV1
{
    public partial class Form1 : Form
    {
        private string connectionString = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void openProfileCtrlOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text file (*.ini)|*.ini|All files (*.*)|*.*";
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Open a profile file";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                //MessageBox.Show(filePath);
                var parser = new FileIniDataParser();

                IniData data = parser.ReadFile(filePath);

                var sqlConfiguration = new SqlConfiguration();

                sqlConfiguration.SqlServerName = data["Configuration"]["SqlServerName"];
                sqlConfiguration.DatabaseName = data["Configuration"]["DatabaseName"];
                sqlConfiguration.UserName = data["Configuration"]["UserName"];
                sqlConfiguration.Password = data["Configuration"]["Password"];
                connectionString = $"Server={sqlConfiguration.SqlServerName},1433;Database={sqlConfiguration.DatabaseName};User Id={sqlConfiguration.UserName};Password={sqlConfiguration.Password};";
                this.menu_connectToServer.Enabled = true;
                this.menu_generateINSERTStatement.Enabled = true;
                this.menu_generateTriggerStatement.Enabled = true;
                //var s = parser.Sections[0];
            }
        }

        private void connectToServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsConnectionStringValid(connectionString))
            {
                //cbListTables.DataSource = new List<string>() { "tCIM_Customer", "tCIM_Currency" };
                var listTables = GetTables(connectionString);
                if (listTables.Rows.Count > 0)
                {
                    this.lstTables.Items.Clear();
                    foreach (DataRow row in listTables.Rows)
                    {
                        this.lstTables.Items.Add(row["TableName"]);
                    }
                }
            }
        }

        private bool IsConnectionStringValid(string connectionString)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString)) { sqlConnection.Open(); }
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                this.menu_connectToServer.Enabled = false;
                this.menu_generateINSERTStatement.Enabled = false;
                this.menu_generateTriggerStatement.Enabled = false;
            }
        }

        private void menu_generateINSERTStatement_Click(object sender, EventArgs e)
        {

        }

        private void menu_generateTriggerStatement_Click(object sender, EventArgs e)
        {

        }

        private DataTable GetTables(string connectionString)
        {
            DataTable dt = new DataTable("ListTable");
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("TableName", typeof(string));
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_SCHEMA ASC;";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                int id = 1;
                while (reader.Read())
                {
                    string tableName = reader["TABLE_NAME"].ToString();
                    DataRow newRow = dt.NewRow();
                    newRow["Id"] = id;
                    newRow["TableName"] = tableName;
                    dt.Rows.Add(newRow);
                    id++;
                }
                dt.AcceptChanges();
            }
            return dt;
        }

        private void lstTables_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckState newState = e.NewValue;
            string itemName = this.lstTables.Items[e.Index].ToString();

            if (newState == CheckState.Checked)
            {
                var lstColumnsOfTable = GetColumns(itemName, connectionString);
                if (lstColumnsOfTable.Rows.Count > 0)
                {
                    ////this.lstColumns.DataSource = null;
                    //var columnOfTables = new List<ColumnOfTable>();
                    //foreach (DataRow row in lstColumnsOfTable.Rows)
                    //{
                    //    columnOfTables.Add(new ColumnOfTable(int.Parse(row["Id"].ToString()), row["ColumnName"].ToString(), row["DataType"].ToString()));
                    //}
                    //if (columnOfTables.Any())
                    //{
                    //    this.lstColumns.DataSource = columnOfTables;
                    //    this.lstColumns.DisplayMember = "ColumnName";
                    //    this.lstColumns.ValueMember = "Id";

                    //    for (int i = 0; i < this.lstColumns.Items.Count; i++)
                    //    {
                    //        this.lstColumns.SetItemChecked(i, true);
                    //    }
                    //}
                }
            }
            else if (newState == CheckState.Unchecked)
            {
                //this.lstColumns.DataSource = null;
            }
        }

        public class ColumnOfTable
        {
            public int Id { get; set; }
            public string ColumnName { get; set; }
            public string DataType { get; set; }
            public ColumnOfTable(int id, string columnName, string dataType)
            {
                Id = id;
                ColumnName = columnName;
                DataType = dataType;
            }
        }
        private DataTable GetColumns(string table, string connectionString)
        {
            DataTable dt = new DataTable("ListColumnsOfTable");
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("ColumnName", typeof(string));
            dt.Columns.Add("DataType", typeof(string));
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @" SELECT COLUMN_NAME,
CASE WHEN ISNULL(CHARACTER_MAXIMUM_LENGTH, '') = '' 
THEN DATA_TYPE
ELSE DATA_TYPE + '(' + CAST(ISNULL(CHARACTER_MAXIMUM_LENGTH, '') AS VARCHAR(100)) + ')'
END AS DATA_TYPE  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName 
ORDER BY ORDINAL_POSITION ASC;";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TableName", table);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int id = 1;
                        while (reader.Read())
                        {
                            string columnName = reader["COLUMN_NAME"].ToString();
                            string dataType = reader["DATA_TYPE"].ToString();
                            DataRow newRow = dt.NewRow();
                            newRow["Id"] = id;
                            newRow["ColumnName"] = columnName;
                            newRow["DataType"] = dataType;
                            dt.Rows.Add(newRow);
                            id++;
                        }
                    }
                }

                dt.AcceptChanges();
            }
            return dt;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
