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

namespace WindowsFormsApp1
{
    public partial class frmAddNewSeller : Form
    {
        DBConnect dbCon = new DBConnect();
        public frmAddNewSeller()
        {
            InitializeComponent();
        }

        private void frmAddNewSeller_Load(object sender, EventArgs e)
        {
            lblSellerID.Visible = false;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnAdd.Visible = true;
            BindSeller();
        }

        private void lblCatID_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSellername.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter Seller Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSellername.Focus();
                    return;
                }
                else if (txtPassword.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Focus();
                    return;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("select SellerName from tblSeller where SellerName=@SellerName", dbCon.GetCon());
                    cmd.Parameters.AddWithValue("@SellerName", txtSellername.Text);
                    dbCon.OpenCon();
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        MessageBox.Show("Seller Name Already Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtClear();
                    }
                    else
                    {
                        cmd = new SqlCommand("spSellerInsert", dbCon.GetCon());
                        cmd.Parameters.AddWithValue("@SellerName", txtSellername.Text);
                        cmd.Parameters.AddWithValue("@SellerAge", Convert.ToInt32(txtAge.Text));
                        cmd.Parameters.AddWithValue("@SellerPhone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@SellerPass", txtPassword.Text);
                        cmd.CommandType = CommandType.StoredProcedure;
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Seller inserted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindSeller();
                        }
                    }
                    dbCon.CloseCon();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void txtClear()
        {
            txtSellername.Clear();
            txtAge.Clear();
            txtPassword.Clear();
            txtPhone.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblSellerID.Text == String.Empty)
                {
                    MessageBox.Show("Please select SellerID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (txtSellername.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter Seller Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSellername.Focus();
                    return;
                }
                else if (txtPassword.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Focus();
                    return;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("select SellerName from tblSeller where SellerName=@SellerName", dbCon.GetCon());
                    cmd.Parameters.AddWithValue("@SellerName", txtSellername.Text);
                    dbCon.OpenCon();
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        MessageBox.Show("Seller Name Already Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtClear();
                    }
                    else
                    {
                        cmd = new SqlCommand("spSellerUpadte", dbCon.GetCon());
                        cmd.Parameters.AddWithValue("@SellerID", Convert.ToInt32(lblSellerID.Text));
                        cmd.Parameters.AddWithValue("@SellerName", txtSellername.Text);
                        cmd.Parameters.AddWithValue("@SellerAge", Convert.ToInt32(txtAge.Text));
                        cmd.Parameters.AddWithValue("@SellerPhone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@SellerPass", txtPassword.Text);
                        cmd.CommandType = CommandType.StoredProcedure;
                        int i = cmd.ExecuteNonQuery();
                        dbCon.CloseCon();
                        if (i > 0)
                        {
                            MessageBox.Show("Seller Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindSeller();
                            btnUpdate.Visible = false;
                            btnDelete.Visible = false;
                            btnAdd.Visible = true;
                            lblSellerID.Visible = false;
                        }
                        else
                        {
                            MessageBox.Show("Update Failed...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtClear();
                        }
                    }
                    dbCon.CloseCon();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblSellerID.Text == String.Empty)
                {
                    MessageBox.Show("Please select SellerID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (lblSellerID.Text != String.Empty)
                {
                    if (DialogResult.Yes == MessageBox.Show("Do you want to delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        SqlCommand cmd = new SqlCommand("spSellerDelete", dbCon.GetCon());
                        cmd.Parameters.AddWithValue("@SellerID", Convert.ToInt32(lblSellerID.Text));
                        cmd.CommandType = CommandType.StoredProcedure;
                        dbCon.OpenCon();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Seller Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindSeller();
                            btnUpdate.Visible = false;
                            btnDelete.Visible = false;
                            lblSellerID.Visible = false;
                            btnAdd.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Delete Failed...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtClear();
                        }
                        dbCon.CloseCon();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BindSeller()
        {
            SqlCommand cmd = new SqlCommand("select * from tblSeller", dbCon.GetCon());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            dbCon.OpenCon();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btnUpdate.Visible = true;
            btnDelete.Visible = true;
            lblSellerID.Visible = true;
            btnAdd.Visible = false;

            lblSellerID.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            txtSellername.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
            txtAge.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
            txtPhone.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
            txtPassword.Text = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
        }
    }
}
