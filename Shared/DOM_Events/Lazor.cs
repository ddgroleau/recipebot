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

        public void SetLoadingStatus(bool isLoading)
        {
            Loading = isLoading;
        }
        
        public string ErrorMessage { get; set; }
        public void SetErrorMessage(string message)
        {
            ErrorMessage = message;
        }

        public bool IsSuccess { get; set; }
        public void SetSuccessStatus(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public bool IsToggled { get; set; } = true;
        public string ToggleTarget { get => IsToggled ? "l-hide" : null; }
        public void Toggle()
        {
            IsToggled = !IsToggled;
        }

        public bool IsHidden { get; set; } = false;
        public string HideTarget { get => IsHidden ? "l-hide" : "l-show"; }

        public void Hide()
        {
            IsHidden = true;
            IsShown = false;
        }
        public bool IsShown { get; set; } = false;
        public string ShowTarget { get => IsShown ? "l-show" : "l-hide"; }
        public void Show()
        {
            IsShown = true;
            IsHidden = false;
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

                isValid = Validator.TryValidateObject(obj, validationContext, new List<ValidationResult>(), true);
            }
            catch (Exception)
            {
                isValid = false;
            }
            return isValid;
        }
    }
}
