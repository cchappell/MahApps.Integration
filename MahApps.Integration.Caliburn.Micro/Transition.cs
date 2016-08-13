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
            var control = context.Source as TransitioningContentControl;
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
            var control = context.Source as TransitioningContentControl;
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
    }
}