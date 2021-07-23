using PBC.Shared.DOM_Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.Lazor
{
    public class Lazor : ILazor
    {
        public bool Loading { get; set; } = false;
        public string ErrorMessage { get; set; }
        public bool isSuccess { get; set; }
        public bool isToggled { get; set; } = true;
        public string ToggleTarget { get => isToggled ? "l-hide" : null; set { } }
        public void Toggle()
        {
            isToggled = !isToggled;
        }

        public bool isHidden { get; set; } = false;
        public string HideTarget { get => isHidden ? "l-hide" : null; set { } }

        public void Hide()
        {
                isHidden = true;
        }

        public bool isShown { get; set; } = false;
        public string ShowTarget { get => isHidden ? "l-show" : null; set { } }
        public void Show()
        {
            isShown = true;
        }

        public bool isStringPropertyValid(Object obj, string property, string propertyName)
        {
            bool isValid;
            try
            {
                var validationContext = new ValidationContext(obj)
                {
                    MemberName = property
                };
                
                isValid = Validator.TryValidateProperty(propertyName, validationContext, new List<ValidationResult>());
            }
            catch (Exception)
            {
                throw;
            }
            return isValid;
        }
    }
}
