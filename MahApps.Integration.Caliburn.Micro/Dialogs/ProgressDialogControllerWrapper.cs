using System;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;

namespace MahApps.Integration.Caliburn.Micro.Dialogs
{
    internal class ProgressDialogControllerWrapper : IProgressDialogController
    {
        private readonly ProgressDialogController controller;

        public ProgressDialogControllerWrapper(ProgressDialogController controller)
        {
            if (controller == null) throw new ArgumentNullException(nameof(controller));

            this.controller = controller;
        }

        /// <inheritdoc />
        public bool IsCanceled => controller.IsCanceled;

        /// <inheritdoc />
        public double Maximum
        {
            get { return controller.Maximum; }
            set { controller.Maximum = value; }
        }

        /// <inheritdoc />
        public double Minimum
        {
            get { return controller.Minimum; }
            set { controller.Minimum = value; }
        }

        /// <inheritdoc />
        public Task CloseAsync() => controller.CloseAsync();

        /// <inheritdoc />
        public void SetCancelable(bool value) => controller.SetCancelable(value);

        /// <inheritdoc />
        public void SetIndeterminate() => controller.SetIndeterminate();

        /// <inheritdoc />
        public void SetMessage(string message) => controller.SetMessage(message);

        /// <inheritdoc />
        public void SetProgress(double value) => controller.SetProgress(value);

        /// <inheritdoc />
        public void SetTitle(string title) => controller.SetTitle(title);
    }
}