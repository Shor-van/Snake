using System;
using System.Drawing;
using System.Runtime.InteropServices;

public class Handler
{
	Graphics buffer;//Used to render 
	Bitmap img;//Render object that will be visible
    
    private const int StandardOutputHandle = -11;

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern IntPtr GetStdHandle(int nStdHandle);


    /// <summary>P/Invoke to native windows function GetConsoleWindow()</summary>
    /// <returns><see cref="IntPtr"/> to the console window</returns>
    [DllImport("kernel32.dll")] private static extern IntPtr GetConsoleWindow();

    /// <summary>P/Invoke to native windows function GetConsoleFontSize(IntPtr, int)</summary>
    /// <param name="hConsoleOutput">A handle to the console screen buffer</param>
    /// <param name="nFont">The index of the font whose size is to be retrieved. This index is obtained by calling the GetCurrentConsoleFont function</param>
    /// <returns>If the function succeeds, the return value is a COORD</returns>
    [DllImport("kernel32.dll")] private static extern Size GetConsoleFontSize(IntPtr hConsoleOutput, int nFont);

	public Handler(int width, int height)
	{
		img = new Bitmap(width, height);
		buffer = Graphics.FromImage(img);
	}

	public void DrawImage(Image toDraw, Point location, Size size)
	{
		Size fontSize = GetConsoleFontSize(GetStdHandle(StandardOutputHandle), 0);//Get the font sie to calculate accurate image bounds
		Rectangle imgRect = new Rectangle(//Calculate image bounds
		location.X * fontSize.Width,
		location.Y * fontSize.Height,
		size.Width * fontSize.Width,
		size.Height * fontSize.Height);
		buffer.DrawImage(img, imgRect);//Use the 'buffer' Graphic instance to render the image to the Bitmap 'img'
	}

	public void Render()
	{
		Graphics render = Graphics.FromHwnd(GetConsoleWindow());
		//Grab the Console handle for drawing
		render.DrawImage(img, 0f, 0f);//draw rendered image to Console
		buffer.Clear(Color.Gray); //use 'buffer' to wipe the rendered image with a background color(I used gray to have contrast to the Console background)
	}
}