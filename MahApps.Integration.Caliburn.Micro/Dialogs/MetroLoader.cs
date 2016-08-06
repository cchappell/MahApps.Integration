using System.Threading.Tasks;
using Caliburn.Micro;
using MahApps.Integration.Caliburn.Micro.Dialogs.Base;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace MahApps.Integration.Caliburn.Micro.Dialogs
{
    /// <summary>
    /// Shows a progress window.
    /// </summary>
    /// <example>
    /// This sample shows how to use the <see cref="MetroLoader"/> in a coroutine.
    /// <code>
    /// public class TestViewModel
    /// {
    ///     public IEnumerable<IResult></IResult> ShowLoader()
    ///     {
    ///         var loader = new MetroLoader("My Title", "Message");
    ///         yield return loader;
    ///
    ///         // Do work
    ///         loader.Controller.SetProgress(.5);
    ///         // Do more work
    ///
    ///         yield return loader.Complete();
    ///     }
    /// }
    /// </code>
    /// </example>
    public class MetroLoader : MessageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetroLoader"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public MetroLoader(string title, string message)
            : base(title, message)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is cancelable.
        /// </summary>
        public bool IsCancelable { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        public MetroDialogSettings Settings { get; set; }

        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        /// <remarks>
        /// The controller is available after this instance has been executed.
        /// </remarks>
        public IProgressDialogController Controller { get; set; }

        /// <summary>
        /// Closes the progress dialog.
        /// </summary>
        /// <returns>The coroutine that closes the dialog.</returns>
        public MetroLoaderComplete Complete()
        {
            return new MetroLoaderComplete(this);
        }

        /// <inheritdoc />
        protected async override Task ShowWindow(MetroWindow window, string title, string message, CoroutineExecutionContext context)
        {
            var controller = await window.ShowProgressAsync(title, message, IsCancelable, Settings);

            Controller = new ProgressDialogControllerWrapper(controller);
        }
    }
}