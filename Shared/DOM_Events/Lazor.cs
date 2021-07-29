using Microsoft.Extensions.Logging;
using PBC.Shared.DOM_Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
        public string HideTarget { get => isHidden ? "l-hide" : "l-show"; set { } }

        public void Hide()
        {
            isHidden = true;
            isShown = false;
        }
        public bool isShown { get; set; } = false;
        public string ShowTarget { get => isShown ? "l-show" : "l-hide"; set { } }
        public void Show()
        {
            isShown = true;
            isHidden = false;
        }

        public bool IsPropertyValid(Object obj, string propertyName, string property)
        {
            bool isValid;
            try
            {
                var validationContext = new ValidationContext(obj)
                {
                    MemberName = propertyName
                };
                
                isValid = Validator.TryValidateProperty(property, validationContext, new List<ValidationResult>());
            }
            catch (Exception)
            {
                throw;
            }
            return isValid;
        }

        public bool IsPropertyValid(Object obj, string propertyName, List<string> property)
        {
            bool isValid;
            try
            {
                var validationContext = new ValidationContext(obj)
                {
                    MemberName = propertyName
                };

                isValid = Validator.TryValidateProperty(property, validationContext, new List<ValidationResult>());
            }
            catch (Exception)
            {
                throw;
            }
            return isValid;
        }

        public bool IsObjectValid(Object obj)
        {
            bool isValid;
            try
            {
                var validationContext = new ValidationContext(obj);

                isValid = Validator.TryValidateObject(obj, validationContext, new List<ValidationResult>());
            }
            catch (Exception)
            {
                throw;
            }
            return isValid;
        }
    }
}
