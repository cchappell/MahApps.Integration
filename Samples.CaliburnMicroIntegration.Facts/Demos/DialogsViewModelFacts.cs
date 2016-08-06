using System.Linq;
using Caliburn.Micro;
using MahApps.Integration.Caliburn.Micro.Dialogs;
using MahApps.Metro.Controls.Dialogs;
using Moq;
using Ploeh.AutoFixture;
using Samples.CaliburnMicroIntegration.Demos;
using Xunit;

namespace Samples.CaliburnMicroIntegration.Facts.Demos
{
    public class DialogsViewModelFacts
    {
        [Fact]
        public void ShowMessage_returns_correct_sequence()
        {
            var sut = new DialogsViewModel();

            var enumerator = sut.ShowMessage().GetEnumerator();

            Assert.True(enumerator.MoveNext());
            Assert.IsAssignableFrom<MetroMessage>(enumerator.Current);
            Assert.False(enumerator.MoveNext());
        }

        [Fact]
        public void GetInput_returns_correct_sequence()
        {
            var sut = new DialogsViewModel();

            var result = sut.GetInput();

            Assert.IsType<MetroInput>(result.Single());
        }

        [Fact]
        public void ShowLogin_returns_correct_sequence()
        {
            var fixture = new Fixture();
            var sut = new DialogsViewModel();

            var enumerator = sut.ShowLogin().GetEnumerator();

            Assert.True(enumerator.MoveNext());
            Assert.IsAssignableFrom<MetroLogin>(enumerator.Current);

            ((MetroLogin)enumerator.Current).Result = fixture.Create<LoginDialogData>();

            Assert.False(enumerator.MoveNext());
        }

        [Fact]
        public void ShowProgress_returns_correct_sequence()
        {
            var progress = 0d;
            var controller = new Mock<IProgressDialogController>();

            controller
                .Setup(c => c.SetCancelable(true))
                .Verifiable();

            controller
                .SetupGet(c => c.IsCanceled)
                .Returns(false)
                .Verifiable();

            controller
                .Setup(c => c.SetProgress(It.IsAny<double>()))
                .Callback((double p) => progress = p)
                .Verifiable();

            controller
                .Setup(c => c.SetMessage(It.IsAny<string>()))
                .Verifiable();

            var sut = new DialogsViewModel();

            var enumerator = sut.ShowProgress().GetEnumerator();

            Assert.True(enumerator.MoveNext());
            Assert.IsAssignableFrom<MetroLoader>(enumerator.Current);

            ((MetroLoader)enumerator.Current).Controller = controller.Object;

            do
            {
                enumerator.MoveNext();

                Assert.IsAssignableFrom<TaskResult>(enumerator.Current);
            }
            while (progress < (1d - 0.2d));

            Assert.True(enumerator.MoveNext());
            Assert.IsAssignableFrom<MetroLoaderComplete>(enumerator.Current);

            controller.Verify();
        }

        [Fact]
        public void ShowProgress_return_correct_sequence_when_canceled()
        {
            var progress = 0d;
            var controller = new Mock<IProgressDialogController>();

            controller
                .SetupGet(c => c.IsCanceled)
                .Returns(() => progress >= 0.4d)
                .Verifiable();

            controller
                .Setup(c => c.SetProgress(It.IsAny<double>()))
                .Callback((double p) => progress = p);

            var sut = new DialogsViewModel();

            var enumerator = sut.ShowProgress().GetEnumerator();

            enumerator.MoveNext();
            ((MetroLoader)enumerator.Current).Controller = controller.Object;

            do
            {
                enumerator.MoveNext();

                Assert.IsAssignableFrom<TaskResult>(enumerator.Current);
            }
            while (progress < 0.4d);

            Assert.True(enumerator.MoveNext());
            Assert.IsAssignableFrom<MetroLoaderComplete>(enumerator.Current);

            Assert.True(enumerator.MoveNext());
            Assert.IsAssignableFrom<MetroMessage>(enumerator.Current);
        }

        [Fact]
        public void ShowCustom_returns_correct_result()
        {
            var sut = new DialogsViewModel();

            var result = sut.ShowCustom();

            Assert.IsType<Dialog>(result.Single());
        }
    }
}