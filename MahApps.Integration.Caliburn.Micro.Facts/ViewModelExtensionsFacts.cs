using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace MahApps.Integration.Caliburn.Micro.Facts
{
    public class ViewModelExtensionsFacts
    {
        private readonly Fixture fixture = new Fixture();

        [Fact]
        public void SetThrowsWhenThisIsNull()
        {
            string field = null;
            Assert.Throws<ArgumentNullException>(() => ViewModelExtensions.Set(null, ref field, null));
        }

        [Theory, AutoData]
        public void SetReturnsTrueWhenFieldUpdated(string original, string newValue)
        {
            var viewModel = new TestViewModel();

            bool changed = ViewModelExtensions.Set(viewModel, ref original, newValue);

            Assert.True(changed);
        }

        [Theory, AutoData]
        public void SetReturnsCorrectValue(string value)
        {
            var viewModel = new TestViewModel();

            bool changed = ViewModelExtensions.Set(viewModel, ref value, value);

            Assert.False(changed);
        }

        [Fact]
        public void SetChangesFieldAndRaisesPropertyChanged()
        {
            var viewModel = new TestViewModel();
            var propertiesChanged = new List<string>();
            viewModel.PropertyChanged += (s, e) => { propertiesChanged.Add(e.PropertyName); };
            var expectedValue = fixture.Create<string>();

            viewModel.Property1 = expectedValue;

            Assert.Equal(expectedValue, viewModel.Property1);
            Assert.Equal(1, propertiesChanged.Count);
            Assert.Equal("Property1", propertiesChanged[0]);
        }

        [Fact]
        public void SetDoesNotRaisePropertyChangedWhenSettingCurrentValueAgain()
        {
            var expectedValue = fixture.Create<string>();
            var viewModel = new TestViewModel { Property1 = expectedValue };
            var propertiesChanged = new List<string>();
            viewModel.PropertyChanged += (s, e) => { propertiesChanged.Add(e.PropertyName); };

            viewModel.Property1 = expectedValue;

            Assert.Equal(expectedValue, viewModel.Property1);
            Assert.Equal(0, propertiesChanged.Count);
        }

        [Fact]
        public void SetDoesNotNotifyOfPropertyChangedWhenNullChangedToNull()
        {
            var viewModel = new TestViewModel();
            var propertiesChanged = new List<string>();
            viewModel.PropertyChanged += (s, e) => { propertiesChanged.Add(e.PropertyName); };

            string expectedValue = null;
            viewModel.Property1 = expectedValue;

            Assert.Equal(expectedValue, viewModel.Property1);
            Assert.Equal(0, propertiesChanged.Count);
        }

        private class TestViewModel : PropertyChangedBase
        {
            private string property1;

            public string Property1
            {
                get { return property1; }
                set { this.Set(ref property1, value); }
            }
        }
    }
}