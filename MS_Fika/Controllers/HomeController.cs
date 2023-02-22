using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MS_Fika.Data;
using MS_Fika.Models;
using System.Diagnostics;

namespace MS_Fika.Controllers
{
    public class HomeController : Controller
    {


        private readonly ApplicationDbContext db;
        /*
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _logger = logger;

            db = context;

            _userManager = userManager;

            _roleManager = roleManager;
        }

        */

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            db = context;
        }


        public IActionResult Index()
        {
            var prf = db.Profiles.Where(p => p.ProfilePublic == true).ToList();
            List<Post> postari = new List<Post>();
            foreach (var profile in prf)
            {
                var id = profile.UserId;
                var post = db.Posts.Where(p => p.GroupId == null && p.UserId == id).ToList();
                foreach (var p in post)
                { postari.Add(p); }

            }
            ViewBag.Posts = postari;
            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        /*
        public IActionResult New(Profile profile)
        {
            profile.UserId = _userManager.GetUserId(User);


            if (ModelState.IsValid)
            {
                db.Profiles.Add(profile);
                db.SaveChanges();
                TempData["message"] = "Profilul a fost adaugat";
                return RedirectToAction("Profiles/Index/");
            }
            else
            {
                return View(profile);
            }
            //return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
        */
    }
}