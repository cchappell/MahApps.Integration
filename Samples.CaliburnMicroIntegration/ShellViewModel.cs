using System.Collections.Generic;
using Caliburn.Micro;
using Samples.CaliburnMicroIntegration.Demos;

namespace Samples.CaliburnMicroIntegration
{
    public class ShellViewModel : PropertyChangedBase
    {
        public ShellViewModel(IEnumerable<IDemo> demos)
        {
            Demos = demos;
        }

        public IEnumerable<IDemo> Demos { get; }
    }
}