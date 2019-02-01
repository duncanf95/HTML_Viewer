using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Brower_v1
{
    class LoginPanel : Panel
    {
        private TextBox txtUsername = new TextBox();
        private TextBox txtPassword = new TextBox();

        private Label lblUsername = new Label();
        private Label lblPassword = new Label();

        private Button btnLogin = new Button();
        private Button btnSignUp = new Button();
        private Button btnGuest = new Button();
        private BrowserTab tp;
        private User user;

        public bool loggedIn;

        


        public LoginPanel(int inputWidth, int inputHeight, User inputUser, BrowserTab curTab)
        {
            Width = inputWidth;
            Height = inputHeight;
            tp = curTab;
            user = inputUser;

            txtPassword.UseSystemPasswordChar = true;

            SetAttributes();
            SetLocations();
            SetFunctions();

            Controls.Add(txtUsername);
            Controls.Add(txtPassword);
            Controls.Add(lblUsername);
            Controls.Add(lblPassword);
            Controls.Add(btnLogin);
            Controls.Add(btnSignUp);
            Controls.Add(btnGuest);
        }

        private void Login(object sender, EventArgs e)
        {
            user.Username = txtUsername.Text;
            user.Password = txtPassword.Text;


            if (user.Username.Length >= 6)
            {
                if (user.Password.Length >= 6)
                {

                    if (user.Login(tp))
                    {
                        Hide();
                        if (user.HomePage != "")
                        {
                            tp.LoadHome();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Password must be longer than 6 characters");
                }
            }
            else
            {
                MessageBox.Show("username must be longer than 6 characters");
            }

        }

        private void SignUp(object sender, EventArgs e)
        {
            user.Username = txtUsername.Text;
            user.Password = txtPassword.Text;

            if (user.Username.Length >= 6)
            {
                if (user.Password.Length >= 6)
                {
                    if (user.SignUp())
                    {
                        MessageBox.Show("Signed up sucessfully");

                    }
                    else
                    {
                        MessageBox.Show("Username already exists or sign up failed");
                    }
                }else
                {
                    MessageBox.Show("Password must be longer than 6 characters");
                }
            }
            else
            {
                MessageBox.Show("username must be longer than 6 characters");
            }
        }

        private void SetFunctions()
        {
            btnLogin.Click += new EventHandler(Login);
            btnSignUp.Click += new EventHandler(SignUp);
            btnGuest.Click += new EventHandler(GuestLogin);
        }

        private void SetLocations()
        {
            int xDrawCursor = 0;
            int yDrawCursor = 0;

            xDrawCursor += (Width / 2) - (txtUsername.Width / 2);
            yDrawCursor += (Height / 2) - ((lblUsername.Height) + (txtUsername.Width));

            lblUsername.Location = new Point(xDrawCursor, yDrawCursor);
            yDrawCursor += lblUsername.Height;

            txtUsername.Location = new Point(xDrawCursor, yDrawCursor);
            yDrawCursor += txtUsername.Height;

            lblPassword.Location = new Point(xDrawCursor, yDrawCursor);
            yDrawCursor += lblPassword.Height;

            txtPassword.Location = new Point(xDrawCursor, yDrawCursor);
            yDrawCursor += txtPassword.Height;

            btnLogin.Location = new Point(xDrawCursor, yDrawCursor);
            xDrawCursor += btnLogin.Width;

            btnSignUp.Location = new Point(xDrawCursor, yDrawCursor);
            xDrawCursor -= btnLogin.Width;
            yDrawCursor += btnLogin.Height;

            btnGuest.Location = new Point(xDrawCursor, yDrawCursor);

        }

        private void SetAttributes()
        {
            txtUsername.Width = Width / 6;
            txtPassword.Width = txtUsername.Width;

            lblUsername.Text = "Username";
            lblPassword.Text = "Password";

            btnLogin.Text = "Login";
            btnLogin.Width = txtUsername.Width/2;

            btnSignUp.Text = "Signup";
            btnSignUp.Width = btnLogin.Width;

            btnGuest.Text = "Guest Login";
            btnGuest.Width = txtUsername.Width;
        }

        private void GuestLogin(object sender, EventArgs e)
        {
            user.Username = "";
            user.Password = "";
            if (user.Login(tp))
            {
                Hide();
                if (user.HomePage != "")
                {
                    tp.LoadHome();
                }
            }
        }

    }
}
