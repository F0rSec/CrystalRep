using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net.Security;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Windows.Forms;
using System;
using System.IO;
using CrystalDecisions.Windows.Forms;
using CrystalDecisions.ReportAppServer.DataDefModel;
using MySqlX.XDevAPI.Common;

namespace Crystal
{
    public partial class Fields : Form
    {

        MySqlConnection con =
         new MySqlConnection("Server=localhost;Database=esten;Uid=root;Pwd=;");
        public Fields()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string lastname = textBox2.Text;
            string group = textBox3.Text;
            float dot = float.Parse(textBox4.Text);
            float php = float.Parse(textBox5.Text);
            float react = float.Parse(textBox6.Text);
            float ai = float.Parse(textBox7.Text);
            float erp = float.Parse(textBox8.Text);
            float ionic = float.Parse(textBox9.Text);
            float machine = float.Parse(textBox10.Text);
            float sig = float.Parse(textBox11.Text);
            float toe = float.Parse(textBox12.Text);
            float moy = (dot + php + react + ai + erp + ionic + machine + sig + toe) / 9;
            MySqlCommand cmd = new MySqlCommand("INSERT INTO bullet (f_name, l_name, group_name, dot, php, react, ai, erp, ionic, machine, sig, toe, moy)\r\nVALUES (@f_name , @l_name , @group ,@dot , @php , @react , @ai , @erp , @ionic , @machine , @sig , @toe , @moy)", con);

            cmd.Parameters.AddWithValue("@f_name", name);
            cmd.Parameters.AddWithValue("@l_name", lastname);
            cmd.Parameters.AddWithValue("@group", group);
            cmd.Parameters.AddWithValue("@dot", dot);
            cmd.Parameters.AddWithValue("@php", php);
            cmd.Parameters.AddWithValue("@react", react);
            cmd.Parameters.AddWithValue("@ai", ai);
            cmd.Parameters.AddWithValue("@erp", erp);
            cmd.Parameters.AddWithValue("@ionic", ionic);
            cmd.Parameters.AddWithValue("@machine", machine);
            cmd.Parameters.AddWithValue("@sig", sig);
            cmd.Parameters.AddWithValue("@toe", toe);
            cmd.Parameters.AddWithValue("@moy", moy);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                MySqlCommand com = new MySqlCommand("select * from bullet", con);

                MySqlDataAdapter adp = new MySqlDataAdapter();
                adp.SelectCommand = com;
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGridView1.DataSource = dt;

            }

            con.Close();
        }

        private void Fields_Load(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand("select * from bullet", con);

            MySqlDataAdapter adp = new MySqlDataAdapter();
            adp.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView1.DataSource = dt;




            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int selectedRow = dataGridView1.CurrentCell.RowIndex;
            int primaryKeyValue = (int)dataGridView1.Rows[selectedRow].Cells["id"].Value;
            string query = "DELETE FROM bullet WHERE id = @id";
            using (MySqlCommand command = new MySqlCommand(query, con))
            {
                command.Parameters.AddWithValue("@id", primaryKeyValue);
                con.Open();
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    dataGridView1.Rows.RemoveAt(selectedRow);
                    MessageBox.Show("Data deleted successfully.");
                    MySqlCommand com = new MySqlCommand("select * from bullet", con);

                    MySqlDataAdapter adp = new MySqlDataAdapter();
                    adp.SelectCommand = com;
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    dataGridView1.DataSource = dt;
                    con.Close();

                }
                else
                {
                    MessageBox.Show("Data not deleted.");
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Reporter report = new Reporter();
            report.Show();
        }

        int indice = -1;
        int ID2;
        bool isEditing = false;
        private void button3_Click(object sender, EventArgs e)
        {

            if (isEditing)
            {

                int selectedRow = dataGridView1.CurrentCell.RowIndex;
                int primaryKeyValue = (int)dataGridView1.Rows[selectedRow].Cells["id"].Value;
                string query = "UPDATE bullet SET f_name = @f_name, l_name = @l_name, group_name = @group, dot = @dot, php = @php, react = @react, ai = @ai, erp = @erp, ionic = @ionic, machine = @machine, sig = @sig, toe = @toe, moy = @moy WHERE id = @id";
                using (MySqlCommand command = new MySqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@id", primaryKeyValue);
                    command.Parameters.AddWithValue("@f_name", textBox1.Text);
                    command.Parameters.AddWithValue("@l_name", textBox2.Text);
                    command.Parameters.AddWithValue("@group", textBox3.Text);
                    command.Parameters.AddWithValue("@dot", float.Parse(textBox4.Text));
                    command.Parameters.AddWithValue("@php", float.Parse(textBox5.Text));
                    command.Parameters.AddWithValue("@react", float.Parse(textBox6.Text));
                    command.Parameters.AddWithValue("@ai", float.Parse(textBox7.Text));
                    command.Parameters.AddWithValue("@erp", float.Parse(textBox8.Text));
                    command.Parameters.AddWithValue("@ionic", float.Parse(textBox9.Text));
                    command.Parameters.AddWithValue("@machine", float.Parse(textBox10.Text));
                    command.Parameters.AddWithValue("@sig", float.Parse(textBox11.Text));
                    command.Parameters.AddWithValue("@toe", float.Parse(textBox12.Text));
                    float moy = (float.Parse(textBox4.Text) + float.Parse(textBox5.Text) + float.Parse(textBox6.Text) + float.Parse(textBox7.Text) + float.Parse(textBox8.Text) + float.Parse(textBox9.Text) + float.Parse(textBox10.Text) + float.Parse(textBox11.Text) + float.Parse(textBox12.Text)) / 9;
                    command.Parameters.AddWithValue("@moy", moy);
                    con.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Data updated successfully.");
                        isEditing = false;
                        button3.Text = "Edit";
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        textBox8.Text = "";
                        textBox9.Text = "";
                        textBox10.Text = "";
                        textBox11.Text = "";
                        textBox12.Text = "";
                        MySqlCommand cmd = new MySqlCommand("SELECT * FROM bullet", con);

                        MySqlDataAdapter adp = new MySqlDataAdapter();
                        adp.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        dataGridView1.DataSource = dt;

                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Data not updated.");
                    }
                    con.Close();
                }
            }
            else
            {

                int selectedRow = dataGridView1.CurrentCell.RowIndex;
                if (selectedRow >= 0)
                {
                    isEditing = true;
                    button3.Text = "Save";
                    DataGridViewRow row = dataGridView1.Rows[selectedRow];
                    ID2 = Convert.ToInt32(row.Cells["id"].Value);
                    textBox1.Text = row.Cells["f_name"].Value.ToString();
                    textBox2.Text = row.Cells["l_name"].Value.ToString();
                    textBox3.Text = row.Cells["group_name"].Value.ToString();
                    textBox4.Text = row.Cells["dot"].Value.ToString();
                    textBox5.Text = row.Cells["php"].Value.ToString();
                    textBox6.Text = row.Cells["react"].Value.ToString();
                    textBox7.Text = row.Cells["ai"].Value.ToString();
                    textBox8.Text = row.Cells["erp"].Value.ToString();
                    textBox9.Text = row.Cells["ionic"].Value.ToString();
                    textBox10.Text = row.Cells["machine"].Value.ToString();
                    textBox11.Text = row.Cells["sig"].Value.ToString();
                    textBox12.Text = row.Cells["toe"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Please select a row to edit.");
                }
            }

            }
    }
}
