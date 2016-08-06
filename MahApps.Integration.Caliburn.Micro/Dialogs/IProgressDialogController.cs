using System.Threading.Tasks;

namespace MahApps.Integration.Caliburn.Micro.Dialogs
{
    /// <summary>
    /// Responsible for controlling a progress dialog.
    /// </summary>
    public interface IProgressDialogController
    {
        /// <summary>
        /// Gets a value indicating whether the loading is canceled.
        /// </summary>
        bool IsCanceled { get; }

        /// <summary>
        /// Gets or sets the maximum restriction of the progress Value property
        /// </summary>
        double Maximum { get; set; }

        /// <summary>
        /// Gets or sets the minimum restriction of the progress Value property
        /// </summary>
        double Minimum { get; set; }

        /// <summary>
        /// Begin closing the progress dialog.
        /// </summary>
        /// <returns></returns>
        Task CloseAsync();

        /// <summary>
        /// Enables cancellation of the progress dialog.
        /// </summary>
        /// <param name="value">if set to <c>true</c> is cancelable.</param>
        void SetCancelable(bool value);

        /// <summary>
        /// Makes the progress bar indeterminate.
        /// </summary>
        void SetIndeterminate();

        /// <summary>
        /// Sets the dialog's message.
        /// </summary>
        /// <param name="message">The message.</param>
        void SetMessage(string message);

        /// <summary>
        /// Sets the progress and makes the progress bar determinate.
        /// </summary>
        /// <param name="value">The value.</param>
        void SetProgress(double value);

        /// <summary>
        /// Sets the title.
        /// </summary>
        /// <param name="title">The title.</param>
        void SetTitle(string title);
    }
}