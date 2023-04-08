using EncryptChat.Shared.Model;

namespace EncryptChat.Client;

public partial class NotificationsForm : Form
{
    private readonly Button dismissAllButton;
    private readonly ListBox notificationsListBox;

    public Form1 _form1;

    public NotificationsForm()
    {
        InitializeComponent();

        dismissAllButton = new Button();
        notificationsListBox = new ListBox();

        //
        // dismissAllButton
        //
        dismissAllButton.Location = new Point(12, 205);
        dismissAllButton.Name = "dismissAllButton";
        dismissAllButton.Size = new Size(260, 23);
        dismissAllButton.TabIndex = 2;
        dismissAllButton.Text = "Dismiss All";
        dismissAllButton.UseVisualStyleBackColor = true;
        dismissAllButton.Click += DismissAllButton_Click;
        //
        // notificationsListBox
        //
        notificationsListBox.FormattingEnabled = true;
        notificationsListBox.Location = new Point(12, 12);
        notificationsListBox.Name = "notificationsListBox";
        notificationsListBox.Size = new Size(260, 186);
        notificationsListBox.TabIndex = 3;
        notificationsListBox.SelectedIndexChanged += NotificationsListBox_SelectedIndexChanged;

        Controls.Add(notificationsListBox);
        Controls.Add(dismissAllButton);

        Name = "NotificationsForm";
        Text = "Notifications";

        AutoScaleDimensions = new SizeF(6F, 13F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(284, 261);

        Load += NotificationsForm_Load;
    }

    public NotificationsForm(Form1 form1) : this()
    {
        _form1 = form1;
    }

    private void NotificationsForm_Load(object sender, EventArgs e)
    {
        foreach (var notification in _form1.Notifications) notificationsListBox.Items.Add(notification);
    }

    private void NotificationsListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        var notification = (Notification)notificationsListBox.SelectedItem!;
        if (notification == null) return;
        InviteForm inviteForm = new(_form1, notification);
        inviteForm.Show();
    }

    private void DismissAllButton_Click(object sender, EventArgs e)
    {
        _form1.Notifications.Clear();
        notificationsListBox.Items.Clear();
    }
}