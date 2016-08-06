using System.Threading.Tasks;
using Caliburn.Micro;
using MahApps.Integration.Caliburn.Micro.Dialogs.Base;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace MahApps.Integration.Caliburn.Micro.Dialogs
{
    /// <summary>
    /// Request login credentials within the open window.
    /// </summary>
    /// <seealso cref="MahApps.Integration.Caliburn.Micro.Dialogs.Dialog" />
    /// <seealso cref="Caliburn.Micro.IResult{MahApps.Metro.Controls.Dialogs.LoginDialogData}" />
    public class MetroLogin : MessageBase, IResult<LoginDialogData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetroLogin"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public MetroLogin(string title, string message)
            : base(title, message)
        {
        }

        /// <inheritdoc />
        public LoginDialogData Result { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        public LoginDialogSettings Settings { get; set; }

        /// <inheritdoc />
        protected async override Task ShowWindow(MetroWindow window, string title, string message, CoroutineExecutionContext context)
        {
            Result = await window.ShowLoginAsync(title, message, Settings);
        }
    }
}