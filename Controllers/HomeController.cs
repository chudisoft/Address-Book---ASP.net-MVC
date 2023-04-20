using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Address_Book.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Calculate(DateTime dob)
        {
            var d = DateTime.Now - dob;
            var dy = DateTime.Now.Year - dob.Year;
            var dm = DateTime.Now.Month - dob.Month;
            if(dm < 0) { dm *= -1; }

            //var h = d.Days - (dy * dm);
            var _mm = (dy * 12) + dm;

            var dd = d.TotalDays.ToString("N2");
            var hh = d.TotalHours.ToString("N2");
            var mm = d.TotalMinutes.ToString("N2");
            var ss = d.TotalSeconds.ToString("N2");

            return Json(new { 
                status = "success", result = 
                //$"Years: {dy}, Months: {dm}, Days: {h}, Hours: {d.Hours}, Minutes: {d.Minutes}, Seconds: {d.Seconds}"
                $"Years: {dy}\n Months: {_mm}\n Days: {dd}\n Hours: {hh}\n Minutes: {mm}\n Seconds: {ss}"
            });
        }

    }
}