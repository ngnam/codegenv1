using IniParser;
using IniParser.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CodeGeneratorV1
{
    public static class Helpers
    {
        public static SqlConfiguration GetSqlConfiguration(string filePath)
        {
            try
            {
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile(filePath);
                var sqlConfiguration = new SqlConfiguration();

                sqlConfiguration.SqlServerName = data["Configuration"]["SqlServerName"];
                sqlConfiguration.DatabaseName = data["Configuration"]["DatabaseName"];
                sqlConfiguration.UserName = data["Configuration"]["UserName"];
                sqlConfiguration.Password = data["Configuration"]["Password"];

                return sqlConfiguration;
            }
            catch (Exception)
            {
                return null;
            }
            //connectionString = $"Server={sqlConfiguration.SqlServerName},1433;Database={sqlConfiguration.DatabaseName};User Id={sqlConfiguration.UserName};Password={sqlConfiguration.Password};";
        }

        public static string GetConnectionString(SqlConfiguration sqlConfiguration)
        {
            return $"Server={sqlConfiguration.SqlServerName},1433;Database={sqlConfiguration.DatabaseName};User Id={sqlConfiguration.UserName};Password={sqlConfiguration.Password};";
        }

        public static bool CheckConnectionString(string connectionString)
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

        public static DataTable GetTables(string connectionString)
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

        public static DataTable GetColumns(string tableName, string connectionString)
        {
            DataTable dt = new DataTable("Columns");
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("DataField", typeof(string));
            dt.Columns.Add("IsPrimaryKey", typeof(string));
            dt.Columns.Add("DataType", typeof(string));
            dt.Columns.Add("MaxLength", typeof(int));
            dt.Columns.Add("Position", typeof(int));

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT c.COLUMN_NAME,
CASE WHEN ISNULL(c.CHARACTER_MAXIMUM_LENGTH, '') = '' OR ISNULL(c.CHARACTER_MAXIMUM_LENGTH, '') = '-1'
THEN c.DATA_TYPE
ELSE c.DATA_TYPE + '(' + CAST(ISNULL(c.CHARACTER_MAXIMUM_LENGTH, '') AS VARCHAR(100)) + ')'
END AS DATA_TYPE,
c.CHARACTER_MAXIMUM_LENGTH,
c.ORDINAL_POSITION,
CASE WHEN k.COLUMN_NAME = c.COLUMN_NAME THEN 'true'
ELSE 'false'
END AS IS_PRIMARY_KEY
FROM INFORMATION_SCHEMA.COLUMNS c
INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE k on c.TABLE_NAME = k.TABLE_NAME AND k.CONSTRAINT_NAME LIKE 'PK%'
WHERE c.TABLE_NAME = @TableName 
ORDER BY c.ORDINAL_POSITION ASC;";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TableName", tableName);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int id = 1;
                        while (reader.Read())
                        {
                            string columnName = reader["COLUMN_NAME"].ToString();
                            string dataType = reader["DATA_TYPE"].ToString();
                            int maxLength = 0;
                            int.TryParse(reader["CHARACTER_MAXIMUM_LENGTH"].ToString(), out maxLength);
                            int position = 0;
                            int.TryParse(reader["ORDINAL_POSITION"].ToString(), out position);

                            bool isPrimaryKey = false;
                            bool.TryParse(reader["IS_PRIMARY_KEY"].ToString(), out isPrimaryKey);

                            DataRow newRow = dt.NewRow();
                            newRow["Id"] = id;
                            newRow["DataField"] = columnName;
                            newRow["IsPrimaryKey"] = isPrimaryKey;
                            newRow["DataType"] = dataType;
                            newRow["MaxLength"] = maxLength;
                            newRow["Position"] = position;
                            dt.Rows.Add(newRow);
                            id++;
                        }
                    }
                }
                dt.AcceptChanges();
            }
            return dt;
        }

        public static string ReadAllTemplate(string templateFilePath)
        {
            string templateContent = string.Empty;
            try
            {
                //string templateFilePath = "path_to_your_template.txt"; // Replace with the actual file path
                templateContent = File.ReadAllText(templateFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading template: " + ex.Message);
            }
            return templateContent;
        }

        public static string Translate(string pattern, object context)
        {
            return Regex.Replace(pattern, @"{{(\w+)}}", match =>
            {
                string tag = match.Groups[1].Value;
                if (context != null)
                {
                    var prop = context.GetType().GetProperty(tag);
                    if (prop != null)
                    {
                        var value = prop.GetValue(context);
                        if (value != null)
                            return value.ToString();
                    }
                }
                return "";
            });
        }

        public static void SaveFileTo(string contents)
        {
            try
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "SQL Files|*.sql|Text Files|*.txt|All Files|*.*";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var filePath = saveFileDialog.FileName;
                        File.WriteAllText(filePath, contents, System.Text.Encoding.UTF8);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
    }
}
