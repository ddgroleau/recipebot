using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.DOM_Events
{
    public interface ILazor
    {
        public bool Loading { get; }
        public void SetLoadingStatus(bool isLoading);

        public string ErrorMessage { get; }
        public void SetErrorMessage(string message);

        public bool IsSuccess { get; }
        public void SetSuccessStatus(bool isSuccess);

        public bool IsToggled { get; }
        public string ToggleTarget { get; }
        public void Toggle();

        public bool IsHidden { get; }
        public string HideTarget { get; }
        public void Hide();

        public bool IsShown { get; }
        public string ShowTarget { get; }
        public void Show();

        public bool IsPropertyValid(Object obj, string propertyName, string property);
        public bool IsPropertyValid(Object obj, string propertyName, List<string> property);
        public bool IsObjectValid(Object obj);
    }
}
