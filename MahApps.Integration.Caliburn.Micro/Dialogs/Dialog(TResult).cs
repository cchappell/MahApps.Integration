using System.Threading.Tasks;
using Caliburn.Micro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace MahApps.Integration.Caliburn.Micro.Dialogs
{

    /// <summary>
    /// Coroutine that displays a dialog that returns a result.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <seealso cref="MahApps.Integration.Caliburn.Micro.Dialogs.Dialog" />
    /// <seealso cref="Caliburn.Micro.IResult{TResult}" />
    public class Dialog<TResult> : Dialog, IResult<TResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Dialog{TResult}"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Dialog(string name) 
            : base(name)
        {
        }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public TResult Result { get; set; }

        /// <inheritdoc />
        protected override Task CloseDialog(MetroWindow window, BaseMetroDialog dialog)
        {
            Result = (TResult)Dialogs.CloseDialog.GetResult(dialog);

            return base.CloseDialog(window, dialog);
        }
    }
}