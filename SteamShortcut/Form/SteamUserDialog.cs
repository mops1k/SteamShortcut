using SteamShortcut.Model;
using BaseForm = System.Windows.Forms.Form;

namespace SteamShortcut.Form;

public class SteamUserDialog
{
    public SteamUser? Show(List<SteamUser>? users)
    {
        var form = new BaseForm();
        var label = new Label();
        var comboBox = new ComboBox();
        var buttonOk = new Button();
        form.Text = Localization.SteamUserDialog_Show_Select_Steam_user;
        form.FormBorderStyle = FormBorderStyle.FixedDialog;
        form.StartPosition = FormStartPosition.CenterScreen;
        form.MinimizeBox = false;
        form.MaximizeBox = false;
        form.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

        label.Text = Localization.SteamUserDialog_Show_User;
        label.AutoSize = true;
        label.Location = new Point(10, 10);

        comboBox.DataSource = users;
        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        comboBox.Location = new Point(10, label.Bottom + 5);
        comboBox.Width = 250;

        buttonOk.Text = "OK";
        buttonOk.DialogResult = DialogResult.OK;
        buttonOk.Location =
            new Point(comboBox.Right - buttonOk.Width,
                comboBox.Bottom + 10);

        form.ClientSize = new Size(
            Math.Max(comboBox.Right, buttonOk.Right) + 20,
            buttonOk.Bottom + 20
        );

        form.Controls.Add(label);
        form.Controls.Add(comboBox);
        form.Controls.Add(buttonOk);

        if (form.ShowDialog() != DialogResult.OK)
        {
            return null;
        }

        if (comboBox.SelectedItem is SteamUser selectedUser)
        {
            return selectedUser;
        }

        return null;
    }
}
