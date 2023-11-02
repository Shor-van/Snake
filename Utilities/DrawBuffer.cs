using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

using Microsoft.Win32.SafeHandles;

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

        /// <summary>Get the width of the buffer</summary>
        internal int BufferWidth => width;

        /// <summary>Get the height of the buffer</summary>
        internal int BufferHeight => height;

        /// <summary>Get the right most point of the buffer</summary>
        internal short Right => right;

        /// <summary>Get the buttom most point of the buffer</summary>
        internal short Bottom => bottom;
        
        /// <summary>Get or set the current foreground <see cref="ConsoleColor"/> of the buffer</summary>
        internal ConsoleColor ForegroundColor { get => foregroundColor; set => foregroundColor = value; }
        
        /// <summary>Get or set the current background <see cref="ConsoleColor"/> of the buffer</summary>
        internal ConsoleColor BackgroundColor { get => backgroundColor; set => backgroundColor = value; }

        /// <summary>Create a new instance of <see cref="DrawBuffer"/> with the given properties</summary>
        /// <param name="left">The left most point the buffer will be drawn relative to the <see cref="Console"/> window</param>
        /// <param name="top">The top most point the buffer will be drawn relative to the <see cref="Console"/> window</param>
        /// <param name="width">The maximum width of the buffer</param>
        /// <param name="height">The maximum height of the buffer</param>
        /// <param name="textEncoding">The text <see cref="Encoding"/> to use</param>
        /// <param name="foreground">The initial foreground <see cref="ConsoleColor"/> of the buffer</param>
        /// <param name="background">The initial background <see cref="ConsoleColor"/> of the buffer</param>
        /// <exception cref="Exception">Thrown if failed to create a output handle</exception>
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

        internal void SetChar((int x, int y) coords, char c, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChar((short)coords.x, (short)coords.y, c, foregroundColor, backgroundColor, overline, leftline, rightline, underline);

        internal void SetChar(int x, int y, char c, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChar((short)x, (short)y, c, foregroundColor, backgroundColor, overline, leftline, rightline, underline);

        internal void SetChar(short x, short y, char c, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChar(x, y, c, foregroundColor, backgroundColor, overline, leftline, rightline, underline);

        internal void SetChar((int x, int y) coords, char c, ConsoleColor foreground, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChar((short)coords.x, (short)coords.y, c, foreground, backgroundColor, overline, leftline, rightline, underline);

        internal void SetChar(int x, int y, char c, ConsoleColor foreground, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChar((short)x, (short)y, c, foreground, backgroundColor, overline, leftline, rightline, underline);

        internal void SetChar(short x, short y, char c, ConsoleColor foreground, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChar(x, y, c, foreground, backgroundColor, overline, leftline, rightline, underline);
        
        internal void SetChar((int x, int y) coords, char c, ConsoleColor foreground, ConsoleColor background, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChar((short)coords.x, (short)coords.y, c, foreground, background, overline, leftline, rightline, underline);

        internal void SetChar(int x, int y, char c, ConsoleColor foreground, ConsoleColor background, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChar((short)x, (short)y, c, foreground, background, overline, leftline, rightline, underline);

        internal void SetChar(short x, short y, char c, ConsoleColor foreground, ConsoleColor background, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false) 
        {
            short address = (short)(width * y + x);
            
            if (address < 0 || address >= buffer.Length) //check that the address is within the buffer
                throw new ArgumentOutOfRangeException(nameof(x) + "/" + nameof(y) + " must be a valid location within the buffer");

            short colorset = Colorset(foreground, background);
            short gridset = Gridset(overline, leftline, rightline);
            buffer[address].Char = c;
            buffer[address].Attributes = (short)(colorset + gridset);

            if (underline) {
                if (y == height - 1) return;
                address = (short)(width * (y + 1) + x);
                buffer[address].Attributes |= overlineBit;
            }
        }

        internal void SetChars((int x, int y) coords, char[] chars, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChars((short)coords.x, (short)coords.y, chars, foregroundColor, backgroundColor, overline, leftline, rightline, underline);

        internal void SetChars(int x, int y, char[] chars, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChars((short)x, (short)y, chars, foregroundColor, backgroundColor, overline, leftline, rightline, underline);

        internal void SetChars(short x, short y, char[] chars, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChars(x, y, chars, foregroundColor, backgroundColor, overline, leftline, rightline, underline);

        internal void SetChars((int x, int y) coords, char[] chars, ConsoleColor foreground, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChars((short)coords.x, (short)coords.y, chars, foreground, backgroundColor, overline, leftline, rightline, underline);

        internal void SetChars(int x, int y, char[] chars, ConsoleColor foreground, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChars((short)x, (short)y, chars, foreground, backgroundColor, overline, leftline, rightline, underline);

        internal void SetChars(short x, short y, char[] chars, ConsoleColor foreground, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChars(x, y, chars, foreground, backgroundColor, overline, leftline, rightline, underline);
        
        internal void SetChars((int x, int y) coords, char[] chars, ConsoleColor foreground, ConsoleColor background, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChars((short)coords.x, (short)coords.y, chars, foreground, background, overline, leftline, rightline, underline);

        internal void SetChars(int x, int y, char[] chars, ConsoleColor foreground, ConsoleColor background, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChars((short)x, (short)y, chars, foreground, background, overline, leftline, rightline, underline);

        internal void SetChars(short x, short y, char[] chars, ConsoleColor foreground, ConsoleColor background, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false) 
        {
            short address = (short)(width * y + x);

            if (address < 0 || address + (chars.Length - 1) >= buffer.Length) //check that the address is within the buffer
                throw new ArgumentOutOfRangeException(nameof(x) + "/" + nameof(y) + " must be a valid location within the buffer");

            short colorset = Colorset(foreground, background);
            short gridset = Gridset(overline, leftline, rightline);
            for (int i = 0; i < chars.Length; i++) {
                buffer[address + i].Char = chars[i];
                buffer[address + i].Attributes = (short)(colorset + gridset);

                if (underline) {
                    if (y == height - 1) continue;
                    buffer[(width * (y + 1) + x) + i].Attributes |= overlineBit;
                }
            }
        }

        internal void Write((int x, int y) coords, string text, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChars((short)coords.x, (short)coords.y, text.ToCharArray(), foregroundColor, backgroundColor, overline, leftline, rightline, underline);

        internal void Write(int x, int y, string text, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChars((short)x, (short)y, text.ToCharArray(), foregroundColor, backgroundColor, overline, leftline, rightline, underline);

        internal void Write(short x, short y, string text, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChars(x, y, text.ToCharArray(), foregroundColor, backgroundColor, overline, leftline, rightline, underline);

        internal void Write((int x, int y) coords, string text, ConsoleColor foreground,  bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChars((short)coords.x, (short)coords.y, text.ToCharArray(), foreground, backgroundColor, overline, leftline, rightline, underline);

        internal void Write(int x, int y, string text, ConsoleColor foreground,  bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChars((short)x, (short)y, text.ToCharArray(), foreground, backgroundColor, overline, leftline, rightline, underline);

        internal void Write(short x, short y, string text, ConsoleColor foreground,  bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChars(x, y, text.ToCharArray(), foreground, backgroundColor, overline, leftline, rightline, underline);
        
        internal void Write((int x, int y) coords, string text, ConsoleColor foreground, ConsoleColor background, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChars((short)coords.x, (short)coords.y, text.ToCharArray(), foreground, background, overline, leftline, rightline, underline);

        internal void Write(int x, int y, string text, ConsoleColor foreground, ConsoleColor background, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChars((short)x, (short)y, text.ToCharArray(), foreground, background, overline, leftline, rightline, underline);

        internal void Write(short x, short y, string text, ConsoleColor foreground, ConsoleColor background, bool overline = false, bool leftline = false, bool rightline = false, bool underline = false)
            => SetChars(x, y, text.ToCharArray(), foreground, background, overline, leftline, rightline, underline);

        /// <summary>Fill the buffer with the specified <see cref="char"/> with the current colors</summary>
        /// <param name="c">The <see cref="char"/> to fill the buffer with</param>
        internal void FillBuffer(char c) => FillBuffer(c, foregroundColor, backgroundColor);

        /// <summary>Fill the buffer with the specified <see cref="char"/> with the specified colors</summary>
        /// <param name="c">The <see cref="char"/> to fill the buffer with</param>
        /// <param name="foreground">The foreground <see cref="ConsoleColor"/> to use</param>
        /// <param name="background">The background <see cref="ConsoleColor"/> to use</param>
        internal void FillBuffer(char c, ConsoleColor foreground, ConsoleColor background) 
        {
            short colorset = Colorset(foreground, background);
            for (int i = 0; i < buffer.Length; i++) {
                buffer[i].Attributes = colorset;
                buffer[i].Char = c;
            }
        }

        /// <summary>Clear the buffer using the specifed colors</summary>
        /// <param name="foreground">The foreground <see cref="ConsoleColor"/> to use</param>
        /// <param name="background">The background <see cref="ConsoleColor"/> to use</param>
        internal void Clear(ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black) => FillBuffer(' ', foreground, background);

        /// <summary>Clear the buffer using the current colors</summary>
        internal void Clear() => FillBuffer(' ', foregroundColor, backgroundColor);

        /// <summary>Draws the contents of the buffer to the <see cref="Console"/></summary>
        /// <param name="clearBuffer">wether to clear the buffer after the draw</param>
        internal void FiinalizeDrow(bool clearBuffer = false)
        {
            Rectangle rect = new Rectangle(left, top, right, bottom);
            WriteConsoleOutputW(outputHandle, buffer, new Coord(width, height), new Coord(0, 0), ref rect);

            if(clearBuffer == true) Clear();
        }

        private short Colorset(ConsoleColor foreground, ConsoleColor background) => (short)(foreground + ((short)background << 4));

        private short Gridset(bool overline, bool leftline, bool rightline) => (short)((overline ? overlineBit : 0) + (leftline ? leftlineBit : 0) + (rightline ? rightlineBit : 0));

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