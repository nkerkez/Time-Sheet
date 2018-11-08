using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Client.Web.Models.Layout;

namespace TimeSheet.Client.Web.Models.Client
{
    public class ClientViewModel : LayoutViewModel
    {

        public Guid Id { get; set; }

      //  [MinLength(1, ErrorMessage ="Name of client can't be empty")]
        [Required(ErrorMessage = "Name of client is required")]
        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public bool IsUpdated { get; set; }

        [Required(ErrorMessage = "Country of client is required")]
        public Guid CountryId { get; set; }

        public List<SelectListItem> Countries { get; set; }
    }
}