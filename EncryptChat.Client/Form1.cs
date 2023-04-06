namespace EncryptChat.Client;

public partial class Form1 : Form
{
    MenuStrip menuStrip;
    ToolStripMenuItem accountToolStripMenuItem;
    ToolStripMenuItem loginToolStripMenuItem;
    ToolStripMenuItem logoutToolStripMenuItem;
    ToolStripMenuItem roomsToolStripMenuItem;
    ToolStripMenuItem createRoomToolStripMenuItem;
    ToolStripMenuItem joinRoomToolStripMenuItem;
    ToolStripMenuItem leaveRoomToolStripMenuItem;

    RichTextBox chatBox;
    TextBox messageBox;
    Button sendButton;

    public Form1()
    {
        InitializeComponent();

        menuStrip = new MenuStrip();
        accountToolStripMenuItem = new ToolStripMenuItem();
        loginToolStripMenuItem = new ToolStripMenuItem();
        logoutToolStripMenuItem = new ToolStripMenuItem();
        roomsToolStripMenuItem = new ToolStripMenuItem();
        createRoomToolStripMenuItem = new ToolStripMenuItem();
        joinRoomToolStripMenuItem = new ToolStripMenuItem();
        leaveRoomToolStripMenuItem = new ToolStripMenuItem();

        chatBox = new RichTextBox();
        messageBox = new TextBox();
        sendButton = new Button();

        //
        // menuStrip
        //
        menuStrip.Items.AddRange(new ToolStripItem[]
        {
            accountToolStripMenuItem,
            roomsToolStripMenuItem
        });
        //
        // accountToolStripMenuItem
        //
        accountToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
        {
            loginToolStripMenuItem,
            logoutToolStripMenuItem
        });
        accountToolStripMenuItem.Name = "accountToolStripMenuItem";
        accountToolStripMenuItem.Size = new Size(65, 20);
        accountToolStripMenuItem.Text = "Account";
        //
        // loginToolStripMenuItem
        //
        loginToolStripMenuItem.Name = "loginToolStripMenuItem";
        loginToolStripMenuItem.Size = new Size(180, 22);
        loginToolStripMenuItem.Text = "Login";
        loginToolStripMenuItem.Click += LoginToolStripMenuItem_Click;
        //
        // logoutToolStripMenuItem
        //
        logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
        logoutToolStripMenuItem.Size = new Size(180, 22);
        logoutToolStripMenuItem.Text = "Logout";
        logoutToolStripMenuItem.Click += LogoutToolStripMenuItem_Click;
        //
        // roomsToolStripMenuItem
        //
        roomsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
        {
            createRoomToolStripMenuItem,
            joinRoomToolStripMenuItem,
            leaveRoomToolStripMenuItem
        });
        roomsToolStripMenuItem.Name = "roomsToolStripMenuItem";
        roomsToolStripMenuItem.Size = new Size(53, 20);
        roomsToolStripMenuItem.Text = "Rooms";
        //
        // createRoomToolStripMenuItem
        //
        createRoomToolStripMenuItem.Name = "createRoomToolStripMenuItem";
        createRoomToolStripMenuItem.Size = new Size(180, 22);
        createRoomToolStripMenuItem.Text = "Create Room";
        createRoomToolStripMenuItem.Click += CreateRoomToolStripMenuItem_Click;
        //
        // joinRoomToolStripMenuItem
        //
        joinRoomToolStripMenuItem.Name = "joinRoomToolStripMenuItem";
        joinRoomToolStripMenuItem.Size = new Size(180, 22);
        joinRoomToolStripMenuItem.Text = "Join Room";
        joinRoomToolStripMenuItem.Click += JoinRoomToolStripMenuItem_Click;
        //
        // leaveRoomToolStripMenuItem
        //
        leaveRoomToolStripMenuItem.Name = "leaveRoomToolStripMenuItem";
        leaveRoomToolStripMenuItem.Size = new Size(180, 22);
        leaveRoomToolStripMenuItem.Text = "Leave Room";
        leaveRoomToolStripMenuItem.Click += LeaveRoomToolStripMenuItem_Click;
        //
        // chatBox
        //
        chatBox.Location = new Point(12, 27);
        chatBox.Name = "chatBox";
        chatBox.Size = new Size(776, 411);
        chatBox.TabIndex = 0;
        chatBox.Text = "";
        chatBox.ReadOnly = true;
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
        sendButton.Location = new Point(12, 470);
        sendButton.Name = "sendButton";
        sendButton.Size = new Size(776, 23);
        sendButton.TabIndex = 2;
        sendButton.Text = "Send";
        sendButton.UseVisualStyleBackColor = true;
        sendButton.Click += SendButton_Click;

        Controls.Add(menuStrip);
        Controls.Add(chatBox);
        Controls.Add(messageBox);
        Controls.Add(sendButton);

        Text = "EncryptChat";
        ClientSize = new Size(800, 500);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
    }

    private void LoginToolStripMenuItem_Click(object sender, EventArgs e)
    {
        //open loginform
        LoginForm loginForm = new LoginForm();
        loginForm.Show();
    }

    private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void CreateRoomToolStripMenuItem_Click(object sender, EventArgs e)
    {
    }

    private void JoinRoomToolStripMenuItem_Click(object sender, EventArgs e)
    {
    }
    
    private void LeaveRoomToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }
    
    private void SendButton_Click(object sender, EventArgs e)
    {

    }
    
    

}