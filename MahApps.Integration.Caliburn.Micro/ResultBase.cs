using System;
using Caliburn.Micro;

namespace MahApps.Integration.Caliburn.Micro
{
    /// <summary>
    /// Base class for coroutines.
    /// </summary>
    /// <seealso cref="Caliburn.Micro.IResult" />
    public abstract class ResultBase : IResult
    {
        /// <inheritdoc />
        public event EventHandler<ResultCompletionEventArgs> Completed;

        /// <inheritdoc />
        public abstract void Execute(CoroutineExecutionContext context);

        /// <summary>
        /// Raises the <see cref="E:Completed" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ResultCompletionEventArgs"/> instance containing the event data.</param>
        protected void OnCompleted(ResultCompletionEventArgs e = null)
        {
            Completed?.Invoke(this, e ?? new ResultCompletionEventArgs());
        }
    }
}
