using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace SimulateDisplay
{
	public partial class Display : Form
	{
		//dictionary to map button int to itself
		private Dictionary<string, Button> buttons = new();
		//list to hold the names of the buttons
		List<string> labels = new()
		{
			"",
			"Circle",
			"Rectangle",
			"Square",
			"Triangle",
			"Polygon",
			"Line",
			"Clear",
			"Save"
		};
		//declare a global to hold the pen color and size in the class
		private Pen drawingPen;
		private Point point = new Point(69, 175);
		private Point cpoint = new Point(923, 114);
		//declare a color dialog
		private ColorDialog colorDialog;
		//count the nmber of times the control has been clicked
		private int times = 0;
		//list to hold the clicked points
		private List<Point> shape = new();
		public Display()
		{
			InitializeComponent();
			Text = "Simple Drawing Tool";
			//disable the maximize button
			MaximizeBox = false;	
			//create a new color dialog and add to the form
			colorDialog = new ColorDialog();
			AddButtons();
			MouseClick += Display_MouseClick;
			Load += Display_Load;
			panel1.BackColor = Color.White;
			panel1.BorderStyle = BorderStyle.FixedSingle;
			panel1.MouseClick += Panel1_MouseClick;//event handler for mouse click
			//register event listeners for each of the buttons
			buttons["1"].Click += DrawCircle;//event handler for the draw circle
			buttons["7"].Click += ClearPanel;//event handler for the clear drawings
			buttons["2"].Click += DrawRectangle;//draw a rectangle
			buttons["4"].Click += DrawTriangle;//draw a triangle
			buttons["8"].Click += SaveDrawing;//save the drawing
			buttons["5"].Click += Polygon_Click;
		}

		private void Polygon_Click(object? sender, EventArgs e)
		{
			//loop through the dictionat and disable the ther buttons
			foreach(var button in buttons)
			{
				//check the key first before disabling
				if (button.Key != "5")
				{
					button.Value.Enabled = false;
				}
			}
		}

		private void Panel1_MouseClick(object? sender, MouseEventArgs e)
		{
			times += 1;
			var point = new Point(e.X, e.Y);
			//add the point to a list
			shape.Add(point);
			if (times == 3)
			{
				DrawShape();
				
			}

			if (times == 2)
			{
				//ask the user if he wants to draw a circle or a  line
				System.Windows.Forms.ContextMenuStrip menuStrip = new ContextMenuStrip();
				//set the location of the context menu
				var line = new ToolStripMenuItem("Line");
				var circle = new ToolStripMenuItem("Circle");
				//what happens when the line is clicked
				line.Click += LineOnClick;
				//what happens when circle is clicked
				circle.Click += CircleOnClick;
				menuStrip.Items.Add(line);
				menuStrip.Items.Add(circle);
				menuStrip.Show(new Point(panel1.Location.X+panel1.Width/2,panel1.Location.Y+panel1.Height/2));
			}
			if (times == 4)
			{
				DrawPolygon();
			}

		}
		void DrawPolygon()
		{
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				drawingPen = new Pen(colorDialog.Color, 4);
			}
			else
			{
				drawingPen = new Pen(Color.DeepSkyBlue, 4);	
			}
			panel1.CreateGraphics().DrawPolygon(drawingPen, shape.ToArray());
			shape.Clear();
			times = 0;
		}
		private void Quad_Click(object? sender, EventArgs e)
		{
			
		}

		private void CircleOnClick(object? sender, EventArgs e)
		{
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				drawingPen = new Pen(colorDialog.Color, 3);
				
			}
			else
			{
				drawingPen = new Pen(Color.Blue, 3);
			}
			//the first point is the center of the rectangle, this needs to be a very clever
			//computation
			var firstPoint = shape[0];
			var secondPoint = shape[1];
			// Calculate the distance between the two points using the distance formula.
			double distance = Math.Sqrt(Math.Pow(secondPoint.X - firstPoint.X, 2) + Math.Pow(secondPoint.Y - firstPoint.Y, 2));
			int radius = (int)distance;

			// Calculate the top-left corner of the bounding rectangle of the circle.
			int x = firstPoint.X - radius;
			int y = firstPoint.Y - radius;

			// Draw the circle on the panel.
			Graphics g = panel1.CreateGraphics();
			g.DrawEllipse(drawingPen, x, y, radius * 2, radius * 2);
			//clear the shape and counter
			shape.Clear();
			times = 0;

		}

		private void LineOnClick(object? sender, EventArgs e)
		{
			//draw a line between the two points
			panel1.CreateGraphics().DrawLine(new Pen(Brushes.DarkSlateBlue,4),shape[0], shape[1]);
			//clear the list
			shape.Clear();
			//clear the times
			times = 0;
		}

		void DrawShape()
		{
			if(colorDialog.ShowDialog()==DialogResult.OK) {
				drawingPen = new Pen(colorDialog.Color, 5);
				panel1.CreateGraphics().DrawPolygon(drawingPen, shape.ToArray());
				//reset the counter
				times = 0;
				shape.Clear();
			}
			
		}

		private void DrawTriangle(object? sender, EventArgs e)
		{
			var choice = Microsoft.VisualBasic.Interaction.MsgBox("Would you like to draw this figure by clicking on the points on the panel?", MsgBoxStyle.OkCancel);
			switch (choice)
			{
				case MsgBoxResult.Ok:
					if (times == 3)
					{
						//choose a color
						if (colorDialog.ShowDialog() == DialogResult.OK)
						{
							drawingPen = new Pen(drawingPen.Color, 5);
							//listen for the points and draw
							panel1.CreateGraphics().DrawPolygon(drawingPen, shape.ToArray());
						}
					}
					
					break;
			}
		}
		void ListenForPointsAndDraw(Pen pen)
		{
			//wait for the in class count variable to be three

		}
		private void DrawRectangle(object? sender, EventArgs e)
		{

		}
		private void DrawCircle(object? sender, EventArgs e)
		{
			//specify the color
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				//get the color chosen
				drawingPen = new Pen(colorDialog.Color, 5);
			}
			else
			{
				drawingPen = new Pen(Brushes.DarkRed, 5);
			}
			//define a pen, remember to make this object global
			var pen = new Pen(Brushes.DarkRed, 5);
			//prompt the user to enter th radius of the circle
			var radius = Microsoft.VisualBasic.Interaction.InputBox("Enter the radius of the cirlce", "Radius Input");
			var empty = radius == string.Empty;
			if (empty)
			{
				MessageBox.Show("The radius input is required");
				return;
			}
			else
			{
				//parse for an integer input
				if (Int32.TryParse(radius, out var res))
				{
					//the input is okay, draw the circle
					var radii = res;
					//get the center of the panel as the center
					var point = new Point(panel1.Location.X + panel1.Width / 2, panel1.Location.Y / 2);
					var rect = new Rectangle(point, new Size(radii, radii));
					//draw the circle on the panel
					panel1.CreateGraphics().DrawEllipse(drawingPen, rect);
				}
				else
				{
					MessageBox.Show("You entered an invalid input", "Icorrect Input");
				}
			}
		}

		private void ClearPanel(object? sender, EventArgs e)
		{
			var confirm =
				Microsoft.VisualBasic.Interaction.MsgBox(
					"This will clear all the drawings in the panel, are you sure yo want to continue?", MsgBoxStyle.OkCancel);
			//check the result and either clear the drawings or cancel
			switch (confirm)
			{
				case MsgBoxResult.Ok:
					panel1.Invalidate();
					break;
				case MsgBoxResult.Cancel:
					//do nothing
					break;
			}
		}
		private void OnClick4(object? sender, EventArgs e)
		{
			//clear the panel of all drawings
			panel1.Invalidate();
		}

		private void OnClick3(object? sender, EventArgs e)
		{
			//draw a new triangle

			//define a pen
			var pen = new Pen(Brushes.Chocolate, 4);
			//define a triangle
			Point[] trianglePoints = new Point[3];
			trianglePoints[0] = new Point(50, 50);
			trianglePoints[1] = new Point(100, 150);
			trianglePoints[2] = new Point(0, 100);
			panel1.CreateGraphics().DrawPolygon(pen, trianglePoints);

		}
		private void SaveDrawing(object? sender, EventArgs e)
		{

			//browse a folder to save the file
			var folderBroswer = new FolderBrowserDialog();
			folderBroswer.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
			folderBroswer.UseDescriptionForTitle = true;
			if(folderBroswer.ShowDialog()==DialogResult.OK)
			{
				//get the folder name
				var folderName = folderBroswer.SelectedPath;
				//create a new bitmap object
				var bitmap = new Bitmap(panel1.Width, panel1.Height,PixelFormat.Format32bppArgb);
				panel1.DrawToBitmap(bitmap, new Rectangle(0,0,panel1.Width,panel1.Height));
				var fileName = "drawing.png";
				//combine he file name and the folder name
				var path = Path.Combine(folderName,fileName);
				bitmap.Save(path);
				MessageBox.Show("drawing saved successfully");
			}
		}

		private void OnClick(object? sender, EventArgs e)
		{
			//define a pen
			var pen = new Pen(Brushes.Chocolate, 4);
			//define a random rectangle size
			var rect = new Rectangle(new Point(panel1.Location.X + 30, panel1.Location.Y + 30), new Size(100, 100));
			//draw the rect on the panel
			panel1.CreateGraphics().DrawRectangle(pen, rect);
		}

		private void Display_Load(object? sender, EventArgs e)
		{

		}
		void AddButtons()
		{
			Panel buttonPanel = new Panel();
			buttonPanel.Dock = DockStyle.Top; // or whichever DockStyle you prefer
			buttonPanel.Height = 100; // set the height of the panel
			Controls.Add(buttonPanel); // add the panel to your form's controls
			for (int i = 1; i <= 8; i++)
			{
				Button button = new Button();
				button.Text = labels[i];
				button.FlatAppearance.BorderSize = 0;
				button.BackColor = Color.DarkSlateGray;//set the background color
				button.ForeColor = Color.White;
				button.Size = new Size(100, 80); // set the size of the button
				button.Location = new Point(i * 110, 5); // set the location of the button
				buttonPanel.Controls.Add(button); // add the button to the panel
				buttons.Add(i.ToString(), button);//add the button to the list
			}

		}
		private void Display_MouseClick(object? sender, MouseEventArgs e)
		{
			MessageBox.Show(e.X + "," + e.Y);
		}

		private void Display_Load_1(object sender, EventArgs e)
		{

		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
