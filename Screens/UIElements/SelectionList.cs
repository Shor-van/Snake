using System;

using Snake.Utilities;

namespace Snake.Screens
{
    /// <summary>A list of different types of alignments</summary>
    internal enum Alignment { Left = 0, Center = 1, Right = 2 }

    /// <summary>A UI Element that represents a menu selection list wherein the user can select an option from a list of options</summary>
    internal sealed class SelectionList
    {
        private int x, y; //the X and Y position of the selection list
        private int highlightedIdx; //the index of the currently highlighted option
        private readonly string highlightPrompte; 
        private readonly string[] options; //the text iin this selection list
        private readonly ConsoleColor textColor, highlightedColor; //the colors used to draw the text
        private readonly Event[] selected; //an array of all the option selected event handlers
        private readonly int width; //the width(longest option) of the selection list
        private readonly Alignment textAlignment; //the text alignment

        /// <summary>An event rasied when the highlighted option chnages</summary>
        internal event EventHandler HighlightedChanged;

        /// <summary>Get or set the X(left most) position of the selection list</summary>
        internal int X { get => x; set => x = value; }
        
        /// <summary>Get or set the Y(top most) position of the selection list</summary>
        internal int Y { get => y; set => y = value; }

        /// <summary>Get the maximum width(longest option) of the selection list</summary>
        internal int Width => width;

        /// <summary>Get the height(number of options) of the selection list</summary>
        internal int Height => options.Length;

        /// <summary>Creats a new instance of <see cref="SelectionList"/> with the given properties</summary>
        /// <param name="x">The X(left most) position of the selection list</param>
        /// <param name="y">The Y(top most) position of the selection list</param>
        /// <param name="options">A <see cref="Array"/> of <see cref="string"/> containing the options to choose from</param>
        /// <param name="textColor">The <see cref="ConsoleColor"/> used to draw the non highlighted options</param>
        /// <param name="highlightedColor">The <see cref="ConsoleColor"/> used to draw the highlighted option</param>
        /// <param name="textAlignment">The text <see cref="Alignment"/> for the selection list</param>
        /// <param name="highlightPrompte">The text <see cref="Alignment"/> for the selection list</param>
        /// <exception cref="ArgumentNullException">Thrown if option or any of is elements are null</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throw if there are less then 2 options</exception>
        internal SelectionList(int x, int y, string[] options, ConsoleColor textColor, ConsoleColor highlightedColor, string highlightPrompte, Alignment textAlignment)
        {
            if(options == null) //check that options is not null
                throw new ArgumentNullException(nameof(options) + " cannot be null", nameof(options));

            if(options.Length < 2) //check that there are atleast 2 options
                throw new ArgumentOutOfRangeException(nameof(options) + " needs to have at least 2 items", nameof(options));

            //check that no element is null and get longest width
            for (int i = 0; i < options.Length; i++) { 
                if (string.IsNullOrWhiteSpace(options[i]) == true) //if null or empty throw
                    throw new ArgumentNullException(nameof(options) + " cannot have null or white space elements", nameof(options));
                if(width < options[i].Length) width = options[i].Length;
            }

            this.x = x;
            this.y = y;
            this.options = options;
            this.textColor = textColor;
            this.highlightedColor = highlightedColor;
            this.highlightPrompte = highlightPrompte;
            this.textAlignment = textAlignment;
            this.width += highlightPrompte.Length;

            this.selected = new Event[options.Length];
        }

        /// <summary>Handles input for the <see cref="SelectionList"/></summary>
        ///<param name="gameTime">The object that holds info about the game's run time</param>
        internal void HandleInput(GameTime gameTime)
        {
            //Update selection
            if(Keyboard.IsNewKeyPress(ConsoleKey.Enter) == true) //enter pressed, option selected
                OnOptionSelected(highlightedIdx, EventArgs.Empty);
            else if (Keyboard.IsNewKeyPress(ConsoleKey.W) == true) //move highlighted index up
                highlightedIdx = highlightedIdx == 0 ? options.Length - 1 : highlightedIdx - 1;
            else if (Keyboard.IsNewKeyPress(ConsoleKey.S) == true) //move highlighted index down
                highlightedIdx = highlightedIdx == options.Length - 1 ? 0 : highlightedIdx + 1;
        }

        /// <summary>Updates the <see cref="SelectionList"/>, does nothing</summary>
        ///<param name="gameTime">The object that holds info about the game's run time</param>
        internal void Update(GameTime gameTime) { }

        /// <summary>Draws the <see cref="SelectionList"/> at its current X/Y location</summary>
        ///<param name="gameTime">The object that holds info about the game's run time</param>
        internal void Draw(GameTime gameTime)
        {
            int halfWidth = width / 2;
            ConsoleColor current = Console.ForegroundColor;
            for (int i = 0; i < options.Length; i++) {
                int optLen = options[i].Length + highlightPrompte.Length;
                Console.ForegroundColor = i == highlightedIdx ? highlightedColor : textColor;

                Console.CursorTop = y + i; //set cursor position
                Console.CursorLeft = textAlignment == Alignment.Left || optLen == width ? x : 
                    x + (textAlignment == Alignment.Center ? halfWidth - (optLen / 2) : width - optLen);
                
                Console.Write((i == highlightedIdx ? highlightPrompte : new string(' ', highlightPrompte.Length)) + options[i]);
            }
            Console.ForegroundColor = current;
        }

        /// <summary>Adds the specified selected <see cref="EventHandler"/> delegate of the specified option</summary>
        /// <param name="idx">The index of the option to add the delegate to</param>
        /// <param name="eventDelegate">The <see cref="EventHandler"/> compliant method to call when the specified option is selected</param>
        internal void Add_SelectedHandlerFor(int idx, EventHandler eventDelegate) 
        {
            if(idx < 0 || idx > selected.Length)
                throw new ArgumentOutOfRangeException(nameof(idx) + " cannot be less then 0 or greater then " + nameof(selected) + ".Length", nameof(idx));

            if(selected[idx] == null) selected[idx] = new Event(); 
            selected[idx] += eventDelegate;
        }

        /// <summary>Removes the specified selected <see cref="EventHandler"/> delegate for the specified option</summary>
        /// <param name="option">The index of the option to remove the delegate from</param>
        /// <param name="eventDelegate">The <see cref="EventHandler"/> compliant method remove</param>
        internal void Remove_SelectedHandlerFor(int idx, EventHandler eventDelegate) 
        {
            if(idx < 0 || idx > selected.Length)
                throw new ArgumentOutOfRangeException(nameof(idx) + " cannot be less then 0 or greater then " + nameof(selected) + ".Length", nameof(idx));

            if(selected[idx] != null)
                selected[idx] -= eventDelegate; 
        }

        /// <summary>Gets index of the specified option</summary>
        /// <param name="option">The option of whos index to get</param>
        /// <returns>The index of the specified option</returns>
        /// <exception cref="ArgumentNullException">Throw if the option is not found</exception>
        private int GetIndexForOption(string option)
        {
            int idx; for (idx = 0; idx < options.Length; idx++)
                if (options[idx] == option)
                    break;

            if (idx == options.Length)
                throw new ArgumentNullException("Failed to find option:" + option, nameof(option));
            return idx;
        }

        /// <summary>Adds the specified selected <see cref="EventHandler"/> delegate of the specified option</summary>
        /// <param name="option">The option to add the delegate to</param>
        /// <param name="eventDelegate">The <see cref="EventHandler"/> compliant method to call when the specified option is selected</param>
        /// <exception cref="ArgumentNullException">Throw if the option is not found</exception>
        internal void Add_SelectedHandlerFor(string option, EventHandler eventDelegate) => Add_SelectedHandlerFor(GetIndexForOption(option), eventDelegate);

        /// <summary>Removes the specified selected <see cref="EventHandler"/> delegate for the specified option</summary>
        /// <param name="option">The option to remove the delegate from</param>
        /// <param name="eventDelegate">The <see cref="EventHandler"/> compliant method remove</param>
        /// <exception cref="ArgumentNullException">Throw if the option is not found</exception>
        internal void Remove_SelectedHandlerFor(string option, EventHandler eventDelegate) => Remove_SelectedHandlerFor(GetIndexForOption(option), eventDelegate);

        /// <summary>Rasies the <see cref="HighlightedChanged"/> event</summary>
        /// <param name="e">The <see cref="EventArgs"/> object containing info on the rasied event</param>
        private void OnHighlightedChnaged(EventArgs e) => HighlightedChanged?.Invoke(this, e);

        /// <summary>Rasies the selected event for the specified option index</summary>
        /// <param name="idx">The index of the selected option to raise the selected event for</param>
        /// <param name="e">The <see cref="EventArgs"/> object containing info on the rasied event</param>
        private void OnOptionSelected(int idx, EventArgs e) => selected[idx]?.Raise(this, e);
    }
}