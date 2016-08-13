using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Caliburn.Micro;
using MahApps.Integration.Caliburn.Micro;

namespace Samples.CaliburnMicroIntegration.Demos
{
    public class TransitionViewModel : Screen, IDemo
    {
        public TransitionViewModel()
        {
            DisplayName = "transitions";
        }

        public DateTime Now => DateTime.Now;

        public IEnumerable<IResult> NextDataTemplate()
        {
            while (true)
            {
                yield return new Transition("DateTimeTemplate1");
                yield return Wait;
                yield return new Transition("DateTimeTemplate2");
                yield return Wait;
                yield return new Transition("DateTimeTemplate3");
                yield return Wait;
            }
        }

        public IEnumerable<IResult> NextUserControl()
        {
            while (true)
            {
                yield return new Transition(new Transition1ViewModel());
                yield return Wait;
                yield return new Transition(new Transition2ViewModel());
                yield return Wait;
            }
        }

        private IResult Wait => Task.Delay(TimeSpan.FromSeconds(1)).AsResult();
    }

    public class Transition1ViewModel : Screen
    {
        public string Header => "Transition 1";
    }

    public class Transition2ViewModel : Screen
    {
        public string Header => "Transition 2";
    }
}