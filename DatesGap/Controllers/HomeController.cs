using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DatesGap.Business;
using DatesGap.Models;

namespace DatesGap.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Creates View with default Model.
        /// </summary>
        /// <returns>Index View.</returns>
        public ActionResult Index()
        {
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";

            HomeVM homeVM = new HomeVM();

            return View(homeVM);
        }

        /// <summary>
        /// Calculates gap between 2 dates.
        /// </summary>
        /// <returns>Index View.</returns>
        /// <param name="theHomeVM">The home vm.</param>
        public ViewResult Submit(HomeVM theHomeVM){
            Date fromDate = new Date()
            {
                Month = Convert.ToInt16(theHomeVM.FromDate.Substring(0, 2)),
                Day = Convert.ToInt16(theHomeVM.FromDate.Substring(3, 2)),
                Year = Convert.ToInt16(theHomeVM.FromDate.Substring(6, 4))
            };
            Date toDate = new Date()
            {
                Month = Convert.ToInt16(theHomeVM.ToDate.Substring(0, 2)),
                Day = Convert.ToInt16(theHomeVM.ToDate.Substring(3, 2)),
                Year = Convert.ToInt16(theHomeVM.ToDate.Substring(6, 4))
            };

            theHomeVM.Gap = Dates.Gap(fromDate, toDate);
            return View("Index", theHomeVM);
        }
    }
}
