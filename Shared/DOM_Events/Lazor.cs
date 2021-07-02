using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.Lazor
{
    public class Lazor
    {

        // Toggle
        public bool isToggled = true;
        public string ToggleTarget => isToggled ? "l-hide" : null;
        public void Toggle()
        {
            isToggled = !isToggled;
        }

        // Hide
        public bool isHidden = false;
        public string HideTarget => isHidden ? "l-hide" : null;

        public void Hide()
        {
                isHidden = true;
        }
        // Show
        public bool isShown = false;
        public string ShowTarget => isHidden ? "l-show" : null;
        public void Show()
        {
            isShown = true;
        }
    }
}
