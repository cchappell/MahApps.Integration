using System.Threading.Tasks;
using Caliburn.Micro;
using MahApps.Integration.Caliburn.Micro.Dialogs.Base;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace MahApps.Integration.Caliburn.Micro.Dialogs
{
    /// <summary>
    /// Request input from the user in a dialog.
    /// </summary>
    public class MetroInput : MessageBase, IResult<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetroInput"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public MetroInput(string title, string message)
            : base(title, message)
        {
        }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Gets or sets the dialog settings.
        /// </summary>
        public MetroDialogSettings Settings { get; set; }

        /// <inheritdoc />
        protected async override Task ShowWindow(MetroWindow window, string title, string message, CoroutineExecutionContext context)
        {
            Result = await window.ShowInputAsync(title, message, Settings);
        }
    }
}