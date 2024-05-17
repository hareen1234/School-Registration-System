
using MySql.Data.MySqlClient;


namespace SchoolSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Loads();
        }

        // connect with mysql workbench

        MySqlConnection conn = new MySqlConnection("Data Source =Hareen;server=localhost;uid=root;pwd=123456789123456789;database=schoolsystem");
        MySqlCommand cmd;
        MySqlDataReader reader;
        MySqlDataAdapter adapter;

        String id;
        bool mood = true;
        String sql;

        // data load for the datagideview

        public void Loads()
        {
            try
            {
                sql = "SELECT * FROM schoolsystem.datatable;";
                cmd = new MySqlCommand(sql, conn);

                conn.Open();

                reader = cmd.ExecuteReader();

                adapter = new MySqlDataAdapter(sql, conn);
                dataGridView1.Rows.Clear();

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader[0], reader[1], reader[2], reader[3]);


                }
                conn.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        public void getId(string id)
        {
            sql = "SELECT * FROM schoolsystem.datatable where id ='" + id + "'";
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                txtname.Text = reader[1].ToString();
                txtcourse.Text = reader[2].ToString();
                txtfee.Text = reader[3].ToString();
            }
            conn.Close();

        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // if mood true add the recod otherwise update

        private void Savebtn_Click(object sender, EventArgs e)
        {
            String name = txtname.Text;
            String course = txtcourse.Text;
            String fee = txtfee.Text;

            if (mood == true)
            {
                sql = "insert into datatable(name,Course,fee) values(@name,@Course,@fee)";
                conn.Open();
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@Course", course);
                cmd.Parameters.AddWithValue("@fee", fee);

                MessageBox.Show("Record was Added.");
                cmd.ExecuteNonQuery();

                txtname.Clear();
                txtcourse.Clear();
                txtfee.Clear();
                txtname.Focus();

            }
            else
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update datatable set name=@name, Course= @Course,fee= @fee where id=@id";
                conn.Open();
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@Course", course);
                cmd.Parameters.AddWithValue("@fee", fee);
                cmd.Parameters.AddWithValue("@id", id);

                MessageBox.Show("Record Updated.");
                cmd.ExecuteNonQuery();

                txtname.Clear();
                txtcourse.Clear();
                txtfee.Clear();
                txtname.Focus();
                Savebtn.Text = "Save";
                mood = true;

            }
            conn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0)
            {
                mood = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getId(id);
                Savebtn.Text = "Edit";

            }
            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                mood = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from datatable where id=@id ";
                conn.Open();
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Record Deleted.");
                conn.Close();

            }
        }

        private void Refreshbtn_Click(object sender, EventArgs e)
        {
            Loads();
        }

        private void Clearbtn_Click(object sender, EventArgs e)
        {
            txtname.Clear();
            txtcourse.Clear();
            txtfee.Clear();
            txtname.Focus();
            Savebtn.Text = "Save";
            mood = true;
        }
    }
}
