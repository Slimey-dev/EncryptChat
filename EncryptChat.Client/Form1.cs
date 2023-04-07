using EncryptChat.Client.Model;

namespace EncryptChat.Client;

public partial class Form1 : Form
{
    private readonly ToolStripMenuItem _createRoomToolStripMenuItem;
    private readonly ToolStripMenuItem _leaveRoomToolStripMenuItem;
    private readonly ToolStripMenuItem _loginToolStripMenuItem;
    private readonly ToolStripMenuItem _logoutToolStripMenuItem;
    private readonly ToolStripMenuItem _notificationsToolStripMenuItem;
    private readonly Button _sendButton;

    public readonly RichTextBox ChatBox;


    public Form1()
    {
        InitializeComponent();

        var menuStrip = new MenuStrip();
        var accountToolStripMenuItem = new ToolStripMenuItem();
        var roomsToolStripMenuItem = new ToolStripMenuItem();
        var messageBox = new TextBox();
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
        _loginToolStripMenuItem.Click += LoginToolStripMenuItem_Click;
        //
        // logoutToolStripMenuItem
        //
        _logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
        _logoutToolStripMenuItem.Size = new Size(180, 22);
        _logoutToolStripMenuItem.Text = "Logout";
        _logoutToolStripMenuItem.Click += LogoutToolStripMenuItem_Click;
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
        _createRoomToolStripMenuItem.Click += CreateRoomToolStripMenuItem_Click;
        //
        // leaveRoomToolStripMenuItem
        //
        _leaveRoomToolStripMenuItem.Name = "leaveRoomToolStripMenuItem";
        _leaveRoomToolStripMenuItem.Size = new Size(180, 22);
        _leaveRoomToolStripMenuItem.Text = "Leave Room";
        _leaveRoomToolStripMenuItem.Click += LeaveRoomToolStripMenuItem_Click;
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
        messageBox.Location = new Point(12, 444);
        messageBox.Name = "messageBox";
        messageBox.Size = new Size(776, 20);
        messageBox.TabIndex = 1;
        //
        // sendButton
        //
        _sendButton.Location = new Point(12, 470);
        _sendButton.Name = "sendButton";
        _sendButton.Size = new Size(776, 23);
        _sendButton.TabIndex = 2;
        _sendButton.Text = "Send";
        _sendButton.UseVisualStyleBackColor = true;
        _sendButton.Click += SendButton_Click;
        //
        // notificationsToolStripMenuItem
        //
        _notificationsToolStripMenuItem.Name = "notificationsToolStripMenuItem";
        _notificationsToolStripMenuItem.Size = new Size(180, 22);
        _notificationsToolStripMenuItem.Text = "Notifications";
        _notificationsToolStripMenuItem.Click += notificationsToolStripMenuItem_Click;

        Controls.Add(menuStrip);
        Controls.Add(ChatBox);
        Controls.Add(messageBox);
        Controls.Add(_sendButton);

        Text = "EncryptChat";
        ClientSize = new Size(800, 500);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
    }

    public List<Notification> Notifications { get; set; } = new();

    private void LoginToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var loginForm = new LoginForm();
        loginForm.Show();
    }

    private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
    {
    }

    private void CreateRoomToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var createRoomForm = new CreateRoom();
        createRoomForm.Show();
    }

    private void LeaveRoomToolStripMenuItem_Click(object sender, EventArgs e)
    {
    }

    private void SendButton_Click(object sender, EventArgs e)
    {
    }

    private void notificationsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var notificationsForm = new NotificationsForm();
        notificationsForm.Show();
    }
}