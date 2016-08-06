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
        private readonly string template;

        /// <summary>
        /// Initializes a new instance of the <see cref="Transition"/> class.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="template"/> is <c>null</c>.
        /// </exception>Components for integrating MahApps with applicatio
        public Transition(string template)
        {
            if (template == null)
            {
                throw new ArgumentNullException("template");
            }

            this.template = template;
        }

        /// <summary>
        /// Gets the template.
        /// </summary>
        public string Template
        {
            get { return template; }
        }

        /// <inheritdoc />
        public override void Execute(CoroutineExecutionContext context)
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

            OnCompleted();
        }
    }
}