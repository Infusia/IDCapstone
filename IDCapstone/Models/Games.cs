using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDCapstone.Models
{
    public class Games
    {

        public static List<SelectListItem> Game = new List<SelectListItem>()
        {
           new SelectListItem() { Text="All", Value="All"},
           new SelectListItem () {Text = "Street Fighter 5", Value = "Street Fighter 5"},
           new SelectListItem () {Text = "Guilty Gear Revelator XRD", Value = "Guilty Gear Revelator XRD"},
           new SelectListItem () {Text = "Mortal Kombat", Value = "Mortal Kombat"},
           new SelectListItem () {Text = "Tekken 7", Value = "Tekken 7"},
           new SelectListItem () {Text = "Guilty Gear Strive", Value = "Guilty Gear Strive"}

        };
    }
}
