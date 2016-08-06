using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MahApps.Metro.Controls;

namespace MahApps.Integration.Caliburn.Micro.Dialogs.Base
{
    /// <summary>
    /// Base class for coroutines that display a dialog.
    /// </summary>
    public abstract class DialogBase : ResultBase
    {
        /// <summary>
        /// Gets a value indicating whether the dialog must be closed manually.
        /// </summary>
        protected virtual bool RequiresManualClose
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the metro window for the current coroutine execution context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The <see cref="MetroWindow"/> if found; otherwise, <c>null</c>.
        /// </returns>
        protected static MetroWindow GetWindow(CoroutineExecutionContext context)
        {
            var view = context.Source as DependencyObject;

            if (view != null)
            {
                return Window.GetWindow(view) as MetroWindow;
            }

            return null;
        }

        /// <inheritdoc />
        public sealed async override void Execute(CoroutineExecutionContext context)
        {
            var window = GetWindow(context);

            if (window != null)
            {
                await ShowWindow(window, context);
            }

            if (!RequiresManualClose)
            {
                OnCompleted();
            }
        }

        /// <summary>
        /// Shows the window.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="context">The context.</param>
        /// <returns>The task displaying the window.</returns>
        protected abstract Task ShowWindow(MetroWindow window, CoroutineExecutionContext context);
    }
}