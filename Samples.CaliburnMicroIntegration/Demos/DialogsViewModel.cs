using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Caliburn.Micro;
using MahApps.Integration.Caliburn.Micro.Dialogs;

namespace Samples.CaliburnMicroIntegration.Demos
{
    public class DialogsViewModel : Screen, IDemo
    {
        private string info;

        public DialogsViewModel()
        {
            DisplayName = "dialogs";
        }

        public string Info
        {
            get { return info; }
            set
            {
                if (info != value)
                {
                    info = value + Environment.NewLine;
                    NotifyOfPropertyChange();
                }
            }
        }

        private IResult Wait => Task.Delay(1000).AsResult();

        public IEnumerable<IResult> ShowMessage()
        {
            var message = new MetroMessage("MetroMessage", "Hello from DialogsViewModel.");
            yield return message;

            Info += $"{message.Result} clicked.";
        }

        public IEnumerable<IResult> GetInput()
        {
            var input = new MetroInput("MetroInput", "Tell me something I don't know.");
            yield return input;

            Info += $"You told me {input.Result ?? "nothing"}.";
        }

        public IEnumerable<IResult> ShowLogin()
        {
            var login = new MetroLogin("MetroLogin", "Enter your username and password.");
            yield return login;

            var result = login.Result;
            var username = result.Username;
            var password = result.Password;

            Info += $"You entered username {username ?? "none"} and password {password ?? "none"}.  Don't do this for real!";
        }

        public IEnumerable<IResult> ShowProgress()
        {
            var loader = new MetroLoader("MetroLoader", "Processing...");
            yield return loader;

            var controller = loader.Controller;
            controller.SetCancelable(true);

            var progress = 0d;

            do
            {
                Info += "Processing...";
                yield return Wait;

                if (controller.IsCanceled)
                {
                    Info += "Canceled";
                    break;
                }

                progress += .2d;
                controller.SetProgress(progress);
                controller.SetMessage($"{progress:p0} complete.");
            }
            while (progress < 1d);

            yield return loader.Complete();

            if (controller.IsCanceled)
            {
                yield return new MetroMessage("MetroMessage", "ShowProgress canceled.");
            }

            Info += "ShowProgress complete.";
        }

        public IEnumerable<IResult> ShowCustom()
        {
            yield return new Dialog("CustomDialog");

            Info += "CustomDialog closed";
        }
    }
}