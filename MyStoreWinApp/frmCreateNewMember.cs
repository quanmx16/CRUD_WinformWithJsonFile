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
    public partial class frmCreateNewMember : Form
    {
        public MemberObject NewMember { get; set; } = null;
        public frmCreateNewMember()
        {
            InitializeComponent();
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool txtMemberName_Check(object sender, EventArgs e)
        {
            if (txtMemberName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Member Name cannot be empty", "Error");
                this.ActiveControl = txtMemberName;
                return false;
            }
            return true;
        }
        private bool txtEmail_Check(object sender, EventArgs e)
        {
            if (txtEmail.Text.Trim().Length == 0)
            {
                MessageBox.Show("Email cannot be empty", "Error");
                this.ActiveControl = txtEmail;

                return false;
            }
            else
            {
                IMemberRepository repository = new MemberRepository();
                if (repository.IsEmailHasBeenUsed(txtEmail.Text))
                {
                    MessageBox.Show("Email has been used", "Error");
                    this.ActiveControl = txtEmail;
                    return false;
                }
            }
            return true;
        }
        private bool txtPassword_Check(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim().Length == 0)
            {
                MessageBox.Show("Password cannot be empty", "Error");
                this.ActiveControl = txtPassword;
                return false;
            }
            return true;
        }
        private bool txtCity_Check(object sender, EventArgs e)
        {
            if (txtCity.Text.Trim().Length == 0)
            {
                MessageBox.Show("City cannot be empty", "Error");
                this.ActiveControl = txtCity;
                return false;
            }
            return true;
        }
        private bool txtCountry_Check(object sender, EventArgs e)
        {
            if (txtCountry.Text.Trim().Length == 0)
            {
                MessageBox.Show("Country cannot be empty", "Error");
                this.ActiveControl = txtCountry;
                return false;
            }
            return true;
        }
        private bool txtPasswordConfirm_Check(object sender, EventArgs e)
        {
            if (txtPasswordConfirm.Text.Trim().Length == 0)
            {
                MessageBox.Show("Password confirm cannot be empty", "Error");
                this.ActiveControl = txtPasswordConfirm;
                return false;
            }
            else if (txtPasswordConfirm.Text != txtPassword.Text)
            {
                MessageBox.Show("Password confirm must match", "Error");
                this.ActiveControl = txtPasswordConfirm;
                return false;

            }
            return true;
        }

        private void frmCreateNewMember_Load(object sender, EventArgs e)
        {
            this.txtEmail.TabIndex = 0;
            this.txtPassword.TabIndex = 1;
            this.txtPasswordConfirm.TabIndex = 2;
            this.txtMemberName.TabIndex = 3;
            this.txtCity.TabIndex = 4;
            this.txtCountry.TabIndex = 5;
            this.ActiveControl = txtEmail;

        }

        private void txtPasswordConfirm_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            bool notError = false;
            notError = txtEmail_Check(sender, e) && txtPassword_Check(sender, e)
                && txtPasswordConfirm_Check(sender, e) && txtMemberName_Check(sender, e)
                && txtCity_Check(sender, e) && txtCountry_Check(sender, e);
            if (notError)
            {
                IMemberRepository memberRepository = new MemberRepository();
                int memberId = memberRepository.GetLastMemberID() + 1;
                NewMember = new MemberObject(memberId, txtMemberName.Text, txtEmail.Text,
                                        txtPassword.Text, txtCity.Text, txtCountry.Text);
                IMemberRepository repository = new MemberRepository();
                repository.CreateNewMember(NewMember);
                this.DialogResult = DialogResult.OK;
            }

        }
    }
}
