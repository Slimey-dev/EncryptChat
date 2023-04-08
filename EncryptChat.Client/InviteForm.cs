using EncryptChat.Shared.Model;

namespace EncryptChat.Client;

public partial class InviteForm : Form
{
    private readonly Button _acceptButton;
    private readonly Button _declineButton;
    private readonly Label _ownerNameLabel;

    private readonly Form1 _form1;

    private Notification _notification = null!;

    public InviteForm()
    {
        InitializeComponent();

        _ownerNameLabel = new Label();
        _acceptButton = new Button();
        _declineButton = new Button();

        //
        // ownerNameLabel
        //
        _ownerNameLabel.AutoSize = true;
        _ownerNameLabel.Location = new Point(12, 9);
        _ownerNameLabel.Name = "ownerNameLabel";
        _ownerNameLabel.Size = new Size(38, 13);
        _ownerNameLabel.TabIndex = 0;
        _ownerNameLabel.Text = "Name:";
        //
        // acceptButton
        //
        _acceptButton.Location = new Point(12, 205);
        _acceptButton.Name = "acceptButton";
        _acceptButton.Size = new Size(260, 23);
        _acceptButton.TabIndex = 2;
        _acceptButton.Text = "Accept";
        _acceptButton.UseVisualStyleBackColor = true;
        _acceptButton.Click += AcceptButton_Click!;
        //
        // declineButton
        //
        _declineButton.Location = new Point(12, 205);
        _declineButton.Name = "declineButton";
        _declineButton.Size = new Size(260, 23);
        _declineButton.TabIndex = 2;
        _declineButton.Text = "Decline";
        _declineButton.UseVisualStyleBackColor = true;
        _declineButton.Click += DeclineButton_Click!;

        Controls.Add(_ownerNameLabel);
        Controls.Add(_acceptButton);
        Controls.Add(_declineButton);
    }

    public InviteForm(Form1 form1, Notification notification) : this()
    {
        _form1 = form1;
        _notification = notification;
        _ownerNameLabel.Text = "Owner:" + notification.OwnerName;
    }

    private void AcceptButton_Click(object sender, EventArgs e)
    {
        _form1.AcceptInvite(_notification);
        Close();
    }

    private void DeclineButton_Click(object sender, EventArgs e)
    {
        _form1.RejectInvite(_notification);
        Close();
    }

    public void SetNotification(Notification notification)
    {
        _notification = notification;
        _ownerNameLabel.Text = notification.OwnerName;
    }
}