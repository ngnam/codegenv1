using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGeneratorV1
{
    public partial class MainForm : Form
    {
        private string _connectionString = "";
        private List<Table> _tables = new List<Table>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DisableMenu();
        }

        private void DisableMenu()
        {
            this.genCodeTrigger.Enabled = false;
            this.genCodeStored.Enabled = false;
            this.saveToFile.Enabled = false;
        }

        private void openProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var openFile = new OpenFileDialog())
            {
                openFile.Filter = "Profile Files|*.ini|Text Files|*.txt|All Files|*.*";

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFile.FileName;
                    var sqlConfiguration = Helpers.GetSqlConfiguration(filePath);
                    if (sqlConfiguration != null)
                    {
                        _connectionString = Helpers.GetConnectionString(sqlConfiguration);
                    }
                }
            }
        }

        private void connectToDB_Click(object sender, EventArgs e)
        {
            if (Helpers.CheckConnectionString(_connectionString))
            {
                this.genCodeTrigger.Enabled = true;
                this.genCodeStored.Enabled = true;

                if (tabTrigger.Created && rtxTrigger.Text.Length > 0)
                {
                    rtxTrigger.Text = "";
                }
                if (tabStored.Created && rtxStored.Text.Length > 0)
                {
                    rtxStored.Text = "";
                }
                this.saveToFile.Enabled = false;

                var tables = Helpers.GetTables(_connectionString);

                if (tables.Rows.Count > 0)
                {
                    foreach (DataRow row in tables.Rows)
                    {
                        var id = int.Parse(row["Id"].ToString());
                        var tableName = row["TableName"].ToString();
                        Table table = new Table(id, tableName);
                        this._tables.Add(table);
                    }

                    updateListTable();

                }
            }
            else
            {
                this.genCodeTrigger.Enabled = false;
                this.genCodeStored.Enabled = false;
            }
        }

        private void updateListTable()
        {
            if (_tables.Count > 0)
            {
                this.clbTables.DataSource = _tables;
                this.clbTables.DisplayMember = "Name";
                this.clbTables.ValueMember = "Id";
            }
        }

        private void selectAll(bool isChecked)
        {
            if (this.clbTables.Items.Count > 0)
            {
                for (int i = 0; i < this.clbTables.Items.Count; i++)
                {
                    this.clbTables.SetItemChecked(i, isChecked);
                }
            }
        }

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbSelectAll = (CheckBox)sender;
            selectAll(cbSelectAll.Checked);
        }

        private void genCodeTrigger_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabTrigger;
            List<Table> _tableToGenCodes = new List<Table>();
            var selectedItems = this.clbTables.CheckedItems;
            if (selectedItems.Count == 0)
            {
                MessageBox.Show("Please select table.");
                return;
            }
            if (selectedItems.Count > 0)
            {
                this.genCodeTrigger.Enabled = false;
                foreach (Table item in selectedItems)
                {
                    item.Columns.Clear();
                    var columns = Helpers.GetColumns(item.Name, _connectionString);
                    if (columns.Rows.Count > 0)
                    {
                        foreach (DataRow row in columns.Rows)
                        {
                            var id = int.Parse(row["Id"].ToString());
                            var dataField = row["DataField"].ToString();
                            var isPrimaryKey = bool.Parse(row["IsPrimaryKey"].ToString());
                            var dataType = row["DataType"].ToString();
                            var maxLength = 0;
                            int.TryParse(row["MaxLength"].ToString(), out maxLength);
                            var position = 0;
                            int.TryParse(row["Position"].ToString(), out position);

                            Column newColumn = new Column(id, dataField, isPrimaryKey, dataType, maxLength, position);
                            item.Columns.Add(newColumn);
                        }
                    }
                    _tableToGenCodes.Add(item);
                }
                genCodeTrigger_Replace(_tableToGenCodes);
                this.genCodeTrigger.Enabled = true;
            }
        }

        private void genCodeTrigger_Replace(List<Table> _tableToGenCodes)
        {
            if (_tableToGenCodes.Count > 0)
            {
                rtxTrigger.Text = "";

                foreach (var item in _tableToGenCodes)
                {
                    var publishedColumns = "";
                    publishedColumns = string.Join(", ", item.Columns.Select(c => c.DataField));
                    publishedColumns = publishedColumns.TrimEnd().TrimEnd(',');
                    var context = new { TableName = item.Name, PublishedColumns = publishedColumns };

                    string pathTemplate = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string template = Helpers.ReadAllTemplate(Path.Combine(pathTemplate, "Templates", "trigger.txt"));
                    string processTranslate = Helpers.Translate(template, context);

                    rtxTrigger.Text += "\n" + processTranslate;
                }

                if (rtxTrigger.Text.Length > 0)
                {
                    this.saveToFile.Enabled = true;
                }
            }
        }

        private void saveToFile_Click(object sender, EventArgs e)
        {
            this.saveToFile.Enabled = false;
            if (tabTrigger.Created && rtxTrigger.Text.Length > 0)
            {
                Helpers.SaveFileTo(rtxTrigger.Text);
            }
            if (tabStored.Created && rtxStored.Text.Length > 0)
            {
                Helpers.SaveFileTo(rtxStored.Text);
            }
            this.saveToFile.Enabled = true;
        }

        private void genCodeStored_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabStored;
            List<Table> _tableToGenCodes = new List<Table>();
            var selectedItems = this.clbTables.CheckedItems;
            if (selectedItems.Count == 0)
            {
                MessageBox.Show("Please select table.");
                return;
            }
            if (selectedItems.Count > 0)
            {
                this.genCodeStored.Enabled = false;
                
                foreach (Table item in selectedItems)
                {
                    item.Columns.Clear();
                    var columns = Helpers.GetColumns(item.Name, _connectionString);
                    if (columns.Rows.Count > 0)
                    {
                        foreach (DataRow row in columns.Rows)
                        {
                            var id = int.Parse(row["Id"].ToString());
                            var dataField = row["DataField"].ToString();
                            var isPrimaryKey = bool.Parse(row["IsPrimaryKey"].ToString());
                            var dataType = row["DataType"].ToString();
                            var maxLength = 0;
                            int.TryParse(row["MaxLength"].ToString(), out maxLength);
                            var position = 0;
                            int.TryParse(row["Position"].ToString(), out position);

                            Column newColumn = new Column(id, dataField, isPrimaryKey, dataType, maxLength, position);
                            item.Columns.Add(newColumn);
                        }
                    }
                    _tableToGenCodes.Add(item);
                }
                genCodStored_Replace(_tableToGenCodes);
                this.genCodeStored.Enabled = true;
            }
        }

        private void genCodStored_Replace(List<Table> _tableToGenCodes)
        {
            if (_tableToGenCodes.Count > 0)
            {
                rtxStored.Text = "";
                string blockInsertTemplate = "";
                foreach (var item in _tableToGenCodes)
                {
                    var primaryKeyColumn = "";
                    var primaryKeyDataType = "";
                    var primaryKey = item.Columns.FirstOrDefault(c => c.IsPrimaryKey);
                    if (primaryKey != null)
                    {
                        primaryKeyColumn = primaryKey.DataField;
                        primaryKeyDataType = primaryKey.DataType;
                    }
                    var publishedColumnsInsert = "";
                    publishedColumnsInsert = string.Join("\n,", item.Columns.Select(c => c.DataField));
                    publishedColumnsInsert = publishedColumnsInsert.TrimEnd().TrimEnd(',');

                    //T.r.query('/Inserted_tCIM_Customer/TenantId').value('.[1]', 'uniqueidentifier') AS TenantId,
                    var publishedColumnsSelect = "";
                    foreach (var col in item.Columns)
                    {
                        string parseStr = $"T.r.query('/Inserted_{item.Name}/{col.DataField}').value('.[1]', '{col.DataType}') AS [{col.DataField}],";
                        publishedColumnsSelect += "\n" + parseStr;
                    }

                    publishedColumnsSelect = publishedColumnsSelect.TrimEnd().TrimEnd(',');

                    var context = new { TableName = item.Name, PrimaryKey = primaryKeyColumn, PrimaryDataType = primaryKeyDataType, PublishedColumnsInsert = publishedColumnsInsert, PublishedColumnsSelect = publishedColumnsSelect };

                    string pathTemplate = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string template1 = Helpers.ReadAllTemplate(Path.Combine(pathTemplate, "Templates", "insert_update.txt"));

                    string processTranslate1 = Helpers.Translate(template1, context);

                    blockInsertTemplate += "\n" + processTranslate1;
                }

                if (blockInsertTemplate.Length > 0)
                {
                    var context = new { BlockInsertTemplate = blockInsertTemplate };
                    string pathTemplate = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string template2 = Helpers.ReadAllTemplate(Path.Combine(pathTemplate, "Templates", "stored_procedure.txt"));

                    string processTranslate2 = Helpers.Translate(template2, context);
                    rtxStored.Text = processTranslate2;
                }

                if (rtxStored.Text.Length > 0)
                {
                    this.saveToFile.Enabled = true;
                }
            }
        }
    }
}
