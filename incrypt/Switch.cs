using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

// Version 1.2

class Switch : Label
{
    Button button;
    bool selecte = true;
    string[] on_off;

    public Switch(string[] on_offText = null)
    {
        if (on_offText == null) on_off = new string[] { ">", "<" };
        else on_off = new string[] { on_offText[0], on_offText[1] };

        // label proerties
        BackColor = Color.DarkGray;
        Height = 30;
        Width = 60;
        // button proerties
        button = new Button
        {
            Text = on_off[0],
            BackColor = Color.White,
            Location = new Point(2, 2),
            Width = Width / 2 - 2,
            Height = Height - 4
        };
        button.Click += Button_Click;
        Controls.Add(button);
    }
    private void Button_Click(object sender, EventArgs e)
    {
        if (button.Text == on_off[0]) { selecte = false; button.Location = new Point(button.Location.X + button.Size.Width, button.Location.Y); button.Text = on_off[1]; }
        else if (button.Text == on_off[1]) {selecte = true; button.Location = new Point(button.Location.X - button.Size.Width, button.Location.Y); button.Text = on_off[0]; }
    }
    public int SWidth
    {
        get { return Width; }
        set { Width = value; button.Width = value / 2 - 2; }
    }
    public int SHeight
    {
        get { return Height; }
        set { Height = value; button.Height = value - 4; }
    }
    public bool Selected
    {
        get { return selecte; }
    }
}
class SwitchVert : Label
{
    Button button;
    bool selecte = true;
    string[] on_off;

    public SwitchVert(string[] on_offText = null)
    {
        if (on_offText == null) on_off = new string[] { "\u2191", "\u2193" };
        else on_off = new string[] { on_offText[0], on_offText[1] };

        // label proerties
        BackColor = Color.DarkGray;
        Height = 60;
        Width = 30;
        // button proerties
        button = new Button
        {
            Text = on_off[0],
            BackColor = Color.White,
            Location = new Point(2, 2),
            Width = Width - 4,
            Height = Height / 2 - 2
        };
        button.Click += Button_Click;
        Controls.Add(button);
    }
    private void Button_Click(object sender, EventArgs e)
    {
        if (button.Text == on_off[0]) { selecte = false; button.Location = new Point(button.Location.X, button.Location.Y + button.Size.Height); button.Text = on_off[1]; }
        else if (button.Text == on_off[1]) { selecte = true; button.Location = new Point(button.Location.X, button.Location.Y - button.Size.Height); button.Text = on_off[0]; }
    }
    public int SWidth
    {
        get { return Width; }
        set { Width = value; button.Width = value - 4; }
    }
    public int SHeight
    {
        get { return Height; }
        set { Height = value; button.Height = value / 2 - 2; }
    }
    public bool Selected
    {
        get { return selecte; }
    }
}
