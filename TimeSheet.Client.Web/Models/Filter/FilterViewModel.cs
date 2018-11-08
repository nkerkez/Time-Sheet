using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Client.Web.Models.Filter
{
    public class FilterViewModel
    {
        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string Title { get; set; }

        public List<FilterLetterViewModel> Buttons = new List<FilterLetterViewModel>();

        public FilterViewModel()
        {
            for(char letter = 'a'; letter <= 'z'; letter++)
            {
                Buttons.Add(
                    new FilterLetterViewModel
                    {
                        Letter = letter,
                        State = State.INACTIVE
                    });
            }
        }
    }
}