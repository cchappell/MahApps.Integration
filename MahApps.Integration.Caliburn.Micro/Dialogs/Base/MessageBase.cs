using System.Threading.Tasks;
using Caliburn.Micro;
using MahApps.Metro.Controls;

namespace MahApps.Integration.Caliburn.Micro.Dialogs.Base
{
    /// <summary>
    /// Base class for coroutines that show a message dialog.
    /// </summary>
    public abstract class MessageBase : DialogBase
    {
        private readonly string message;
        private readonly string title;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBase"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        protected MessageBase(string title, string message)
        {
            this.title = title;
            this.message = message;
        }

        /// <inheritdoc />
        protected sealed override Task ShowWindow(MetroWindow window, CoroutineExecutionContext context)
        {
            return ShowWindow(window, title, message, context);
        }

        /// <summary>
        /// Shows the window.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        /// <returns>The task showing the window.</returns>
        protected abstract Task ShowWindow(MetroWindow window, string title, string message, CoroutineExecutionContext context);
    }
}