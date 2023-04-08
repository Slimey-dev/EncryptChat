namespace EncryptChat.Client;

public partial class CreateRoom : Form
{
    private readonly Button createButton;
    private readonly Label participantsLabel;
    private readonly RichTextBox participantsTextBox;

    private readonly Form1 _form1;

    public CreateRoom()
    {
        InitializeComponent();

        createButton = new Button();
        participantsTextBox = new RichTextBox();
        participantsLabel = new Label();

        //
        // createButton
        //
        createButton.Location = new Point(12, 205);
        createButton.Name = "createButton";
        createButton.Size = new Size(260, 23);
        createButton.TabIndex = 2;
        createButton.Text = "Create";
        createButton.UseVisualStyleBackColor = true;
        createButton.Click += CreateButton_Click;
        //
        // participantsTextBox
        //
        participantsTextBox.Location = new Point(12, 64);
        participantsTextBox.Name = "participantsTextBox";
        participantsTextBox.Size = new Size(260, 135);
        participantsTextBox.TabIndex = 3;
        //
        // participantsLabel
        //
        participantsLabel.AutoSize = true;
        participantsLabel.Location = new Point(12, 48);
        participantsLabel.Name = "participantsLabel";
        participantsLabel.Size = new Size(66, 13);
        participantsLabel.TabIndex = 4;
        participantsLabel.Text = "Participants:";
        //
        // CreateRoom
        //
        AutoScaleDimensions = new SizeF(6F, 13F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(284, 261);
        Controls.Add(participantsLabel);
        Controls.Add(participantsTextBox);
        Controls.Add(createButton);
        Name = "CreateRoom";
        Text = "Create Room";
    }

    public CreateRoom(Form1 form1) : this()
    {
        _form1 = form1;
    }

    private void CreateButton_Click(object sender, EventArgs e)
    {
        var participants =
            participantsTextBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        _form1.CreateRoom(participants);
        Close();
    }
}