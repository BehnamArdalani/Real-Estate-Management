using Real_Estate_Management.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Real_Estate_Management.GUI
{
    public partial class Profile : Form
    {
        User loggedInUser;
        public Profile(User user)
        {
            InitializeComponent();

            lblRole.Text = user.IsAdmin ? "Admin" : "Agent";
            lblFirstName.Text = user.FirstName;
            txtProfileFirstName.Text = user.FirstName;
            txtProfileLastName.Text = user.LastName;
            txtProfilePhone.Text = user.Phone;
            txtProfilePassword.Text = user.Password;
            loggedInUser = user;

            btnContract.Enabled = user.IsAdmin;
            btnUser.Enabled = user.IsAdmin;

            if (Store.ReadUserPhoto(user.Id) is not null)
                userPhoto.BackgroundImage = Store.ReadUserPhoto(user.Id);
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            pSelected.Location = new Point(btnProfile.Location.X + btnProfile.Width, btnProfile.Location.Y - 3);
            
            pProfile.Show();
            pHouse.Hide();
            pOwner.Hide();
            pContract.Hide();
            pUser.Hide();

            btnProfile.BackColor = Color.FromArgb(163, 173, 186);
            btnHouse.BackColor = Color.White;
            btnOwner.BackColor = Color.White;
            btnContract.BackColor = Color.White;
            btnUser.BackColor = Color.White;

            txtProfileFirstName.Text = loggedInUser.FirstName;
            txtProfileLastName.Text = loggedInUser.LastName;
            txtProfilePhone.Text = loggedInUser.Phone;
            txtProfilePassword.Text = loggedInUser.Password;

        }

        private void btnHouse_Click(object sender, EventArgs e)
        {
            pSelected.Location = new Point(btnHouse.Location.X + btnHouse.Width, btnHouse.Location.Y - 3);
            
            pProfile.Hide();
            pHouse.Show();
            pOwner.Hide();
            pContract.Hide();
            pUser.Hide();

            cbHouseOwner.DataSource = Store.ReadAllSellers();
            cbHouseOwner.SelectedIndex = -1;

            RefreshGrid(pHouse.Name);

            btnProfile.BackColor = Color.White;
            btnHouse.BackColor = Color.FromArgb(163, 173, 186);
            btnOwner.BackColor = Color.White;
            btnContract.BackColor = Color.White;
            btnUser.BackColor = Color.White;
        }

        private void btnOwner_Click(object sender, EventArgs e)
        {
            pSelected.Location = new Point(btnOwner.Location.X + btnOwner.Width, btnOwner.Location.Y - 3);

            pProfile.Hide();
            pHouse.Hide();
            pOwner.Show();
            pContract.Hide();
            pUser.Hide();
            
            RefreshGrid(pOwner.Name);

            btnProfile.BackColor = Color.White;
            btnHouse.BackColor = Color.White;
            btnOwner.BackColor = Color.FromArgb(163, 173, 186);
            btnContract.BackColor = Color.White;
            btnUser.BackColor = Color.White;
        }

        private void btnContract_Click(object sender, EventArgs e)
        {
            pSelected.Location = new Point(btnContract.Location.X + btnContract.Width, btnContract.Location.Y - 3);

            pProfile.Hide();
            pHouse.Hide();
            pOwner.Hide();
            pContract.Show();
            pUser.Hide();

            RefreshGrid(pContract.Name);

            cbContractAgent.DataSource = Store.ReadAllUsers().FindAll(user => user.IsAdmin == false);
            cbContractAgent.SelectedIndex = -1;

            btnProfile.BackColor = Color.White;
            btnHouse.BackColor = Color.White;
            btnOwner.BackColor = Color.White;
            btnContract.BackColor = Color.FromArgb(163, 173, 186);
            btnUser.BackColor = Color.White;
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            pSelected.Location = new Point(btnUser.Location.X + btnUser.Width, btnUser.Location.Y - 3);

            pProfile.Hide();
            pHouse.Hide();
            pOwner.Hide();
            pContract.Hide();
            pUser.Show();

            RefreshGrid(pUser.Name);

            btnProfile.BackColor = Color.White;
            btnHouse.BackColor = Color.White;
            btnOwner.BackColor = Color.White;
            btnContract.BackColor = Color.White;
            btnUser.BackColor = Color.FromArgb(163, 173, 186);
        }

        private void btnHouseSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool isNew = true;
                int lastHouseId = 10000;
                List<House> houses = Store.ReadAllHouses();
                foreach (House house in houses)
                {
                    if (house.Id == txtHouseId.Text)
                    {
                        isNew = false;
                        house.Street = txtHouseStreet.Text;
                        house.Number = txtHouseNumber.Text;
                        house.City = txtHouseCity.Text;
                        house.Province = txtHouseProvince.Text;
                        house.Country = txtHouseCountry.Text;
                        house.PostalCode = txtHousePostalCode.Text;
                        house.LivingArea = Convert.ToDouble(txtHouseLivingArea.Text);
                        house.BedRooms = Convert.ToInt32(txtHouseBedRooms.Text);
                        house.OwnerId = cbHouseOwner.Text.Substring(cbHouseOwner.Text.Length - 8);
                        house.Price = Convert.ToDouble(txtHousePrice.Text);
                    }
                    if (Convert.ToInt32(house.Id.Substring(3, 5)) > lastHouseId)
                        lastHouseId = Convert.ToInt32(house.Id.Substring(3, 5));
                }
                if (isNew)
                    houses.Add(new House("Hse" + Convert.ToString(lastHouseId + 1), txtHouseStreet.Text, txtHouseNumber.Text, txtHouseCity.Text, txtHouseProvince.Text, txtHouseCountry.Text, txtHousePostalCode.Text, Convert.ToDouble(txtHouseLivingArea.Text), Convert.ToInt32(txtHouseBedRooms.Text), Convert.ToDouble(txtHousePrice.Text), cbHouseOwner.Text.Substring(cbHouseOwner.Text.Length - 8)));
                Store.SaveAllHouses(houses);

                btnHouseDelete.Enabled = false;

                RefreshGrid(pHouse.Name);

                ResetForm(pHouse.Name);
            }
            catch (Exception ex)
            { 
                MessageBox.Show("Invalid input!\n\n" + ex.Message, "Error"); 
            }
            
        }

        private void dgvHouse_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtHouseId.Text = dgvHouse.CurrentRow.Cells["id"].Value.ToString();
            txtHouseStreet.Text = dgvHouse.CurrentRow.Cells["street"].Value.ToString();
            txtHouseNumber.Text = dgvHouse.CurrentRow.Cells["number"].Value.ToString();
            txtHouseCity.Text = dgvHouse.CurrentRow.Cells["city"].Value.ToString();
            txtHouseProvince.Text = dgvHouse.CurrentRow.Cells["province"].Value.ToString();
            txtHouseCountry.Text = dgvHouse.CurrentRow.Cells["country"].Value.ToString();
            txtHousePostalCode.Text = dgvHouse.CurrentRow.Cells["postalCode"].Value.ToString();
            txtHouseLivingArea.Text = dgvHouse.CurrentRow.Cells["livingArea"].Value.ToString();
            txtHouseBedRooms.Text = dgvHouse.CurrentRow.Cells["bedRooms"].Value.ToString();
            cbHouseOwner.SelectedText = dgvHouse.CurrentRow.Cells["ownerId"].Value.ToString();
            txtHousePrice.Text = dgvHouse.CurrentRow.Cells["price"].Value.ToString();

            btnHouseDelete.Enabled = true;

        }

        private void RefreshGrid(string panelName)
        {
            var source = new BindingSource();

            switch (panelName) {
                case "pHouse":
                    List<House> houses = new List<House>();
                    houses = Store.ReadAllHouses();
                    source.DataSource = houses;
                    dgvHouse.DataSource = source;
                    dgvHouse.Columns["id"].Visible = false;
                    break;
                case "pOwner":
                    List<Customer> sellers = new List<Customer>();
                    sellers = Store.ReadAllSellers();
                    source.DataSource = sellers;
                    dgvOwner.DataSource = source;
                    dgvOwner.Columns["id"].Visible = false;
                    //dgvHouse.Columns["HouseId"].Visible = false;
                    break;
                case "pContract":
                    List<Contract> contracts = new List<Contract>();
                    contracts = Store.ReadAllContracts();
                    source.DataSource = contracts;
                    dgvContract.DataSource = source;
                    dgvContract.Columns["id"].Visible = false;
                    dgvContract.Columns["sellerId"].Visible = false;
                    dgvContract.Columns["buyerId"].Visible = false;
                    dgvContract.Columns["houseId"].Visible = false;
                    dgvContract.Columns["AgentId"].Visible = false;
                    break;
                case "pUser":
                    List<User> users = new List<User>();
                    users = Store.ReadAllUsers();
                    source.DataSource = users;
                    dgvUser.DataSource = source;
                    dgvUser.Columns["id"].Visible = false;
                    dgvUser.Columns["Password"].Visible = false;
                    break;
                default:
                    break;

            }
        }
        private void ResetForm(string panelName)
        {
            foreach (Panel pnl in Controls.OfType<Panel>())
            {
                if(pnl.Name == panelName)
                {
                    foreach (TextBox tb in pnl.Controls.OfType<TextBox>())
                    {
                        tb.Clear();
                    }
                    foreach(DateTimePicker dtp in pnl.Controls.OfType<DateTimePicker>())
                    {
                        dtp.Value = DateTime.Now;
                    }
                    foreach(ComboBox cb in pnl.Controls.OfType<ComboBox>())
                    {
                        cb.SelectedIndex = -1;
                        cb.SelectedText = "";
                        cb.Text = "";
                        cb.SelectedItem = null;
                    }
                    foreach(RadioButton rb in pnl.Controls.OfType<RadioButton>())
                    {
                        rb.Checked = false;
                    }
                }
                
            }
        }
       
        private void btnHouseDelete_Click(object sender, EventArgs e)
        {
            if (txtHouseId.Text.Trim().Length > 0)
            {
                List<House> houses = Store.ReadAllHouses();
                houses.Remove(houses[houses.FindIndex(h => h.Id == txtHouseId.Text)]);
                Store.SaveAllHouses(houses);

                RefreshGrid(pHouse.Name);

                ResetForm(pHouse.Name);
            }
            else
                btnHouseDelete.Enabled = false;
        }

        private void btnHouseReset_Click(object sender, EventArgs e)
        {
            ResetForm(pHouse.Name);
            btnHouseDelete.Enabled = false;
        }

        private void btnHouseShowAll_Click(object sender, EventArgs e)
        {
            btnHouseDelete.Enabled = false;
            txtHouseSearch.Clear();

        }

        private void txtHouseSearch_TextChanged(object sender, EventArgs e)
        {
            List<House> houses = new List<House>();
            houses = Store.ReadAllHouses();
            var source = new BindingSource();
            source.DataSource = houses.FindAll(house => house.Street.ToLower().Contains(txtHouseSearch.Text.ToLower()) || house.Number.ToLower().Contains(txtHouseSearch.Text.ToLower()) || house.City.ToLower().Contains(txtHouseSearch.Text.ToLower()) || house.Province.ToLower().Contains(txtHouseSearch.Text.ToLower()) || house.Country.ToLower().Contains(txtHouseSearch.Text.ToLower()) || house.PostalCode.ToLower().Contains(txtHouseSearch.Text.ToLower()));
            dgvHouse.DataSource = source;
            dgvHouse.Columns["id"].Visible = false;
        }

        private void btnOwnerSave_Click(object sender, EventArgs e)
        {
            if(Validation.IsEmail(txtOwnerEmail.Text) && Validation.IsPhoneNumber(txtOwnerPhone.Text))
            {
                bool isNew = true;
                int lastSellerId = 10000;
                List<Customer> sellers = Store.ReadAllSellers();
                foreach (Customer seller in sellers)
                {
                    if (seller.Id == txtOwnerId.Text)
                    {
                        isNew = false;
                        seller.FirstName = txtOwnerFirstName.Text;
                        seller.LastName = txtOwnerLastName.Text;
                        seller.BirthDate = dtpOwnerDOB.Value.Date;
                        seller.Email = txtOwnerEmail.Text;
                        seller.Phone = txtOwnerPhone.Text;
                    }
                    if(Convert.ToInt32(seller.Id.Substring(3,5)) > lastSellerId)
                        lastSellerId = Convert.ToInt32(seller.Id.Substring(3,5));
                }
                if (isNew)
                {
                    sellers.Add(new Customer("Slr" + Convert.ToString(lastSellerId + 1), txtOwnerFirstName.Text,txtOwnerLastName.Text,dtpOwnerDOB.Value.Date,txtOwnerEmail.Text,txtOwnerPhone.Text));
                }
                Store.SaveAllSellers(sellers);

                btnOwnerDelete.Enabled = false;

                RefreshGrid(pOwner.Name);

                ResetForm(pOwner.Name);

            }
            else
            {
                MessageBox.Show("Invalid input values!\n\nPlease check the values of email and/or phone...", "Error");
            }
            
        }

        private void btnOwnerReset_Click(object sender, EventArgs e)
        {
            ResetForm(pOwner.Name);
            btnOwnerDelete.Enabled = false;
        }

        private void btnOwnerDelete_Click(object sender, EventArgs e)
        {
            if (txtOwnerId.Text.Trim().Length > 0)
            {
                List<Customer> sellers = Store.ReadAllSellers();
                sellers.Remove(sellers[sellers.FindIndex(h => h.Id == txtOwnerId.Text)]);
                Store.SaveAllSellers(sellers);

                RefreshGrid(pOwner.Name);

                ResetForm(pOwner.Name);
            }
            else
                btnOwnerDelete.Enabled = false;
        }

        private void btnOwnerShowAll_Click(object sender, EventArgs e)
        {
            btnHouseDelete.Enabled = false;
            txtOwnerSearch.Clear();
        }

        private void txtOwnerSearch_TextChanged(object sender, EventArgs e)
        {
            List<Customer> sellers = new List<Customer>();
            sellers = Store.ReadAllSellers();
            var source = new BindingSource();
            source.DataSource = sellers.FindAll(seller => seller.FirstName.ToLower().Contains(txtOwnerSearch.Text.ToLower()) || seller.LastName.ToLower().Contains(txtOwnerSearch.Text.ToLower()) || seller.Email.ToLower().Contains(txtOwnerSearch.Text.ToLower()) || seller.Phone.ToLower().Contains(txtOwnerSearch.Text.ToLower()));
            dgvOwner.DataSource = source;
            dgvOwner.Columns["id"].Visible = false;
        }

        private void dgvOwner_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtOwnerId.Text = dgvOwner.CurrentRow.Cells["id"].Value.ToString();
            txtOwnerFirstName.Text = dgvOwner.CurrentRow.Cells["FirstName"].Value.ToString();
            txtOwnerLastName.Text = dgvOwner.CurrentRow.Cells["LastName"].Value.ToString();
            dtpOwnerDOB.Value = (DateTime)dgvOwner.CurrentRow.Cells["BirthDate"].Value;
            txtOwnerEmail.Text = dgvOwner.CurrentRow.Cells["Email"].Value.ToString();
            txtOwnerPhone.Text = dgvOwner.CurrentRow.Cells["Phone"].Value.ToString();

            btnOwnerDelete.Enabled = true;
        }

        private void txtBuyerDOB_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnContractReset_Click(object sender, EventArgs e)
        {
            ResetForm(pContract.Name);
            btnContractDelete.Enabled = false;
        }

        private void btnContractSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool isNew = true;
                int lastContractId = 10000;
                List<Contract> contracts = Store.ReadAllContracts();
                foreach (Contract contract in contracts)
                {
                    if (contract.Id == txtContractId.Text)
                    {
                        isNew = false;
                        contract.SellerId = txtContractSeller.Text.Substring(txtContractSeller.Text.Length - 8);
                        contract.BuyerId = cbContractBuyer.Text.ToString().Substring(cbContractBuyer.Text.ToString().Length - 8);
                        contract.HouseId = cbContractHouse.Text.ToString().Substring(cbContractHouse.Text.ToString().Length - 8);
                        contract.AgentId = cbContractAgent.Text.ToString().Substring(cbContractAgent.Text.ToString().Length - 8);
                        contract.ContractDate = dtpContractDate.Value.Date;
                        contract.Amount = Convert.ToDouble(txtContractAmount.Text);
                    }
                    if (Convert.ToInt32(contract.Id.Substring(3, 5)) > lastContractId)
                        lastContractId = Convert.ToInt32(contract.Id.Substring(3, 5));
                }
                if (isNew)
                {
                    string sellerId = txtContractSeller.Text.ToString().Substring(txtContractSeller.Text.ToString().Length - 8);
                    string buyerId = cbContractBuyer.Text.ToString().Substring(cbContractBuyer.Text.ToString().Length - 8);
                    string houseId = cbContractHouse.Text.ToString().Substring(cbContractHouse.Text.ToString().Length - 8);
                    string agentId = cbContractAgent.Text.ToString().Substring(cbContractAgent.Text.ToString().Length - 8);

                    contracts.Add(new Contract("Ctt" + Convert.ToString(lastContractId + 1), sellerId, buyerId, dtpContractDate.Value.Date, Convert.ToDouble(txtContractAmount.Text), agentId, houseId));
                }
                Store.SaveAllContracts(contracts);

                btnContractDelete.Enabled = false;

                RefreshGrid(pContract.Name);

                ResetForm(pContract.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid input!\n\n" + ex.Message, "Error");
            }
            
        }

        private void btnContractDelete_Click(object sender, EventArgs e)
        {
            if (txtContractId.Text.Trim().Length > 0)
            {
                List<Contract>contracts = Store.ReadAllContracts();
                contracts.Remove(contracts[contracts.FindIndex(h => h.Id == txtContractId.Text)]);
                Store.SaveAllContracts(contracts);

                RefreshGrid(pContract.Name);

                ResetForm(pContract.Name);
            }
            else
                btnContractDelete.Enabled = false;
        }

        private void btnContractShowAll_Click(object sender, EventArgs e)
        {
            btnContractDelete.Enabled = false;
            txtContractSearch.Clear();
        }

        private void txtContractSearch_TextChanged(object sender, EventArgs e)
        {
            List<Contract> contracts = Store.ReadAllContracts();
            var source = new BindingSource();
            source.DataSource = contracts.FindAll(ctt => ctt.BuyerName.ToLower().Contains(txtContractSearch.Text.ToLower()) || ctt.SellerName.ToLower().Contains(txtContractSearch.Text.ToLower()) || ctt.HouseName.ToLower().Contains(txtContractSearch.Text.ToLower()) || ctt.AgentName.ToLower().Contains(txtContractSearch.Text.ToLower()));
            dgvContract.DataSource = source;
            dgvContract.Columns["id"].Visible = false;
            dgvContract.Columns["sellerId"].Visible = false;
            dgvContract.Columns["buyerId"].Visible = false;
            dgvContract.Columns["houseId"].Visible = false;
            dgvContract.Columns["AgentId"].Visible = false;
        }

        private void dgvContract_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtContractId.Text = dgvContract.CurrentRow.Cells["id"].Value.ToString();
            txtContractAmount.Text = dgvContract.CurrentRow.Cells["Amount"].Value.ToString();
            dtpContractDate.Value = (DateTime)dgvContract.CurrentRow.Cells["ContractDate"].Value;
            txtContractSeller.Text = dgvContract.CurrentRow.Cells["SellerName"].Value.ToString();
            cbContractBuyer.SelectedText = "";
            cbContractBuyer.SelectedText = dgvContract.CurrentRow.Cells["BuyerName"].Value.ToString();
            cbContractAgent.SelectedText = "";
            cbContractAgent.SelectedText = dgvContract.CurrentRow.Cells["AgentName"].Value.ToString();
            cbContractHouse.SelectedText = "";
            cbContractHouse.SelectedText = dgvContract.CurrentRow.Cells["HouseName"].Value.ToString();

            btnContractDelete.Enabled = true;
        }

        private void btnUserReset_Click(object sender, EventArgs e)
        {
            ResetForm(pUser.Name);
            btnUserDelete.Enabled = false;
        }

        private void btnUserSave_Click(object sender, EventArgs e)
        {
            if (Validation.IsEmail(txtUserEmail.Text) && 
                Validation.IsPhoneNumber(txtUserPhone.Text) && 
                Validation.IsDoubleOnly(txtUserBaseSalary.Text) &&
                Validation.IsDoubleOnly(txtUserCommission.Text))
            {
                bool isNew = true;
                int lastUserId = 10000;
                List<User> users = Store.ReadAllUsers();
                foreach (User user in users)
                {
                    if (user.Id == txtUserId.Text)
                    {
                        isNew = false;
                        user.FirstName = txtUserFirstName.Text;
                        user.LastName = txtUserLastName.Text;
                        user.BirthDate = dtpUserDOB.Value.Date;
                        user.BaseSalary = Convert.ToDouble(txtUserBaseSalary.Text);
                        user.Commission = Convert.ToDouble(txtUserCommission.Text);
                        user.Email = txtUserEmail.Text;
                        user.Phone = txtUserPhone.Text;
                        user.IsAdmin = rbUserAdmin.Checked;
                        loggedInUser = user;
                    }
                    if (Convert.ToInt32(user.Id.Substring(3, 5)) > lastUserId)
                        lastUserId = Convert.ToInt32(user.Id.Substring(3, 5));
                }
                if (isNew)
                {
                    users.Add(new User("Usr" + Convert.ToString(lastUserId + 1), txtUserFirstName.Text, txtUserLastName.Text, dtpUserDOB.Value.Date, txtUserEmail.Text, txtUserPhone.Text, Convert.ToDouble(txtUserBaseSalary.Text), Convert.ToDouble(txtUserCommission.Text), rbUserAdmin.Checked, txtUserPassword.Text));
                }
                Store.SaveAllUsers(users);

                btnUserDelete.Enabled = false;

                RefreshGrid(pUser.Name);

                ResetForm(pUser.Name);
            }
            else
            {
                MessageBox.Show("Invalid input values!\n\nPlease check the values of email and/or phone...", "Error");
            }
        }

        private void btnUserDelete_Click(object sender, EventArgs e)
        {
            if (txtUserId.Text.Trim().Length > 0)
            {
                List<User> users = Store.ReadAllUsers();
                users.Remove(users[users.FindIndex(h => h.Id == txtUserId.Text)]);
                Store.SaveAllUsers(users);

                RefreshGrid(pUser.Name);

                ResetForm(pUser.Name);
            }
            else
                btnUserDelete.Enabled = false;
        }

        private void btnUserShowAll_Click(object sender, EventArgs e)
        {
            btnUserDelete.Enabled = false;
            txtUserSearch.Clear();
        }

        private void txtUserSearch_TextChanged(object sender, EventArgs e)
        {
            List<User> users = new List<User>();
            users = Store.ReadAllUsers();
            var source = new BindingSource();
            source.DataSource = users.FindAll(user => user.FirstName.ToLower().Contains(txtUserSearch.Text.ToLower()) || user.LastName.ToLower().Contains(txtUserSearch.Text.ToLower()) || user.Email.ToLower().Contains(txtUserSearch.Text.ToLower()) || user.Phone.ToLower().Contains(txtUserSearch.Text.ToLower()));
            dgvUser.DataSource = source;
            dgvUser.Columns["id"].Visible = false;
            dgvUser.Columns["Password"].Visible = false;
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUserId.Text = dgvUser.CurrentRow.Cells["id"].Value.ToString();
            txtUserFirstName.Text = dgvUser.CurrentRow.Cells["FirstName"].Value.ToString();
            txtUserLastName.Text = dgvUser.CurrentRow.Cells["LastName"].Value.ToString();
            dtpUserDOB.Value = (DateTime)dgvUser.CurrentRow.Cells["BirthDate"].Value;
            txtUserBaseSalary.Text = dgvUser.CurrentRow.Cells["BaseSalary"].Value.ToString();
            txtUserCommission.Text = dgvUser.CurrentRow.Cells["Commission"].Value.ToString();
            txtUserEmail.Text = dgvUser.CurrentRow.Cells["Email"].Value.ToString();
            txtUserPhone.Text = dgvUser.CurrentRow.Cells["Phone"].Value.ToString();
            rbUserAdmin.Checked = (bool)dgvUser.CurrentRow.Cells["IsAdmin"].Value;
            rbUserAgent.Checked = !rbUserAdmin.Checked;
            txtUserPassword.Text = dgvUser.CurrentRow.Cells["Password"].Value.ToString();

            btnUserDelete.Enabled = true;
        }

        private void btnProfileSave_Click(object sender, EventArgs e)
        {
            if (txtProfilePassword.Text != txtProfileRePassword.Text)
            {
                MessageBox.Show("The passwords are not same!");
                txtProfileRePassword.BackColor = Color.LightPink;
            }
            else if (!Validation.IsPhoneNumber(txtProfilePhone.Text))
                MessageBox.Show("Invalid phone number!");
            else
            {
                List<User> users = Store.ReadAllUsers();
                foreach (User user in users)
                {
                    if (user.Id == loggedInUser.Id)
                    {
                        user.FirstName = txtProfileFirstName.Text;
                        loggedInUser.FirstName = txtProfileFirstName.Text;
                        user.LastName = txtProfileLastName.Text;
                        loggedInUser.LastName = txtProfileLastName.Text;
                        user.Phone = txtProfilePhone.Text;
                        loggedInUser.Phone = txtProfilePhone.Text;
                        user.Password = txtProfilePassword.Text;
                        loggedInUser.Password = txtProfilePassword.Text;
                        if (txtProfileBrows.Text.Trim().Length > 0)
                        {
                            Store.SaveUserPhoto(txtProfileBrows.Text, user.Id);
                        }
                        break;
                    }
                }
                Store.SaveAllUsers(users);
                txtProfileRePassword.Clear();
                txtProfileBrows.Clear();
            }
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
            this.Close();
        }

        private void btnProfileBrows_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files(*.jpeg; *.jpg; *.gif; *.png; *.bmp) | *.jpeg; *.jpg; *.gif; *.png; *.bmp";
            openFileDialog.Multiselect = false;
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtProfileBrows.Text = openFileDialog.FileName;
                userPhoto.BackgroundImage = new Bitmap(openFileDialog.FileName);
            }
        }

        private void btnRemovePhoto_Click(object sender, EventArgs e)
        {
            userPhoto.BackgroundImage.Dispose();
            userPhoto.BackgroundImage = null;
        }

        private void txtHouseLivingArea_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.IsDoubleOnly(txtHouseLivingArea.Text))
            {
                txtHouseLivingArea.BackColor = Color.LightPink;
            }
            else
            {
                txtHouseLivingArea.BackColor = Color.White;
            }
        }

        private void txtHouseBedRooms_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.IsNumberOnly(txtHouseBedRooms.Text))
            {
                txtHouseBedRooms.BackColor = Color.LightPink;
            }
            else
            {
                txtHouseBedRooms.BackColor= Color.White;
            }
        }

        

        private void txtHousePrice_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.IsDoubleOnly(txtHousePrice.Text))
            {
                txtHousePrice.BackColor = Color.LightPink;
            }
            else
            {
                txtHousePrice.BackColor = Color.White;
            }
        }

        private void txtOwnerEmail_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.IsEmail(txtOwnerEmail.Text))
            {
                txtOwnerEmail.BackColor = Color.LightPink;
            }
            else
            {
                txtOwnerEmail.BackColor = Color.White;
            }
        }

        private void txtOwnerPhone_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.IsPhoneNumber(txtOwnerPhone.Text))
            {
                txtOwnerPhone.BackColor = Color.LightPink;
            }
            else
            {
                txtOwnerPhone.BackColor = Color.White;
            }
        }

        private void txtProfilePhone_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.IsPhoneNumber(txtProfilePhone.Text))
            {
                txtProfilePhone.BackColor = Color.LightPink;
            }
            else
            {
                txtProfilePhone.BackColor = Color.White;
            }
        }

        private void txtContractAmount_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.IsDoubleOnly(txtContractAmount.Text))
            {
                txtContractAmount.BackColor = Color.LightPink;
            }
            else
            {
                txtContractAmount.BackColor = Color.White;
            }
        }

        private void txtUserBaseSalary_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.IsDoubleOnly(txtUserBaseSalary.Text))
            {
                txtUserBaseSalary.BackColor = Color.LightPink;
            }
            else
            {
                txtUserBaseSalary.BackColor = Color.White;
            }
        }

        private void txtUserEmail_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.IsEmail(txtUserEmail.Text))
            {
                txtUserEmail.BackColor = Color.LightPink;
            }
            else
            {
                txtUserEmail.BackColor = Color.White;
            }
        }

        private void txtUserPhone_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.IsPhoneNumber(txtUserPhone.Text))
            {
                txtUserPhone.BackColor = Color.LightPink;
            }
            else
            {
                txtUserPhone.BackColor = Color.White;
            }
        }

        private void txtUserCommission_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.IsDoubleOnly(txtUserCommission.Text))
            {
                txtUserCommission.BackColor = Color.LightPink;
            }
            else
            {
                txtUserCommission.BackColor = Color.White;
            }
        }

        private void txtProfileRePassword_TextChanged(object sender, EventArgs e)
        {
            if (txtProfilePassword.Text != txtProfileRePassword.Text)
            {
                txtProfileRePassword.BackColor = Color.LightPink;
            }
            else
            {
                txtProfileRePassword.BackColor = Color.White;
            }
        }


        private void cbContractHouse_SelectedValueChanged(object sender, EventArgs e)
        {
            

        }

        private void cbContractHouse_Click(object sender, EventArgs e)
        {
            cbContractHouse.DataSource = Store.ReadAllHouses();
            cbContractHouse.SelectedIndex = 0;

        }

        private void cbContractBuyer_Click(object sender, EventArgs e)
        {
            if(txtContractSeller.Text.Length > 0)
            {
                cbContractBuyer.DataSource = Store.ReadAllSellers().FindAll(slr => slr.Id != txtContractSeller.Text.Substring(txtContractSeller.Text.Length - 8));
                cbContractBuyer.SelectedIndex = -1;
            }
        }

        private void cbContractHouse_SelectionChangeCommitted(object sender, EventArgs e)
        {
           
        }

        private void cbContractHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                House house = Store.ReadAllHouses().Find(hse => hse.Id == cbContractHouse.Text.Substring(cbContractHouse.Text.Length - 8));

                txtContractSeller.Text = Store.ReadAllSellers().Find(x => x.Id == house.OwnerId).ToString();
            }
            catch (Exception ex)
            { }
        }
    }
}
