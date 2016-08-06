using System;
using Caliburn.Micro;

namespace MahApps.Integration.Caliburn.Micro.Dialogs
{
    /// <summary>
    /// Responsible for closing a progress dialog that was opened using a <see cref="MetroLoader"/>.
    /// </summary>
    /// <seealso cref="MahApps.Integration.Caliburn.Micro.ResultBase" />
    public class MetroLoaderComplete : ResultBase
    {
        private readonly MetroLoader loader;

        internal MetroLoaderComplete(MetroLoader loader)
        {
            this.loader = loader;
        }

        /// <inheritdoc />
        public async override void Execute(CoroutineExecutionContext context)
        {
            var controller = loader.Controller;
            if (controller == null)
            {
                throw new InvalidOperationException("The progress dialog is not showing.");
            }

            await controller.CloseAsync();
            OnCompleted();
        }
    }
}
