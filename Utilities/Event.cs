using System;

namespace Snake.Utilities
{
    /// <summary>A class that holds a standard <see cref="EventHandler"/> delegate, used ot create arrays of an event, kindof</summary>
    internal class Event
    {
        private event EventHandler eventHandler; //the events delegate

        /// <summary>Attempts to raise the event if it is not null</summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">Data about the raised event</param>
        internal void Raise(object sender, EventArgs e) => eventHandler?.Invoke(sender, e);

        /// <summary></summary>
        /// <param name="kEvent"></param>
        /// <param name="kDelegate"></param>
        /// <returns></returns>
        public static Event operator +(Event kEvent, EventHandler kDelegate)
        {
            kEvent.eventHandler += kDelegate;
            return kEvent;
        }

        /// <summary></summary>
        /// <param name="kEvent"></param>
        /// <param name="kDelegate"></param>
        /// <returns></returns>
        public static Event operator -(Event kEvent, EventHandler kDelegate)
        {
            kEvent.eventHandler -= kDelegate;
            return kEvent;
        }
    }

    /// <summary>A class that holds a standard <see cref="EventHandler{T}"/> delegate, used ot create arrays of an event, kindof</summary>
    /// <typeparam name="TEventArgs">Any type that derives from <see cref="EventArgs"/></typeparam>
    internal class Event<TEventArgs> where TEventArgs : EventArgs
    {
        private event EventHandler<TEventArgs> eventHandler; //the events delegate

        /// <summary>Attempts to raise the event if it is not null</summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">Data about the raised event</param>
        internal void Raise(object sender, TEventArgs e) => eventHandler?.Invoke(sender, e);

        public static Event<TEventArgs> operator +(Event<TEventArgs> kEvent, EventHandler<TEventArgs> kDelegate)
        {
            kEvent.eventHandler += kDelegate;
            return kEvent;
        }

        public static Event<TEventArgs> operator -(Event<TEventArgs> kEvent, EventHandler<TEventArgs> kDelegate)
        {
            kEvent.eventHandler -= kDelegate;
            return kEvent;
        }
    }
}