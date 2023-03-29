namespace SimulateDisplay
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			pictureBox1 = new PictureBox();
			button1 = new Button();
			button2 = new Button();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			// 
			// pictureBox1
			// 
			pictureBox1.Location = new Point(31, 12);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(514, 396);
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			// 
			// button1
			// 
			button1.Location = new Point(795, 55);
			button1.Name = "button1";
			button1.Size = new Size(149, 101);
			button1.TabIndex = 1;
			button1.Text = "Secondary";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// button2
			// 
			button2.Location = new Point(795, 212);
			button2.Name = "button2";
			button2.Size = new Size(149, 101);
			button2.TabIndex = 2;
			button2.Text = "Stream";
			button2.UseVisualStyleBackColor = true;
			button2.Click += button2_Click;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1013, 450);
			Controls.Add(button2);
			Controls.Add(button1);
			Controls.Add(pictureBox1);
			Name = "Form1";
			Text = "Form1";
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private PictureBox pictureBox1;
		private Button button1;
		private Button button2;
	}
}