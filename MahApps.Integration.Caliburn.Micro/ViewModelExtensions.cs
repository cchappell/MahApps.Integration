using System;
using System.Runtime.CompilerServices;

namespace Caliburn.Micro
{
    /// <summary>
    /// Utility methods for view models.
    /// </summary>
    public static class ViewModelExtensions
    {
        /// <summary>
        /// Sets the specified field to <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="view">The view.</param>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="view"/> is <c>null</c>
        /// </exception>
        public static bool Set<T>(this INotifyPropertyChangedEx view, ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (view == null) throw new ArgumentNullException(nameof(view));

            if (((field != null) && !field.Equals(value)) || ((field == null) && (value != null)))
            {
                field = value;
                view.NotifyOfPropertyChange(propertyName);
                return true;
            }

            return false;
        }
    }
}