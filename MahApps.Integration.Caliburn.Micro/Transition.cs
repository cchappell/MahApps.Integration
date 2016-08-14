using System;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using MahApps.Metro.Controls;

namespace MahApps.Integration.Caliburn.Micro
{
    /// <summary>
    /// Coroutine result that transitions from one screen to another.
    /// </summary>
    public class Transition : ResultBase
    {
        private static readonly ILog Log = LogManager.GetLog(typeof(Transition));

        /// <summary>
        /// Identifies the Target attached property.
        /// </summary>
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.RegisterAttached("Target", typeof(TransitioningContentControl), typeof(Transition), new PropertyMetadata(null));

        /// <summary>
        /// Gets the target.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The target</returns>
        public static TransitioningContentControl GetTarget(DependencyObject obj) => (TransitioningContentControl)obj.GetValue(TargetProperty);

        /// <summary>
        /// Sets the target.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetTarget(DependencyObject obj, TransitioningContentControl value) => obj.SetValue(TargetProperty, value);

        private readonly Action<CoroutineExecutionContext> execute;

        /// <summary>
        /// Initializes a new instance of the <see cref="Transition"/> class.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="template"/> is <c>null</c>.
        /// </exception>
        public Transition(string template)
        {
            if (template == null) throw new ArgumentNullException(nameof(template));

            execute = context => Execute(template, context);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transition"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="model"/> is <c>null</c>
        /// </exception>
        public Transition(object model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            execute = context => Execute(model, context);
        }

        /// <inheritdoc />
        public override void Execute(CoroutineExecutionContext context)
        {
            execute(context);
            OnCompleted();
        }

        private void Execute(string template, CoroutineExecutionContext context)
        {
            var control = FindControl(context);
            if (control != null)
            {
                var dataTemplate = control.TryFindResource(template) as DataTemplate;
                if (dataTemplate != null)
                {
                    control.Content = new ContentControl { ContentTemplate = dataTemplate, Content = context.Target };
                }
            }
        }

        private void Execute(object model, CoroutineExecutionContext context)
        {
            var control = FindControl(context);
            if (control != null)
            {
                var currentView = control.Content as FrameworkElement;
                var currentModel = currentView?.DataContext;
                ScreenExtensions.TryDeactivate(currentModel, true);

                var view = ViewLocator.LocateForModel(model, null, null);
                control.Content = view;
                ViewModelBinder.Bind(model, view, context.Target);
                ScreenExtensions.TryActivate(model);
            }
        }

        private TransitioningContentControl FindControl(CoroutineExecutionContext context)
        {
            var source = context.Source;

            var control = source as TransitioningContentControl;
            if (control == null)
            {
                var obj = source as DependencyObject;
                if (obj != null)
                {
                    control = GetTarget(obj);
                }
            }

            if (control == null)
            {
                Log.Warn($"Transition: Unable to find target TransitioningContentControl. Use the Target attached property on " +
                    $"the element that triggered the Transition to provide the target. Source: {context.Source}");
            }

            return control;
        }
    }
}