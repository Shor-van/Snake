using System;
using System.IO;
using System.Text;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace Snake.Utilities
{   
    /// <summary>A class that can be used to draw char's to the <see cref="Console"/> quickly by making use of a buffer
    /// <br>Note: in order for this to work we make use of windows P/Invoked functions, Here Be Dragons!</br></summary>
    internal sealed class DrawBuffer
    {
        private const short overlineBit = 0x0400;
        private  const short leftlineBit = 0x0800;
        private const short rightlineBit = 0x1000;

        private SafeFileHandle outputHandle; //the buffer's output handle
        private CharInfo[] buffer = Array.Empty<CharInfo>(); //array that holds the buffer with char data
        private short left, top, width, height, right, bottom; //the buffer's draw rectangle data
        private ConsoleColor foregroundColor, backgroundColor; //the base colors for the buffer

        /// <summary></summary>
        internal int BufferWidth => width;

        /// <summary></summary>
        internal int BufferHeight => height;

        /// <summary></summary>
        internal short Right => right;

        /// <summary></summary>
        internal short Bottom => bottom;
        
        /// <summary></summary>
        internal ConsoleColor ForegroundColor { get => foregroundColor; set => foregroundColor = value; }
        
        /// <summary></summary>
        internal ConsoleColor BackgroundColor { get => backgroundColor; set => backgroundColor = value; }

        /// <summary></summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="textEncoding"></param>
        /// <param name="foreground"></param>
        /// <param name="background"></param>
        /// <exception cref="Exception"></exception>
        internal DrawBuffer(short left, short top, short width, short height, Encoding textEncoding, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            Console.OutputEncoding = textEncoding;

            //create output handle
            outputHandle = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);

            if (outputHandle.IsInvalid) //check that the handle was created
                throw new Exception("outputHandle is invalid!");

            this.left = left;
            this.top = top;
            this.width = width;
            this.height = height;
            this.right = (short)(left + width - 1);
            this.bottom = (short)(top + height - 1);
            buffer = new CharInfo[width * height];

            foregroundColor = foreground;
            backgroundColor = background;
            Clear();
        }

         // Character attribute utilities
        private short Colorset(ConsoleColor foreground, ConsoleColor background) => (short)(foreground + ((short)background << 4));

        private short Gridset(bool overline, bool leftline, bool rightline) => (short)((overline ? overlineBit : 0) + (leftline ? leftlineBit : 0) + (rightline ? rightlineBit : 0));

        internal void FillBuffer(char c, ConsoleColor foreground, ConsoleColor background) 
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i].Attributes = Colorset(foreground, background);
                buffer[i].Char = c;
            }
        }

        internal void Clear(ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black) => FillBuffer(' ', foreground, background);

        internal void Clear() => FillBuffer(' ', foregroundColor, backgroundColor);

        /// <summary>Draws the contents of the buffer to the <see cref="Console"/></summary>
        /// <param name="clearBuffer">wether to clear the buffer after the draw</param>
        internal void DrawToConsole(bool clearBuffer = false)
        {
            Rectangle rect = new Rectangle(left, top, right, bottom);
            WriteConsoleOutputW(outputHandle, buffer, new Coord(width, height), new Coord(0, 0), ref rect);

            if(clearBuffer == true) Clear();
        }

        //P/Invoke methods
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] uint fileAccess,
            [MarshalAs(UnmanagedType.U4)] uint fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] int flags,
            IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteConsoleOutputW(
          SafeFileHandle hConsoleOutput,
          CharInfo[] lpBuffer,
          Coord dwBufferSize,
          Coord dwBufferCoord,
          ref Rectangle lpWriteRegion);
    }

    [StructLayout(LayoutKind.Sequential)] 
    internal struct Coord 
    {
        public short x, y;
        public Coord(short x, short y) { this.x = x; this.y = y; }
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct CharInfo 
    {
        [FieldOffset(0)] public ushort Char;
        [FieldOffset(2)] public short Attributes;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Rectangle 
    {
        public short left, top, right, bottom;
        public Rectangle(short left, short top, short right, short bottom) 
            { this.left = left; this.top = top; this.right = right; this.bottom = bottom; }
    }
}