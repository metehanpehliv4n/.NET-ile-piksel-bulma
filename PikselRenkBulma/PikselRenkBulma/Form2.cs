using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PikselRenkBulma
{
    public partial class Form2 : Form
    {
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInf);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);


        public Form2()
        {
            InitializeComponent();
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string inputHexColorCode = txtRenkBul.Text;
            SearchPixel(inputHexColorCode);
        }

        private bool SearchPixel(string hexcode)
        {

            Bitmap bitmap = new Bitmap(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height); 

            Graphics graphics = Graphics.FromImage(bitmap as Image);

            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size); 

            Color desiredPixelColor = ColorTranslator.FromHtml(hexcode);

            for (int x = 0; x < SystemInformation.VirtualScreen.Width; x++)
            {
                for (int y = 0; y < SystemInformation.VirtualScreen.Height; y++)
                {
                    Color currentPixelColor = bitmap.GetPixel(x, y);

                    if (desiredPixelColor == currentPixelColor)
                    {
                        MessageBox.Show("Piksel bulundu. Tıklanıyor...");
                        DoubleClickAtPosition(x, y);
                        return true;
                    }

                }
            }

            MessageBox.Show("Piksel bulunamadı.");
            return false;
        }

        private void DoubleClickAtPosition(int posX, int posY)
        {
            SetCursorPos(posX, posY);

            Click();
            System.Threading.Thread.Sleep(250);
            Click();
        }

        private new void Click()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        private void txtRenkBul_Click(object sender, EventArgs e)
        {
            txtRenkBul.Text = null;
            txtRenkBul.ForeColor = Color.Black;
        }
        private void btnGeri_Click(object sender, EventArgs e)
        {
            Form1 ff = new Form1();
            ff.Show();
            this.Hide();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            txtRenkBul.Text = "#ffffff";
            txtRenkBul.ForeColor = Color.Gray;
        }
    }
}
