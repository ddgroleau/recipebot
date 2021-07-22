using PBC.Shared.Lazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.DOM_Events
{
    public class LazorTests
    {
        [Fact]
        public void Toggle_WithIsToggledDefault_ShouldMakeIsToggledFalse()
        {
            var lazor = new Lazor();

            lazor.Toggle();

            Assert.False(lazor.isToggled);
        }
        [Fact]
        public void Toggle_WithIsToggledAsFalse_ShouldMakeIsToggledTrue()
        {
            var lazor = new Lazor();
            lazor.isToggled = false;
            
            lazor.Toggle();

            Assert.True(lazor.isToggled);
        }
        [Fact]
        public void Toggle_WithToggleMethodRunTwice_ShouldMakeIsToggledTrue()
        {
            var lazor = new Lazor();
          
            lazor.Toggle();
            lazor.Toggle();

            Assert.True(lazor.isToggled);
        }

        [Fact]
        public void Hide_WithIsHiddenDefault_ShouldMakeIsHiddenTrue()
        {
            var lazor = new Lazor();

            lazor.Hide();

            Assert.True(lazor.isHidden);
        }

        [Fact]
        public void Show_WithIsShownDefault_ShouldMakeIsShownTrue()
        {
            var lazor = new Lazor();

            lazor.Show();

            Assert.True(lazor.isShown);
        }

    }
}
