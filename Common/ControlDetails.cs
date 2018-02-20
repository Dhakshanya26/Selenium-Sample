using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serve.Platform.Configuration.UI.SeleniumTest.Common
{
    public class ControlDetails
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public ControlType Type { get; set; }
    }

    public enum ControlType
    {
        TextBox,
        CheckBox,
        DropDownList,
        DateTime
    }
    
    public enum NavigateType
    {
        First,
        Last,
        Previous,
        Next
    }
}
