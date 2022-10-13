using BusinessObject;
using DataAccess;
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
    public partial class frmMemberManagement : Form
    {
        public MemberObject LoggedInMember { get; set; }
        public string Role { get; set; }
        public List<MemberObject> CurrentMemberInListView { get; set; } = new List<MemberObject>();
        public frmMemberManagement(MemberObject member, string role)
        {
            LoggedInMember = member;
            Role = role;
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Exit", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                Environment.Exit(0);
            }

        }


        private void frmMemberManagement_Load(object sender, EventArgs e)
        {


            txtMemberID.Enabled = false;
            txtMemberName.Enabled = false;
            txtEmail.Enabled = false;
            txtCity.Enabled = false;
            txtCountry.Enabled = false;
            txtPassword.Enabled = false;

            if (Role == "admin")
            {
                btnCreate.Visible = true;
                btnDelete.Visible = true;
                boxSearch.Visible = true;
                boxFilter.Visible = true;
                listView.Visible = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                rdByName.Checked = true;

                IMemberRepository memberRepository = new MemberRepository();
                CurrentMemberInListView = memberRepository.GetAllMember();
                Filter_Load();
                listView.Items.Clear();
                ListView_Load();



            }
            else
            {
                txtMemberID.Text = LoggedInMember.MemberID.ToString();
                txtMemberName.Text = LoggedInMember.MemberName;
                txtEmail.Text = LoggedInMember.Email;
                txtCity.Text = LoggedInMember.City;
                txtCountry.Text = LoggedInMember.Country;
                txtPassword.Text = LoggedInMember.Password;

                btnCreate.Visible = false;
                btnDelete.Visible = false;
                btnDelete.Enabled = false;
                btnLoadAll.Visible = false;
                btnLoadAll.Enabled = false;

                boxSearch.Visible = false;
                boxFilter.Visible = false;
                listView.Visible = false;
            }

        }
        private void Filter_Load()
        {
            countryBox.Items.Clear();
            cityBox.Items.Clear();

            //config filter
            IMemberRepository repository = new MemberRepository();

            //country
            countryBox.Items.Add("All");
            HashSet<string> countries = repository.GetAllMemberCountry(CurrentMemberInListView);
            foreach (string country in countries)
            {
                countryBox.Items.Add(country);
            }
            countryBox.Text = "All";

            //city
            cityBox.Items.Add("All");
            HashSet<string> cites = repository.GetAllMemberCity(CurrentMemberInListView);
            foreach (string city in cites)
            {
                cityBox.Items.Add(city);
            }
            cityBox.Text = "All";

        }
        private void ListView_Load()
        {
            IMemberRepository repository = new MemberRepository();
            List<MemberObject> members = repository.GetAllMemberSortedByName();
            CurrentMemberInListView = members;
            for (int i = 0; i < members.Count; i++)
            {
                MemberObject member = members[i];
                if (member.Email != "admin@fstore.com")
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = member.MemberID.ToString();
                    item.SubItems.Add(member.MemberName);
                    item.SubItems.Add(member.Email);
                    item.SubItems.Add(member.Password);
                    item.SubItems.Add(member.City);
                    item.SubItems.Add(member.Country);
                    listView.Items.Add(item);
                }

            }
            txtMemberID.Text = "";
            txtMemberName.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtCountry.Text = "";
            txtCity.Text = "";
            txtSearchValue.Text = "";

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }
        private void ListView_Load(List<MemberObject> members)
        {
            
            for (int i = 0; i < members.Count; i++)
            {
                MemberObject member = members[i];
                if (member.Email != "admin@fstore.com")
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = member.MemberID.ToString();
                    item.SubItems.Add(member.MemberName);
                    item.SubItems.Add(member.Email);
                    item.SubItems.Add(member.Password);
                    item.SubItems.Add(member.City);
                    item.SubItems.Add(member.Country);
                    listView.Items.Add(item);

                }

            }
            txtMemberID.Text = "";
            txtMemberName.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtCountry.Text = "";
            txtCity.Text = "";

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            IMemberRepository repository = new MemberRepository();
            if (this.Role == "member")
            {
                frmUpdateInformation updateForm = new frmUpdateInformation(this.LoggedInMember);
                DialogResult result = updateForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    updateForm.Close();
                    LoggedInMember = repository.GetMemberById(LoggedInMember.MemberID);
                    frmMemberManagement_Load(sender, e);
                }

            }
            else if (this.Role == "admin")
            {
                if (listView.SelectedItems.Count > 0)
                {
                    var item = listView.SelectedItems[0];
                    int memberId = Convert.ToInt32(item.SubItems[0].Text);
                    MemberObject updatedMember = repository.GetMemberById(memberId);
                    if (updatedMember != null)
                    {
                        frmUpdateInformation updateForm = new frmUpdateInformation(updatedMember);
                        DialogResult result = updateForm.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            updateForm.Close();
                            IMemberRepository memberRepository = new MemberRepository();
                            CurrentMemberInListView = memberRepository.GetAllMember();
                            Filter_Load();
                            listView.Items.Clear();
                            ListView_Load();

                        }

                    }
                }

            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmCreateNewMember createForm = new frmCreateNewMember();
            DialogResult result = createForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                createForm.Close();
                //reload listview
                IMemberRepository memberRepository = new MemberRepository();
                CurrentMemberInListView = memberRepository.GetAllMember();
                Filter_Load();
                listView.Items.Clear();
                ListView_Load();

            }

        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                var selectedItem = listView.SelectedItems[0];
                txtMemberID.Text = selectedItem.Text;
                txtMemberName.Text = selectedItem.SubItems[1].Text;
                txtEmail.Text = selectedItem.SubItems[2].Text;
                txtPassword.Text = selectedItem.SubItems[3].Text;
                txtCity.Text = selectedItem.SubItems[4].Text;
                txtCountry.Text = selectedItem.SubItems[5].Text;


                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure?", "Delete Member", MessageBoxButtons.OKCancel);
            if (confirm == DialogResult.OK)
            {
                if (listView.SelectedItems.Count > 0)
                {
                    var item = listView.SelectedItems[0];
                    int memberId = Convert.ToInt32(item.SubItems[0].Text);
                    IMemberRepository repository = new MemberRepository();
                    bool deleteResult = repository.DeleteMemberById(memberId);
                    if (deleteResult)
                    {
                        IMemberRepository memberRepository = new MemberRepository();
                        CurrentMemberInListView = memberRepository.GetAllMember();
                        Filter_Load();
                        listView.Items.Clear();
                        ListView_Load();


                    }
                }
            }

        }

        private void filter_SelectedValueChanged(object sender, EventArgs e)
        {
            listView.Items.Clear();
            IMemberRepository repository = new MemberRepository();
            if (countryBox.Text == "All" && cityBox.Text == "All")
            {
                this.ListView_Load(CurrentMemberInListView);
            }
            else if (countryBox.Text != "All" && cityBox.Text == "All")
            {
                List<MemberObject> members = repository.GetMembersByCountry(countryBox.Text, CurrentMemberInListView);
                this.ListView_Load(members);
            }
            else if (countryBox.Text == "All" && cityBox.Text != "All")
            {
                List<MemberObject> members = repository.GetMembersByCity(cityBox.Text, CurrentMemberInListView);
                this.ListView_Load(members);
            }
            else if (countryBox.Text != "All" && cityBox.Text != "All")
            {
                List<MemberObject> members = repository.GetMembersByCountryAndCity(countryBox.Text, cityBox.Text, CurrentMemberInListView);
                this.ListView_Load(members);
            }
        }

        private void txtSearchValue_TextChanged(object sender, EventArgs e)
        {
            if (rdById.Checked == true)
            {
                try
                {
                    if (txtSearchValue.Text.Trim().Length > 0)
                    {
                        Convert.ToInt32(txtSearchValue.Text.Trim());
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Id must be a number!");
                    this.txtSearchValue.Focus();
                    this.txtSearchValue.Text = this.txtSearchValue.Text.Substring(0, txtSearchValue.Text.Length - 1);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.txtSearchValue.Text.Trim().Length == 0)
            {
                MessageBox.Show("Does not have any search value!", "Error");
            }
            else if (rdByName.Checked == true)
            {
                IMemberRepository memberRepository = new MemberRepository();
                List<MemberObject> members = memberRepository.SearchMemberByName(this.txtSearchValue.Text.Trim());
                CurrentMemberInListView = members;
                Filter_Load(members);
                listView.Items.Clear();
                ListView_Load(members);
            }
            else if (rdById.Checked == true)
            {
                IMemberRepository memberRepository = new MemberRepository();
                List<MemberObject> members = memberRepository.SearchMemberById(Convert.ToInt32(this.txtSearchValue.Text.Trim()));
                CurrentMemberInListView = members;
                Filter_Load(members);
                listView.Items.Clear();
                ListView_Load(members);

            }
        }
        private void Filter_Load(List<MemberObject> members)
        {
            countryBox.Items.Clear();
            cityBox.Items.Clear();

            //config filter

            HashSet<string> countries = new HashSet<string>();
            HashSet<string> cities = new HashSet<string>();

            for (int i = 0; i < members.Count; i++)
            {
                if (members[i].Email != "admin@fstore.com")
                {
                    countries.Add(members[i].Country);
                    cities.Add(members[i].City);
                }

            }


            //country
            countryBox.Items.Add("All");
            foreach (string country in countries)
            {
                countryBox.Items.Add(country);
            }
            countryBox.Text = "All";
            //city
            cityBox.Items.Add("All");
            foreach (string city in cities)
            {
                cityBox.Items.Add(city);
            }
            cityBox.Text = "All";
        }

        private void rdById_CheckedChanged(object sender, EventArgs e)
        {
            txtSearchValue.Text = "";
            txtSearchValue.Focus();
        }

        private void rdByName_CheckedChanged(object sender, EventArgs e)
        {
            txtSearchValue.Text = "";
            txtSearchValue.Focus();

        }

        private void btnLoadAll_Click(object sender, EventArgs e)
        {
            IMemberRepository memberRepository = new MemberRepository();
            CurrentMemberInListView = memberRepository.GetAllMember();
            Filter_Load();
            listView.Items.Clear();
            ListView_Load();
        }
    }
}
