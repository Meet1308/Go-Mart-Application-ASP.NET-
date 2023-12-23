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
    public partial class AddProduct : Form
    {
        DBConnect dbCon = new DBConnect();
        public AddProduct()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void AddProduct_Load(object sender, EventArgs e)
        {
            BindCategory();
            BindProductList();
            lblProdID.Visible = false;
            btnUpdatePro.Visible = false;
            btnDeletePro.Visible = false;
            btnAddPro.Visible = true;
            SearchBy_Category();
        }

        private void BindCategory()
        {
            SqlCommand cmd = new SqlCommand("spGetCategory", dbCon.GetCon());
            cmd.CommandType = CommandType.StoredProcedure;
            dbCon.OpenCon();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbCategory.DataSource = dt;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CatID";
            dbCon.CloseCon();
        }

        private void SearchBy_Category()
        {
            SqlCommand cmd = new SqlCommand("spGetCategory", dbCon.GetCon());
            cmd.CommandType = CommandType.StoredProcedure;
            dbCon.OpenCon();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbSearch.DataSource = dt;
            cmbSearch.DisplayMember = "CategoryName";
            cmbSearch.ValueMember = "CatID";
            dbCon.CloseCon();
        }

        private void btnAddPro_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProductName.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter Product Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtProductName.Focus();
                    return;
                }
                else if (Convert.ToInt32(txtPrice.Text) < 0 || txtPrice.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter Price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrice.Focus();
                    return;
                }
                else if (txtQuntity.Text == String.Empty ||  Convert.ToInt32(txtQuntity.Text) < 0)
                {
                    MessageBox.Show("Please Enter Quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrice.Focus();
                    return;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("spCheckDuplicateProduct", dbCon.GetCon());
                    cmd.Parameters.AddWithValue("@ProdName", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@ProdCatID",cmbCategory.SelectedValue);
                    cmd.CommandType = CommandType.StoredProcedure;
                    dbCon.OpenCon();
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        MessageBox.Show("Product Name Already Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtClear();
                    }
                    else
                    {
                        cmd = new SqlCommand("spInsertProduct", dbCon.GetCon());
                        cmd.Parameters.AddWithValue("@ProdName", txtProductName.Text);
                        cmd.Parameters.AddWithValue("@ProdCatID", cmbCategory.SelectedValue);
                        cmd.Parameters.AddWithValue("@ProdPrice", Convert.ToDecimal(txtPrice.Text));
                        cmd.Parameters.AddWithValue("@ProdQty",Convert.ToInt32(txtQuntity.Text));
                        cmd.CommandType = CommandType.StoredProcedure;
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Product inserted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindProductList();
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

        private void BindProductList()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spGetAllProductList", dbCon.GetCon());
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
                dbCon.OpenCon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
        }

        private void txtClear()
        {
            txtProductName.Clear();
            txtPrice.Clear();
            txtQuntity.Clear();
        }

        private void btnUpdatePro_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblProdID.Text == "" &&  txtProductName.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter ProductID and Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtProductName.Focus();
                    return;
                }
                else if (txtPrice.Text == String.Empty && Convert.ToInt32(txtPrice.Text) >= 0)
                {
                    MessageBox.Show("Please Enter Price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrice.Focus();
                    return;
                }
                else if (txtQuntity.Text == String.Empty && Convert.ToInt32(txtQuntity.Text) >= 0)
                {
                    MessageBox.Show("Please Enter Quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrice.Focus();
                    return;
                }
                else
                {
                    //SqlCommand cmd = new SqlCommand("spCheckDuplicateProduct", dbCon.GetCon());
                    //cmd.Parameters.AddWithValue("@ProdName", txtProductName.Text);
                    //cmd.Parameters.AddWithValue("@ProdCatID", cmbCategory.SelectedValue);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //dbCon.OpenCon();
                    //var result = cmd.ExecuteScalar();
                    //if (result != null)
                    //{
                    //    MessageBox.Show("Product Name Already Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    txtClear();
                    //}
                    //else
                    //{
                        
                    //}
                    SqlCommand cmd = new SqlCommand("spUpdateProduct", dbCon.GetCon());
                    cmd.Parameters.AddWithValue("@ProdName", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@ProdCatID", cmbCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@ProdPrice", Convert.ToDecimal(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@ProdQty", Convert.ToInt32(txtQuntity.Text));
                    cmd.Parameters.AddWithValue("@ProdID", Convert.ToInt32(lblProdID.Text));
                    cmd.CommandType = CommandType.StoredProcedure;
                    dbCon.OpenCon();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Product Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtClear();
                        BindProductList();
                        lblProdID.Visible = false;
                        btnAddPro.Visible = true;
                        btnUpdatePro.Visible = false;
                        btnDeletePro.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Updation failed...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    dbCon.CloseCon();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            try
            {
                btnUpdatePro.Visible = true;
                btnDeletePro.Visible = true;
                lblProdID.Visible = true;
                btnAddPro.Visible = false;

                lblProdID.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                txtProductName.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                cmbCategory.SelectedValue = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                txtPrice.Text = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
                txtQuntity.Text = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnDeletePro_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblProdID.Text == String.Empty)
                {
                    MessageBox.Show("Please select ProductID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (lblProdID.Text != String.Empty)
                {
                    if (DialogResult.Yes == MessageBox.Show("Do you want to delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        SqlCommand cmd = new SqlCommand("spDeleteProduct", dbCon.GetCon());
                        cmd.Parameters.AddWithValue("@ProdID", Convert.ToInt32(lblProdID.Text));
                        cmd.CommandType = CommandType.StoredProcedure;
                        dbCon.OpenCon();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Product Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindProductList();
                            btnUpdatePro.Visible = false;
                            btnDeletePro.Visible = false;
                            lblProdID.Visible = false;
                            btnAddPro.Visible = true;
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

        private void cmbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Searched_ProductList();
        }
        private void Searched_ProductList()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spGetAllProductList_SearchByCat", dbCon.GetCon());
                cmd.Parameters.AddWithValue("@ProdCatID",cmbSearch.SelectedValue);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
                dbCon.OpenCon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtPrice.Clear();
            txtProductName.Clear();
            txtQuntity.Clear();
       
        }
    }
}
