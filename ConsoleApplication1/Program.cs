
using System.Drawing;
using System;

using System.Windows.Forms;

//Работа с нажатиями клавиш - ToDo

public class TryKey : Form
{
    private char theKey = 'd';

    public TryKey()
    {
        Size = new Size(300, 200);
        BackColor = Color.White;
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.DrawString(theKey.ToString(), new Font("Arial", 36, FontStyle.Bold), Brushes.Red, 100, 50);
        base.OnPaint(e);
    }
    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (e.Control)
        {
            Console.WriteLine("Control");
        }
        else if (e.KeyCode == Keys.Right)
        {
            Console.WriteLine("Right");
        }
        else if (e.KeyCode == Keys.Left)
        {
            Console.WriteLine("Left");
        }
        else if (e.KeyCode == Keys.Down)
        {
            Console.WriteLine("Down");
        }
        else if (e.KeyCode == Keys.Up)
        {
            Console.WriteLine("Up");
        }
        else if (e.KeyCode == Keys.Space)
        {
            Console.WriteLine("Fire!!");
        }

        Invalidate();
        base.OnKeyDown(e);
    }
    protected override void OnKeyUp(KeyEventArgs e)
    {
        Console.WriteLine("Key go Up");
        base.OnKeyUp(e);
    }
    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        if (char.IsLetterOrDigit(e.KeyChar))
        {
            theKey = e.KeyChar;
        }
        Invalidate();
        base.OnKeyPress(e);
    }
    public static void Main()
    {
        Application.Run(new TryKey());

    }
}
