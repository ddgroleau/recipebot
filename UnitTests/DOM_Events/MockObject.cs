using PBC.Shared.Custom_Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.DOM_Events
{
    public class MockObject
    {
        [AcceptableURL]
        public string URL { get; set; }
        [ListMustContainElements]
        public List<string> ListString { get; set; } = new List<string>();
    }
}
