using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Client.Web.Models.Filter;
using TimeSheet.Client.Web.Models.Layout;
using TimeSheet.Models;

namespace TimeSheet.Client.Web.Models.Client
{
    public class ClientsViewModel : LayoutViewModel
    {
        public List<ClientViewModel> Clients { get; set; }

        public ClientViewModel NewClient { get; set; }

        public FilterViewModel FilterViewModel { get; set; }

        public bool ModelHasErrors { get; set; }

    }
}