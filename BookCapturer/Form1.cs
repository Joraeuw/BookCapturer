using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using WindowsInput;
using System.Threading;

namespace BookCapturer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int pageCount = 308;

            InputSimulator simulator = new InputSimulator();
            for (int i = 0; i < pageCount; i++)
            {
                string dir = $"{textBox1.Text}\\page{i}.jpg";
                CaptureMyScreen(dir);
                MoveToNextPage(simulator);
            }
        }

        private void MoveToNextPage(InputSimulator simulator)
        {
            simulator.Mouse.MoveMouseTo(convertToSimulatorValueX(798), convertToSimulatorValueY(220));
            Thread.Sleep(300);
            simulator.Mouse.MoveMouseTo(convertToSimulatorValueX(668), convertToSimulatorValueY(111));
            Thread.Sleep(300);
            simulator.Mouse.LeftButtonClick();
            Thread.Sleep(300);
            simulator.Mouse.MoveMouseTo(convertToSimulatorValueX(0), convertToSimulatorValueY(0));
            Thread.Sleep(300);
        }

        private double convertToSimulatorValueX(int coordinate)
        {
            return Convert.ToDouble(coordinate * 65535 / (Screen.PrimaryScreen.Bounds.Width - 1));
        }
        private double convertToSimulatorValueY(int coordinate)
        {
            return Convert.ToDouble(coordinate * 65535 / (Screen.PrimaryScreen.Bounds.Height - 1));
        }

        private void CaptureMyScreen(string dir)
        {
            InputSimulator simulator = new InputSimulator();
            try
            {
                //creating a bitmap with the size of the image
                Bitmap captureBitmap = new Bitmap(683, 884, PixelFormat.Format64bppPArgb);

                //coordinate from witch to start (should take from mouse click)
                //data for my device: {X=633,Y=102,Width=683,Height=884}
                Rectangle captureRectangle = Screen.AllScreens[0].Bounds;

                //Creating a New Graphics Object
                Graphics captureGraphics = Graphics.FromImage(captureBitmap);

                //Copying Image from The Screen
                captureGraphics.CopyFromScreen(633, 102, 0, 0, captureRectangle.Size);

                captureBitmap.Save(dir, ImageFormat.Jpeg);

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
