using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace LotteGet
{
    public partial class Form1 : Form
    {
        string connetionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+ Environment.CurrentDirectory +"/Data.accdb";
        OleDbConnection cnn;
        DataTable dt;
        OleDbDataAdapter da;
        static int[] numberArray;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgv_data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv_n1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv_n2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv_n3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv_n4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv_n5.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv_n6.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv_collectionData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void btn_getData_Click(object sender, EventArgs e)
        {
            dt = getData();
            //numberArray = new int[45];
            numberArray = createEmptyArrayNumber(45);
            MessageBox.Show(numberArray[10].ToString());
            string[] row = getLastestRoundResult();
            dgv_data.DataSource = dt;
            lb_Round.Text = dt.Rows.Count.ToString();
            txb_n1.Text = row[0].ToString();
            txb_n2.Text = row[1].ToString();
            txb_n3.Text = row[2].ToString();
            txb_n4.Text = row[3].ToString();
            txb_n5.Text = row[4].ToString();
            txb_n6.Text = row[5].ToString();
            //list_n1 = getResultNextRoundByNumber(row["n1"].ToString());
            //DataTable dt_1 = new DataTable();
            //dt_1 = ConvertListToDataTable(list_n1);

            //DataTable list_n2 = new DataTable();
            //list_n2 = getResultNextRoundByNumber(row["n2"].ToString());
            //List<DataRow> list_n3 = new List<DataRow>();
            //list_n3 = getResultNextRoundByNumber(row["n3"].ToString());
            //List<DataRow> list_n4 = new List<DataRow>();
            //list_n4 = getResultNextRoundByNumber(row["n4"].ToString());
            //List<DataRow> list_n5 = new List<DataRow>();
            //list_n5 = getResultNextRoundByNumber(row["n5"].ToString());
            //List<DataRow> list_n6 = new List<DataRow>();
            //list_n6 = getResultNextRoundByNumber(row["n6"].ToString());
            DataTable dt_1 = new DataTable();
            dt_1 = getResultNextRoundByNumber(txb_n1.Text);
            dgv_n1.DataSource = dt_1;
            lb_count_n1.Text = dt_1.Rows.Count.ToString();
            collectionDataLotte(dt_1, 45);
            DataTable dt_2 = new DataTable();
            dt_2 = getResultNextRoundByNumber(txb_n2.Text);
            dgv_n2.DataSource = dt_2;
            lb_count_n2.Text = dt_2.Rows.Count.ToString();
            collectionDataLotte(dt_2, 45);
            DataTable dt_3 = new DataTable();
            dt_3 = getResultNextRoundByNumber(txb_n3.Text);
            dgv_n3.DataSource = dt_3;
            lb_count_n3.Text = dt_3.Rows.Count.ToString();
            collectionDataLotte(dt_3, 45);
            DataTable dt_4 = new DataTable();
            dt_4 = getResultNextRoundByNumber(txb_n4.Text);
            dgv_n4.DataSource = dt_4;
            lb_count_n4.Text = dt_4.Rows.Count.ToString();
            collectionDataLotte(dt_4, 45);
            DataTable dt_5 = new DataTable();
            dt_5 = getResultNextRoundByNumber(txb_n5.Text);
            dgv_n5.DataSource = dt_5;
            lb_count_n5.Text = dt_5.Rows.Count.ToString();
            collectionDataLotte(dt_5, 45);
            DataTable dt_6 = new DataTable();
            dt_6 = getResultNextRoundByNumber(txb_n6.Text);
            dgv_n6.DataSource = dt_6;
            lb_count_n6.Text = dt_6.Rows.Count.ToString();
            collectionDataLotte(dt_6, 45);

            DataTable dt_colect = new DataTable();
            dt_colect = getCollectionResult(numberArray);
            dgv_collectionData.DataSource = dt_colect;
        }

        private string[] getLastestRoundResult()
        {
            string[] row = new string[6];
            DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
            row[0] = lastRow.ItemArray[4].ToString();
            row[1] = lastRow.ItemArray[5].ToString();
            row[2] = lastRow.ItemArray[6].ToString();
            row[3] = lastRow.ItemArray[7].ToString();
            row[4] = lastRow.ItemArray[8].ToString();
            row[5] = lastRow.ItemArray[9].ToString();
            return row;
        }

        public DataTable getCollectionResult(int[] data)
        {
            DataTable result = new DataTable();
            DataColumn new_column;
            DataRow new_row;
            new_column = new DataColumn();
            new_column.DataType = System.Type.GetType("System.String");
            new_column.ColumnName = "number";
            result.Columns.Add(new_column);
            new_column = new DataColumn();
            new_column.DataType = System.Type.GetType("System.Int32");
            new_column.ColumnName = "count";
            result.Columns.Add(new_column);
            if(data.Count() > 0)
            {
                for (int i = 0; i < data.Count(); i++ )
                {
                    new_row = result.NewRow();
                    new_row["number"] = i;
                    new_row["count"] = data[i];
                    result.Rows.Add(new_row);
                }
            }
            return result;
        }

        public void collectionDataLotte(DataTable dt_n, int loop)
        {
            if(dt_n.Rows.Count > 0 && loop > 0)
            {
                for (int i = 0; i < dt_n.Rows.Count; i++)
                {
                    for (int j = 0; j < dt_n.Columns.Count; j++)
                    {
                        int temp = Convert.ToInt32(dt_n.Rows[i][j]);
                        numberArray[temp]++;
                    }
                }
            }
            else
            {
                MessageBox.Show("Data is empty");
            }
        }

        private DataTable getResultNextRoundByNumber(string n)
        {
            string[] rows = new string[]{};
            //List<string[]> dataReturn = new List<string[]>();
            DataTable dataReturn = new DataTable();
            DataColumn column;
            DataRow next;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "n1";
            dataReturn.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "n2";
            dataReturn.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "n3";
            dataReturn.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "n4";
            dataReturn.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "n5";
            dataReturn.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "n6";
            dataReturn.Columns.Add(column);
            DataTable result = new DataTable();
            //DataRow temp = new DataRow();
            result = getData();
            for (int i = 0; i < result.Rows.Count; i++)
            {
                if (i < result.Rows.Count - 1)
                {
                    for (int j = 0; j < result.Columns.Count; j++ )
                    {
                        if(j >= 4)
                        {
                            
                            if(n == result.Rows[i][j].ToString())
                            {
                                next = dataReturn.NewRow();
                                DataRow temp = result.Rows[i + 1];
                                for (int k = 4; k < result.Columns.Count; k++)
                                {
                                    string res = result.Rows[i + 1][k].ToString();
                                    next["n" + (k-3).ToString()] = res;
                                } 
                                dataReturn.Rows.Add(next);
                            }
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            return dataReturn;
        }

        private DataTable getData()
        {
            List<DataRow> result = new List<DataRow>();
            cnn = new OleDbConnection(connetionString);
            cnn.Open();
            try
            {
                string StrCmd = "select * from Data;";

                da = new OleDbDataAdapter(StrCmd, cnn);
                if (da != null)
                {

                    dt = new DataTable();
                    da.Fill(dt);
                }
                else
                {
                    MessageBox.Show("Error: Data null");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception Error: " + ex);
            }
            cnn.Close();
            return dt;
        }

        public int[] createEmptyArrayNumber (int loop)
        {
            int[] data = new int[]{};
            if(loop > 0 && (loop == 45 || loop == 55))
            {
                data = new int[loop+1];
                switch(loop)
                {
                    case 45 :
                        for (int i = 1; i <= loop; i++)
                        {
                            data[i] = 0;
                        }
                        break;
                    case 55 :
                        for (int i = 1; i <= loop; i++)
                        {
                            data[i] = 0;
                        }
                        break;
                    default :
                        break;
                }
                MessageBox.Show(data[10].ToString());
            }
            else
            {
                MessageBox.Show("Rule failed");
            }
            return data;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
