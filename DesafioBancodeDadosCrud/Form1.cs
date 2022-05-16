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

namespace DesafioBancodeDadosCrud
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnAtualizar.Enabled = false;
            btnDeletar.Enabled = false;
            
            string con = @"Data Source=DESKTOP-BVK8T3K\SQLEXPRESS;Initial Catalog=Revisao;Integrated Security=True";
            SqlConnection sqlCon = new SqlConnection(con);

            sqlCon.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Cliente", sqlCon);
            cmd.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter();

            DataSet ds = new DataSet();

            da.SelectCommand = cmd;

            da.Fill(ds);

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.Tables[0].TableName;

            sqlCon.Close();

        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            string con = @"Data Source=DESKTOP-BVK8T3K\SQLEXPRESS;Initial Catalog=Revisao;Integrated Security=True";
            SqlConnection sqlCon = new SqlConnection(con);

            string sql = @"INSERT INTO Cliente(Nome, Localizacao, Email)
                                    VALUES('" + txtNome.Text + "', '" + txtLocalizacao.Text + "', '" + txtEmail.Text + "')";

            SqlCommand cmd = new SqlCommand(sql, sqlCon);

            cmd.CommandType = CommandType.Text;
            sqlCon.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Inserido com Sucesso!");
                    LimparCampos();
                    Form1_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR!" + ex);
                throw;
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtLocalizacao.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            string sql = @"UPDATE Cliente SET Nome = '" + txtNome.Text + "', Localizacao = '" + txtLocalizacao.Text + "', Email = '" + txtEmail.Text + "' WHERE ClienteID = '" + txtID.Text + "'";
            string con = @"Data Source=DESKTOP-BVK8T3K\SQLEXPRESS;Initial Catalog=Revisao;Integrated Security=True";


            SqlConnection sqlCon = new SqlConnection(con);

            SqlCommand cmd = new SqlCommand(sql, sqlCon);

            cmd.CommandType = CommandType.Text;
            sqlCon.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Atualizado com Sucesso!");
                    LimparCampos();
                    Form1_Load(sender, e);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex);
                throw;
            }
            finally
            {
                sqlCon.Close();
            }



        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            string sql = @"DELETE FROM Cliente WHERE ClienteID = '" + txtID.Text + "'";

            string con = @"Data Source=DESKTOP-BVK8T3K\SQLEXPRESS;Initial Catalog=Revisao;Integrated Security=True";

            SqlConnection sqlCon = new SqlConnection(con);
            SqlCommand cmd = new SqlCommand(sql, sqlCon);

            cmd.CommandType = CommandType.Text;
            sqlCon.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    //const string message = "Deletar este Cadastro?";
                    //var result = MessageBox.Show(message, "",MessageBoxButtons.YesNo,
                    //                             MessageBoxIcon.Question);
                    MessageBox.Show("Deletado com Sucesso");
                    LimparCampos();
                    Form1_Load(sender, e);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR!" + ex);
                throw;
            }
            finally
            {
                sqlCon.Close();
            }
        }
        public void LimparCampos()
        {
            txtNome.Text = "";
            txtLocalizacao.Text = "";
            txtEmail.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnDeletar.Enabled = true;
            btnAtualizar.Enabled = true;
        }
    }
}
