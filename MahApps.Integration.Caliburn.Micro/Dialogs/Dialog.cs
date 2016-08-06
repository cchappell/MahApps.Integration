using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MahApps.Integration.Caliburn.Micro.Dialogs.Base;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace MahApps.Integration.Caliburn.Micro.Dialogs
{
    /// <summary>
    /// Coroutine for displaying a <typeparamref name="TDialog"/>.
    /// </summary>
    /// <typeparam name="TDialog">The type of the dialog.</typeparam>
    /// <seealso cref="Dialogs.CloseDialog"/>
    public class Dialog : DialogBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Dialog"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Dialog(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <inheritdoc />
        protected override bool RequiresManualClose
        {
            get { return true; }
        }

        /// <inheritdoc />
        protected async override Task ShowWindow(MetroWindow window, CoroutineExecutionContext context)
        {
            var view = context.View as FrameworkElement;
            var dialog = view?.Resources[Name] as BaseMetroDialog;

            if (dialog != null)
            {
                RoutedEventHandler handler = null;

                handler = new RoutedEventHandler((s, e) =>
                {
                    var screen = (Screen)dialog.DataContext;
                    screen.Parent = context.Target;
                    ScreenExtensions.TryActivate(dialog.DataContext);

                    Dialogs.CloseDialog.SetCloseDialog(dialog, () => CloseDialog(window, dialog));

                    dialog.Loaded -= handler;
                });

                dialog.Loaded += handler;

                await window.ShowMetroDialogAsync(dialog);
            }
        }

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="dialog">The dialog.</param>
        protected virtual async Task CloseDialog(MetroWindow window, BaseMetroDialog dialog)
        {
            await window.HideMetroDialogAsync(dialog);
            OnCompleted();
        }
    }
}