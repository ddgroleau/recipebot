﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.DOM_Events
{
    public interface ILazor
    {
        public bool Loading { get; set; }
        public string ErrorMessage { get; set; }
        public bool isSuccess { get; set; }
        public bool isToggled { get; set; }
        public string ToggleTarget { get; set; }
        public void Toggle();

        public bool isHidden { get; set; }
        public string HideTarget { get; set; }
        public void Hide();

        public bool isShown { get; set; }
        public string ShowTarget { get; set; }
        public void Show();
    }
}