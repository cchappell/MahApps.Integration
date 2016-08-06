using System.Threading.Tasks;
using Caliburn.Micro;
using MahApps.Integration.Caliburn.Micro.Dialogs.Base;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace MahApps.Integration.Caliburn.Micro.Dialogs
{
    /// <summary>
    /// A coroutine for displaying a message to the user.
    /// </summary>
    public class MetroMessage : MessageBase, IResult<MessageDialogResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetroMessage"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public MetroMessage(string title, string message)
            : base(title, message)
        {
        }

        /// <summary>
        /// Gets or sets the buttons to present to the user.
        /// </summary>
        public MessageDialogStyle Buttons { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        public MetroDialogSettings Settings { get; set; }

        /// <summary>
        /// Gets the result.
        /// </summary>
        public MessageDialogResult Result { get; set; }

        /// <inheritdoc />
        protected async override Task ShowWindow(MetroWindow window, string title, string message, CoroutineExecutionContext context)
        {
            Result = await window.ShowMessageAsync(title, message, Buttons, Settings);
        }
    }
}