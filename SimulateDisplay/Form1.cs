using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace SimulateDisplay
{
	public partial class Form1 : Form
	{
		private System.Timers.Timer timer = new System.Timers.Timer(100);
		private Display form;
		private WebBrowser webBrowser;
		public Form1()
		{
			InitializeComponent();
			timer.Elapsed += Timer_Elapsed;
			form = new Display();
			
			button2.Enabled = false;	
	
		}

		private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
		{
			//screenshot secondary display and show in picture box
			Bitmap bmpScreenshot = new Bitmap(form.Width, form.Height, PixelFormat.Format32bppArgb);
			form.DrawToBitmap(bmpScreenshot, new Rectangle(0, 0, form.ClientSize.Width, form.ClientSize.Height));
			bmpScreenshot.Save("screenshot.png", ImageFormat.Png);
			pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBox1.ImageLocation = "screenshot.png";
		}

		private void button1_Click(object sender, EventArgs e)
		{
			//show a new form and stream its contents to the picture box

			form.ShowDialog();
			button2.Enabled = true;
			//start capturing frames from it and display in the picture boc
			Bitmap bmpScreenshot = new Bitmap(form.Width, form.Height, PixelFormat.Format32bppArgb);
			form.DrawToBitmap(bmpScreenshot, new Rectangle(0, 0, form.ClientSize.Width, form.ClientSize.Height));
			bmpScreenshot.Save("screenshot.png", ImageFormat.Png);
			pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBox1.ImageLocation = "screenshot.png";
		}

		private void button2_Click(object sender, EventArgs e)
		{
			//start a timer to screenshot the display form and display in the windows forms app
			timer.Start();
		
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//create a new shape
			var shape1 = new Circle(pictureBox1);
			shape1.Display();

		}
	}
	//build a class for the shape class that let's you draws different shapes on the picture
	//box using the picture box's graphic context
	public abstract class Shape
	{
		protected abstract void Draw();
	}
	public class Circle : Shape
	{
		//declare a private graphics context inside the class
		private PictureBox mybox;
		//require the graphics context in the ctor
		public Circle(PictureBox box)
		{
			mybox = box;
		}
		protected override void Draw()
		{
			//get the client rectangle
			var client = mybox.ClientRectangle;
			//create a new pen for drawing on the picture box
			var pen = new Pen(Brushes.AliceBlue, 3);
			var map = new Bitmap(client.Width, client.Height);
			using(var gfx = Graphics.FromImage(map)) {

				gfx.DrawEllipse(pen, client);
				gfx.FillRectangle(Brushes.Blue, client);
				
			}
			map.Save("edited.png");
			mybox.ImageLocation = "edited.png";
		}
		public void Display()
		{
			Draw();
			
		}
	}
	public class MyRectangle : Shape
	{
		//declare the picture box explicitly in the class
		PictureBox mybox;
		//define the constructor
		public MyRectangle(PictureBox box)
		{
			mybox = box;
		}
		protected override void Draw()
		{
			//get the graphics context
			var gfx = mybox.CreateGraphics();
			//define a drawing pen
			var pen = new Pen(Brushes.Beige, 3);
			//get the client rect
			var client = mybox.ClientRectangle;
			//draw the retangle on the picture box
			gfx.DrawRectangle(pen, client);
		}
	}
}