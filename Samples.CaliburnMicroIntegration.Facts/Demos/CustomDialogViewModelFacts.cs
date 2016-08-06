using System.Linq;
using MahApps.Integration.Caliburn.Micro.Dialogs;
using MahApps.Metro.Controls.Dialogs;
using Ploeh.AutoFixture;
using Samples.CaliburnMicroIntegration.Demos;
using Xunit;

namespace Samples.CaliburnMicroIntegration.Facts.Demos
{
    public class CustomDialogViewModelFacts
    {
        private readonly Fixture fixture = new Fixture();

        [Fact]
        public void HasChanges_and_CanAcceptChanges_defaults_to_false()
        {
            var sut = new CustomDialogViewModel();

            Assert.False(sut.HasChanges);
            Assert.False(sut.CanAcceptChanges);
        }

        [Fact]
        public void Info_sets_HasChanges_and_CanAcceptChanges_to_true()
        {
            var sut = new CustomDialogViewModel();

            sut.Info = fixture.Create<string>();

            Assert.True(sut.HasChanges);
            Assert.True(sut.CanAcceptChanges);
        }

        [Fact]
        public void AcceptChanges_returns_correct_result()
        {
            var sut = new CustomDialogViewModel();

            var result = sut.AcceptChanges();

            Assert.IsType<CloseDialog>(result);
        }

        [Fact]
        public void CancelChanges_returns_correct_result_when_not_changed()
        {
            var sut = new CustomDialogViewModel();

            var result = sut.CancelChanges().ToList();

            Assert.IsType<CloseDialog>(result.Single());
        }

        [Fact]
        public void CancelChanges_shows_confirms_cancel_when_changed()
        {
            var sut = new CustomDialogViewModel();
            sut.Info = fixture.Create<string>();

            var result = sut.CancelChanges();

            Assert.IsType<MetroMessage>(result.Single());
        }

        [Fact]
        public void CancelChanges_discards_changes_when_confirmed()
        {
            var sut = new CustomDialogViewModel();
            sut.Info = fixture.Create<string>();

            var result = sut.CancelChanges().GetEnumerator();
            result.MoveNext();

            var message = (MetroMessage)result.Current;
            message.Result = MessageDialogResult.Affirmative;
            result.MoveNext();

            Assert.IsType<CloseDialog>(result.Current);
            Assert.False(result.MoveNext());
            Assert.Equal(string.Empty, sut.Info);
            Assert.False(sut.HasChanges);
            Assert.False(sut.CanAcceptChanges);
        }
    }
}