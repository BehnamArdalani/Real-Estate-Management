using Real_Estate_Management.Business;
using System;
using System.Windows.Forms;

namespace Real_Estate_Management.GUI
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPasswordPlain.Text = txtPassword.Text;
            lblMessage.Visible = false;
        }

        private void btnShowPassword_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.Hide();
            txtPasswordPlain.Show();
        }

        private void btnShowPassword_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.Show();
            txtPasswordPlain.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            User user = Store.Login(txtEmail.Text.Trim().ToLower(), txtPassword.Text);

            if (user != null)
            {
                this.Hide();
                Profile profile = new Profile(user);
                profile.ShowDialog();
                this.Close();
            }
            else
                lblMessage.Visible = true;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
        }
    }
}
