using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Client.Web.Models.Filter
{
    public class FilterLetterViewModel
    {
        public char Letter { get; set; }

        public State State { get; set; }

    }

    public enum State
    {
        ACTIVE = 1,
        INACTIVE = 2,
        DISABLED = 3
    }
}