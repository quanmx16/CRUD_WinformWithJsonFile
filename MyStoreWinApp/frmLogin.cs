using BusinessObject;
using DataAccess.Repository;

namespace MyStoreWinApp
{
    public partial class frmLogin : Form
    {
        public const string AdminEmail = "admin@fstore.com";
        public const string ADMIN_ROLE = "admin";
        public const string MEMBER_ROLE = "member";

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text; 
            IMemberRepository memberRepository  = new MemberRepository();
            MemberObject member = memberRepository.CheckLogin(email, password);
            if (member != null)
            {
                this.Hide();
                if (txtEmail.Text == AdminEmail)
                {
                    frmMemberManagement frmManamentForm = new frmMemberManagement(member, ADMIN_ROLE);
                    frmManamentForm.Show();
                }
                else
                {
                    frmMemberManagement frmManagementForm = new frmMemberManagement(member, MEMBER_ROLE);
                    frmManagementForm.Show();
                }
            }
            else
            {
                MessageBox.Show("Email or Password is incorrect!", "Login fail", MessageBoxButtons.OK);

            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtEmail;
            txtEmail.TabIndex = 0;
            txtPassword.TabIndex = 1;
            btnLogin.TabIndex = 3;
            btnExit.TabIndex = 4;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}