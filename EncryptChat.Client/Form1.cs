using System.Security.Cryptography;
using System.Text;
using EncryptChat.Client.Util;
using EncryptChat.Shared.Model;
using Microsoft.AspNetCore.SignalR.Client;

namespace EncryptChat.Client;

public partial class Form1 : Form
{
    private readonly ToolStripMenuItem _createRoomToolStripMenuItem;
    private readonly ToolStripMenuItem _leaveRoomToolStripMenuItem;
    private readonly ToolStripMenuItem _loginToolStripMenuItem;
    private readonly ToolStripMenuItem _logoutToolStripMenuItem;
    private readonly TextBox _messageBox;
    private readonly ToolStripMenuItem _notificationsToolStripMenuItem;
    private readonly Button _sendButton;

    public readonly RichTextBox ChatBox;
    private HubConnection _connection;

    private byte[] _privatekey;

    private Guid _roomId = Guid.Empty;
    private string _symmetricKey = string.Empty;


    public Form1()
    {
        InitializeComponent();

        var menuStrip = new MenuStrip();
        var accountToolStripMenuItem = new ToolStripMenuItem();
        var roomsToolStripMenuItem = new ToolStripMenuItem();
        _messageBox = new TextBox();
        _loginToolStripMenuItem = new ToolStripMenuItem();
        _logoutToolStripMenuItem = new ToolStripMenuItem();
        _createRoomToolStripMenuItem = new ToolStripMenuItem();
        _leaveRoomToolStripMenuItem = new ToolStripMenuItem();
        _notificationsToolStripMenuItem = new ToolStripMenuItem();
        _sendButton = new Button();

        ChatBox = new RichTextBox();

        //
        // menuStrip
        //
        menuStrip.Items.AddRange(new ToolStripItem[]
        {
            accountToolStripMenuItem,
            roomsToolStripMenuItem,
            _notificationsToolStripMenuItem
        });
        //
        // accountToolStripMenuItem
        //
        accountToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
        {
            _loginToolStripMenuItem,
            _logoutToolStripMenuItem
        });
        accountToolStripMenuItem.Name = "accountToolStripMenuItem";
        accountToolStripMenuItem.Size = new Size(65, 20);
        accountToolStripMenuItem.Text = "Account";
        //
        // loginToolStripMenuItem
        //
        _loginToolStripMenuItem.Name = "loginToolStripMenuItem";
        _loginToolStripMenuItem.Size = new Size(180, 22);
        _loginToolStripMenuItem.Text = "Login";
        _loginToolStripMenuItem.Click += LoginToolStripMenuItem_Click!;
        //
        // logoutToolStripMenuItem
        //
        _logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
        _logoutToolStripMenuItem.Size = new Size(180, 22);
        _logoutToolStripMenuItem.Text = "Logout";
        _logoutToolStripMenuItem.Click += LogoutToolStripMenuItem_Click!;
        //
        // roomsToolStripMenuItem
        //
        roomsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
        {
            _createRoomToolStripMenuItem,
            _leaveRoomToolStripMenuItem
        });
        roomsToolStripMenuItem.Name = "roomsToolStripMenuItem";
        roomsToolStripMenuItem.Size = new Size(53, 20);
        roomsToolStripMenuItem.Text = "Rooms";
        //
        // createRoomToolStripMenuItem
        //
        _createRoomToolStripMenuItem.Name = "createRoomToolStripMenuItem";
        _createRoomToolStripMenuItem.Size = new Size(180, 22);
        _createRoomToolStripMenuItem.Text = "Create Room";
        _createRoomToolStripMenuItem.Click += CreateRoomToolStripMenuItem_Click!;
        //
        // leaveRoomToolStripMenuItem
        //
        _leaveRoomToolStripMenuItem.Name = "leaveRoomToolStripMenuItem";
        _leaveRoomToolStripMenuItem.Size = new Size(180, 22);
        _leaveRoomToolStripMenuItem.Text = "Leave Room";
        _leaveRoomToolStripMenuItem.Click += LeaveRoomToolStripMenuItem_Click!;
        //
        // chatBox
        //
        ChatBox.Location = new Point(12, 27);
        ChatBox.Name = "chatBox";
        ChatBox.Size = new Size(776, 411);
        ChatBox.TabIndex = 0;
        ChatBox.Text = "";
        ChatBox.ReadOnly = true;
        //
        // messageBox
        //
        _messageBox.Location = new Point(12, 444);
        _messageBox.Name = "messageBox";
        _messageBox.Size = new Size(776, 20);
        _messageBox.TabIndex = 1;
        //
        // sendButton
        //
        _sendButton.Location = new Point(12, 470);
        _sendButton.Name = "sendButton";
        _sendButton.Size = new Size(776, 23);
        _sendButton.TabIndex = 2;
        _sendButton.Text = "Send";
        _sendButton.UseVisualStyleBackColor = true;
        _sendButton.Click += SendButton_Click!;
        //
        // notificationsToolStripMenuItem
        //
        _notificationsToolStripMenuItem.Name = "notificationsToolStripMenuItem";
        _notificationsToolStripMenuItem.Size = new Size(180, 22);
        _notificationsToolStripMenuItem.Text = "Notifications";
        _notificationsToolStripMenuItem.Click += notificationsToolStripMenuItem_Click!;

        Controls.Add(menuStrip);
        Controls.Add(ChatBox);
        Controls.Add(_messageBox);
        Controls.Add(_sendButton);

        Text = "EncryptChat";
        ClientSize = new Size(800, 500);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        Load += Form1_Load!;
    }

    public sealed override string Text
    {
        get { return base.Text; }
#pragma warning disable CS8765
        set { base.Text = value ?? throw new ArgumentNullException(nameof(value)); }
#pragma warning restore CS8765
    }

    public List<Notification> Notifications { get; set; } = new();
    public string? ApiKey { get; set; }

    public string? Email { get; set; }

    private void Form1_Load(object sender, EventArgs e)
    {
        if (File.Exists("apikey.txt"))
        {
            var encrypted = File.ReadAllBytes("apikey.txt");
            ApiKey = Encoding.UTF8.GetString(ProtectedData.Unprotect(encrypted, null, DataProtectionScope.CurrentUser));
        }

        if (File.Exists("email.txt"))
        {
            var encrypted = File.ReadAllBytes("email.txt");
            Email = Encoding.UTF8.GetString(ProtectedData.Unprotect(encrypted, null, DataProtectionScope.CurrentUser));
        }

        if (File.Exists("privateKey.txt")) _privatekey = File.ReadAllBytes("privateKey.xml");

        if (ApiKey != null && Email != null) Connect();
    }

    public void SetPrivateKey(byte[] privateKey)
    {
        _privatekey = privateKey;
    }

    public void Connect()
    {
        _connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:44383/chatHub")
            .Build();

        _connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            var decryptedMessage = Encryption.Decrypt(message, _symmetricKey);
            var msg = $"{user}: {decryptedMessage}";
            ChatBox.Invoke(new MethodInvoker(delegate { ChatBox.AppendText(msg + "\n"); }));
        });

        _connection.On<Notification>("ReceiveNotification", notification =>
        {
            var baloon = new NotifyIcon
            {
                Visible = true,
                Icon = SystemIcons.Information,
                BalloonTipTitle = "New room invitation",
                BalloonTipText = $"{notification.OwnerName} invited you to a room"
            };
            baloon.BalloonTipClicked += (sender, args) =>
            {
                var form = new InviteForm(this, notification);
                form.Show();
            };

            baloon.ShowBalloonTip(5000);

            Notifications.Add(notification);
            _notificationsToolStripMenuItem.Text = $"Notifications ({Notifications.Count})";
        });

        _connection.StartAsync();
    }

    public void CreateRoom(string[] participants)
    {
        _roomId = Guid.NewGuid();
        _symmetricKey = Encryption.GenerateKey();

        _connection.InvokeAsync("JoinRoom", _roomId);

        foreach (var participant in participants)
        {
            var client = new HttpClient();
            client.GetAsync("https://localhost:44383/api/UserApi/GetApiKey?email=" + participant).ContinueWith(task =>
            {
                var response = task.Result;
                var publickey = response.Content.ReadAsStringAsync().Result;
                var encryptedKey = EncryptionRsa.Encrypt(_symmetricKey, Convert.FromBase64String(publickey));
                _connection.InvokeAsync("SendNotification", participant, new Notification
                {
                    RoomId = _roomId,
                    EncryptedSymmetricKey = encryptedKey,
                    OwnerName = Email
                });
            });
        }
    }

    public void AcceptInvite(Notification notification)
    {
        _roomId = notification.RoomId;
        _symmetricKey = EncryptionRsa.Decrypt(notification.EncryptedSymmetricKey!, _privatekey);
        _connection.InvokeAsync("JoinRoom", _roomId);
        ChatBox.Text = "Joined room by " + notification.OwnerName + Environment.NewLine;
        RemoveNotification(notification);
    }

    public void RejectInvite(Notification notification)
    {
        RemoveNotification(notification);
    }

    private void RemoveNotification(Notification notification)
    {
        Notifications.Remove(notification);
        _notificationsToolStripMenuItem.Text = $"Notifications ({Notifications.Count})";
    }

    public void SendMessage(string message)
    {
        var encryptedMessage = Encryption.Encrypt(message, _symmetricKey);
        _connection.InvokeAsync("SendMessage", _roomId, Email, encryptedMessage);
    }

    private void LoginToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var loginForm = new LoginForm(this);
        loginForm.Show();
    }

    private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
    {
    }

    private void CreateRoomToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var createRoomForm = new CreateRoom(this);
        createRoomForm.Show();
    }

    private void LeaveRoomToolStripMenuItem_Click(object sender, EventArgs e)
    {
    }

    private void SendButton_Click(object sender, EventArgs e)
    {
        if (_connection.State != HubConnectionState.Connected)
            return;
        if (_roomId == Guid.Empty)
            return;
        SendMessage(_messageBox.Text);
        _messageBox.Text = "";
    }

    private void notificationsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var notificationsForm = new NotificationsForm(this);
        notificationsForm.Show();
    }
}