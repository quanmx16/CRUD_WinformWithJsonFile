using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStoreWinApp
{
    public partial class frmUpdateInformation : Form
    {
        public MemberObject UpdatedMember { get; set; }
        public MemberObject NewMemberInformation { get; set; }

        public frmUpdateInformation()
        {
            InitializeComponent();
        }
        public frmUpdateInformation(MemberObject member)
        {
            UpdatedMember = member;
            InitializeComponent();
        }

        private void frmUpdateInformation_Load(object sender, EventArgs e)
        {
            txtMemberID.Text = UpdatedMember.MemberID.ToString();
            txtMemberName.Text = UpdatedMember.MemberName;
            txtEmail.Text = UpdatedMember.Email;
            txtCity.Text = UpdatedMember.City;
            txtCountry.Text = UpdatedMember.Country;
            txtPassword.Text = UpdatedMember.Password;
            txtMemberID.Enabled = false;
        }

     

        private void txtMemberName_Leave(object sender, EventArgs e)
        {
            if (txtMemberName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Member Name cannot be empty", "Error");
                this.ActiveControl = txtMemberName;
            }
        }
        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (txtEmail.Text.Trim().Length == 0)
            {
                MessageBox.Show("Email cannot be empty", "Error");
                this.ActiveControl = txtEmail;
            }
            else
            {
                IMemberRepository repository = new MemberRepository();
                if (repository.IsEmailHasBeenUsed(txtEmail.Text))
                {
                    MessageBox.Show("Email has been used", "Error");
                    this.ActiveControl = txtEmail;

                }
            }
        }
        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim().Length == 0)
            {
                MessageBox.Show("Password cannot be empty", "Error");
                this.ActiveControl = txtPassword;

            }
        }
        private void txtCity_Leave(object sender, EventArgs e)
        {
            if (txtCity.Text.Trim().Length == 0)
            {
                MessageBox.Show("City cannot be empty", "Error");
                this.ActiveControl = txtCity;

            }
        }
        private void txtCountry_Leave(object sender, EventArgs e)
        {
            if (txtCountry.Text.Trim().Length == 0)
            {
                MessageBox.Show("Country cannot be empty", "Error");
                this.ActiveControl = txtCountry;

            }
        }
        private void txtMemberName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
