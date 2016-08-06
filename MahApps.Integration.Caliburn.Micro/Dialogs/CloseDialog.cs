using System;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;

namespace MahApps.Integration.Caliburn.Micro.Dialogs
{
    /// <summary>
    /// Responsible for closing a custom dialog within a coroutine.
    /// </summary>
    /// <seealso cref="MahApps.Integration.Caliburn.Micro.ResultBase" />
    /// <seealso cref="Caliburn.Micro.IResult{System.Object}" />
    /// <seealso cref="Dialog{TDialog}" />
    public class CloseDialog : ResultBase, IResult<object>
    {
        /// <summary>
        /// Identifies the close dialog property
        /// </summary>
        public static readonly DependencyProperty CloseDialogProperty =
            DependencyProperty.RegisterAttached("CloseDialog", typeof(Func<Task>), typeof(CloseDialog), new PropertyMetadata(null));


        /// <summary>
        /// Identifies the result property.
        /// </summary>
        public static readonly DependencyProperty ResultProperty =
            DependencyProperty.RegisterAttached("Result", typeof(object), typeof(CloseDialog), new PropertyMetadata(0));

        /// <summary>
        /// Gets the close dialog delegate.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// The delegate if found; otherwise, <c>null</c>.
        /// </returns>
        public static Func<Task> GetCloseDialog(DependencyObject obj)
        {
            return (Func<Task>)obj.GetValue(CloseDialogProperty);
        }

        /// <summary>
        /// Sets the close dialog delegate.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetCloseDialog(DependencyObject obj, Func<Task> value)
        {
            obj.SetValue(CloseDialogProperty, value);
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The result.</returns>
        public static object GetResult(DependencyObject obj)
        {
            return obj.GetValue(ResultProperty);
        }

        /// <summary>
        /// Sets the result.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetResult(DependencyObject obj, object value)
        {
            obj.SetValue(ResultProperty, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CloseDialog"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public CloseDialog(object result = null)
        {
            Result = result;
        }

        /// <summary>
        /// Gets or sets the result to be passed to the parent coroutine.
        /// </summary>
        public object Result { get; set; }

        /// <inheritdoc />
        public async override void Execute(CoroutineExecutionContext context)
        {
            var view = context.View as BaseMetroDialog;
            if (view != null)
            {
                SetResult(view, Result);

                var closeDialog = GetCloseDialog(view);
                if (closeDialog != null)
                {
                    await closeDialog();
                    OnCompleted();
                }
            }
        }
    }
}
