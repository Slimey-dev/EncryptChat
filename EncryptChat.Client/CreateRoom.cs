namespace EncryptChat.Client;

public partial class CreateRoom : Form
{
    private readonly Button createButton;
    private readonly Label nameLabel;
    private readonly RichTextBox nameTextBox;
    private readonly Label participantsLabel;
    private readonly RichTextBox participantsTextBox;

    public CreateRoom()
    {
        InitializeComponent();

        nameLabel = new Label();
        nameTextBox = new RichTextBox();
        createButton = new Button();
        participantsTextBox = new RichTextBox();
        participantsLabel = new Label();

        //
        // nameLabel
        //
        nameLabel.AutoSize = true;
        nameLabel.Location = new Point(12, 9);
        nameLabel.Name = "nameLabel";
        nameLabel.Size = new Size(38, 13);
        nameLabel.TabIndex = 0;
        nameLabel.Text = "Name:";
        //
        // nameTextBox
        //
        nameTextBox.Location = new Point(12, 25);
        nameTextBox.Name = "nameTextBox";
        nameTextBox.Size = new Size(260, 20);
        nameTextBox.TabIndex = 1;
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
        Controls.Add(nameTextBox);
        Controls.Add(nameLabel);
        Name = "CreateRoom";
        Text = "Create Room";
    }

    private void CreateButton_Click(object sender, EventArgs e)
    {
    }
}