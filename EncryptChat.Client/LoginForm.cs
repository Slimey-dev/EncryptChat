using System.Windows.Forms;

namespace EncryptChat.Client;

public partial class LoginForm : Form
{
    Label emailLabel;
    TextBox emailBox;
    Label passwordLabel;
    TextBox passwordBox;
    Button loginButton;
    
    public LoginForm()
    {
        InitializeComponent();
        
        emailLabel = new Label();
        emailBox = new TextBox();
        passwordLabel = new Label();
        passwordBox = new TextBox();
        loginButton = new Button();

        //
        // emailLabel
        //
        emailLabel.AutoSize = true;
        emailLabel.Location = new Point(12, 9);
        emailLabel.Name = "emailLabel";
        emailLabel.Size = new Size(32, 13);
        emailLabel.Text = "Email";
        //
        // emailBox
        //
        emailBox.Location = new Point(12, 25);
        emailBox.Name = "emailBox";
        emailBox.Size = new Size(260, 20);
        //
        // passwordLabel
        //
        passwordLabel.AutoSize = true;
        passwordLabel.Location = new Point(12, 48);
        passwordLabel.Name = "passwordLabel";
        passwordLabel.Size = new Size(53, 13);
        passwordLabel.Text = "Password";
        //
        // passwordBox
        //
        passwordBox.Location = new Point(12, 64);
        passwordBox.Name = "passwordBox";
        passwordBox.Size = new Size(260, 20);
        //
        // loginButton
        //
        loginButton.Location = new Point(12, 95);
        loginButton.Name = "loginButton";
        loginButton.Size = new Size(75, 23);
        loginButton.Text = "Login";
        loginButton.Click += LoginButton_Click;

        Controls.Add(emailLabel);
        Controls.Add(emailBox);
        Controls.Add(passwordLabel);
        Controls.Add(passwordBox);
        Controls.Add(loginButton);
        
        Text = "Login";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(300, 170);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
    }

    private void LoginButton_Click(object sender, EventArgs e)
    {
    }
}