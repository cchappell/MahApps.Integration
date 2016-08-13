using System;
using System.Collections.Generic;
using Caliburn.Micro;
using MahApps.Integration.Caliburn.Micro.Dialogs;
using MahApps.Metro.Controls.Dialogs;

namespace Samples.CaliburnMicroIntegration.Demos
{
    public class CustomDialogViewModel : Screen
    {
        private string info;
        private bool hasChanges;

        public CustomDialogViewModel()
        {
            DisplayName = "Custom Dialog View Model";
        }

        public string Info
        {
            get { return info; }
            set
            {
                if (this.Set(ref info, value))
                {
                    HasChanges = true;
                }
            }
        }

        public bool HasChanges
        {
            get { return hasChanges; }
            set
            {
                if (this.Set(ref hasChanges, value))
                {
                    NotifyOfPropertyChange(nameof(CanAcceptChanges));
                }
            }
        }

        public bool CanAcceptChanges => HasChanges;

        protected override void OnActivate()
        {
            base.OnActivate();

            Console.WriteLine($"{GetType().Name} activated.");
        }

        public IResult AcceptChanges() => new CloseDialog();

        public IEnumerable<IResult> CancelChanges()
        {
            if (!HasChanges)
            {
                yield return new CloseDialog();
                yield break;
            }

            var message = new MetroMessage("Confirm Cancel", "Are you sure that you want to discard changes?")
            {
                Buttons = MessageDialogStyle.AffirmativeAndNegative
            };

            yield return message;

            if (message.Result == MessageDialogResult.Affirmative)
            {
                yield return new CloseDialog();

                Info = string.Empty;
                HasChanges = false;
            }
        }
    }
}