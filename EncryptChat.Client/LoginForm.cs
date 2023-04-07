using System.Security.Cryptography;
using System.Text;

namespace EncryptChat.Client;

public partial class LoginForm : Form
{
    private readonly Form1 _form1;
    private readonly TextBox emailBox;
    private readonly Label emailLabel;
    private readonly Button loginButton;
    private readonly TextBox passwordBox;
    private readonly Label passwordLabel;

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

    public LoginForm(Form1 form1) : this()
    {
        _form1 = form1;
    }

    private void LoginButton_Click(object sender, EventArgs e)
    {
        var client = new HttpClient();
        var data = new MultipartFormDataContent();
        data.Add(new StringContent(emailBox.Text), "email");
        data.Add(new StringContent(passwordBox.Text), "password");
        var response = client.PostAsync("https://localhost:44383/api/UserApi/GetApiKey", data).Result.Content
            .ReadAsStringAsync()
            .Result;
        if (response != "null")
        {
            var email = emailBox.Text;
            _form1.ApiKey = response;
            _form1.Email = email;
            SaveEmail(email);
            SaveApiKey(response);
            GenerateKeyPair();
            _form1.Connect();
            Close();
        }
        else
        {
            MessageBox.Show("Invalid credentials");
        }
    }

    private void SaveEmail(string email)
    {
        var encrypted = ProtectedData.Protect(Encoding.UTF8.GetBytes(email), null, DataProtectionScope.CurrentUser);
        File.WriteAllBytes("email.txt", encrypted);
    }

    private void SaveApiKey(string apiKey)
    {
        var encrypted = ProtectedData.Protect(Encoding.UTF8.GetBytes(apiKey), null, DataProtectionScope.CurrentUser);
        File.WriteAllBytes("apiKey.txt", encrypted);
    }

    private void GenerateKeyPair()
    {
        using var rsa = RSA.Create();
        var publicKey = rsa.ExportRSAPublicKey();
        var privateKey = rsa.ExportRSAPrivateKey();
        var client = new HttpClient();
        var multipart = new MultipartFormDataContent();
        multipart.Add(new ByteArrayContent(publicKey), "publicKey");
        multipart.Add(new StringContent(_form1.ApiKey), "apiKey");
        var response = client.PostAsync("https://localhost:44383/api/UserApi/SetPublicKey", multipart).Result;

        if (response.Content.ReadAsStringAsync().Result == "Added")
        {
            File.WriteAllBytes("privateKey.xml", privateKey);
            MessageBox.Show("Key pair generated");
        }
        else if (response.Content.ReadAsStringAsync().Result == "Updated")
        {
            File.WriteAllBytes("privateKey.xml", privateKey);
            MessageBox.Show("Key pair updated");
        }
        else
        {
            MessageBox.Show("Something went wrong");
        }
    }
}