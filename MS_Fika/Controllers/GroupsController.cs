using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MS_Fika.Data;
using MS_Fika.Models;
using System.Collections.ObjectModel;
using System.Data;

namespace MS_Fika.Controllers
{
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public GroupsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            db = context;

            _userManager = userManager;

            _roleManager = roleManager;
        }
        [Authorize(Roles = "User,Admin")]
        public IActionResult Index()
        {
            var groups = db.Groups;
            // ViewBag.OriceDenumireSugestiva
            ViewBag.Groups = groups;

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }

            return View();
        }
        [Authorize]
        public ActionResult New()
        {
            Group group = new Group();
            group.GroupAdminId = _userManager.GetUserId(User);
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult New(Group group)
        {
            group.GroupDate = DateTime.Now;
            group.GroupAdminId = _userManager.GetUserId(User);
            //ApplicationUser user = db.Users.Find(_userManager.GetUserId(User));
            try
            {
                if (ModelState.IsValid)
                {
                    UserInGroup useringrup = new UserInGroup();
                    useringrup.GroupId = group.GroupId;
                    useringrup.UserId = _userManager.GetUserId(User);
                    useringrup.User = ViewBag.UserCurent;
                    useringrup.Group = group;
                    useringrup.JoinDate = DateTime.Now;
                    db.UserInGroups.Add(useringrup);
                    //user.UserInGroups.Add(useringrup);
                    //group.UserInGroups.Add(useringrup);
                    //group.GroupUsers = new Collection<ApplicationUser>();
                    //user.Groups = new Collection<Group>();
                    //group.GroupUsers.Add(user);
                    //user.Groups.Add(group);
                    db.Groups.Add(group);
                    db.SaveChanges();
                    TempData["message"] = "Grupul a fost creat cu succes!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(group);
                }
            }
            catch (Exception)
            {
                return View(group);
            }
        }
        /*
        [Authorize(Roles = "User,Admin")]
        public IActionResult New()
        {
            Group group = new Group();

            return View(group);
        }
        // Se adauga articolul in baza de date
        // Doar utilizatorii cu rolul de Editor sau Admin pot adauga articole in platforma
        
        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public IActionResult New(Group group)
        {
            group.GroupDate = DateTime.Now;
            group.GroupAdminId = _userManager.GetUserId(User);
            //var user = db.Users.Find(_userManager.GetUserId(User));

            if (ModelState.IsValid)
            {
                //group.GroupUsers = new Collection<ApplicationUser>();
                //group.GroupUsers.Add(user);
                db.Groups.Add(group);
                db.SaveChanges();
                TempData["message"] = "Grupul a fost adaugat";
                return RedirectToAction("Index");
            }
            else
            {
                return View(group);
            }
        }*/
        // Se sterge un grup din baza de date 
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public ActionResult Delete(int id)
        {
            Group group = db.Groups
                                         .Include("Posts")
                                         .Include("UserInGroups")
                                         .Where(p => p.GroupId == id)
                                         .First();

            if (group.GroupAdminId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                //var users = group.GroupUsers;
                //foreach (var user in users)
                {
                    // user.Groups.Remove(group);
                }
                var posts = db.Posts.Include("Comments").Where(p => p.GroupId == id).ToList();
                foreach (var po in posts)
                { db.Posts.Remove(po); }
                db.Groups.Remove(group);
                db.SaveChanges();
                TempData["message"] = "Grupul a fost sters";
                return RedirectToAction("Index");
            }

            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti un grup care nu va apartine";
                return RedirectToAction("Index");
            }
        }
        [Authorize(Roles = "User,Admin")]
        public IActionResult Show(int id)
        {
            Group group = db.Groups

                                         .Include("Posts")
                                         .Include("Posts.User")
                                         .Where(p => p.GroupId == id)
                                         .First();


            UserInGroup useringrup = new UserInGroup();
            List<UserInGroup> useringrup2;
            useringrup2 = db.UserInGroups
                   .Where(p => p.GroupId == id)
                   .ToList();
            List<string> user = new List<string>();
            //var ug = group.UserInGroups;
            foreach (var u in useringrup2)
            {
                user.Add(u.UserId);
            }
            if (_userManager.GetUserId(User) == group.GroupAdminId || user.Contains(_userManager.GetUserId(User)))
            {
                ViewBag.EsteInGrup = 1;
            }
            else { ViewBag.EsteInGrup = 0; }
            SetAccessRights();
            return View(group);

        }

        // Adaugarea unei postari asociat unui grup in baza de date
        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Show([FromForm] Post post)
        {
            post.PostDate = DateTime.Now;
            post.UserId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                //post.Group=group;
                db.Posts.Add(post);
                db.SaveChanges();
                return Redirect("/Groups/Show/" + post.GroupId);
            }

            else
            {
                Group p = db.Groups

                                         .Include("Posts")
                                         .Include("Posts.User")
                                         .Where(p => p.GroupId == post.GroupId)
                                         .First();

                //return Redirect("/Articles/Show/" + comm.ArticleId);

                SetAccessRights();

                return View(p);
            }

        }
        [Authorize(Roles = "Admin,User")]
        public IActionResult Edit(int id)
        {

            Group group = db.Groups
                                        .Where(p => p.GroupId == id)
                                        .First();


            if (group.GroupAdminId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                return View(group);
            }

            else
            {
                TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unei postari care nu va apartine";
                return RedirectToAction("Index");
            }

        }
        // Se adauga articolul modificat in baza de date
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public IActionResult Edit(int id, Group requestGroup)
        {
            Group group = db.Groups.Find(id);


            if (ModelState.IsValid)
            {
                if (group.GroupAdminId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
                {
                    group.Description = requestGroup.Description;
                    group.GroupName = requestGroup.GroupName;
                    //post.PostDate = requestPost.PostDate;
                    //trb si group id ulterior
                    TempData["message"] = "Grupul a fost modificat";
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui grup care nu va apartine";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(requestGroup);
            }
        }
        /*
        [Authorize(Roles = "User,Admin")]
        public ActionResult Join(int id)
        {
            Group? group = db.Groups?.Find(id);
            ApplicationUser?  usercurent = null;
            var user=db.Users?.Find(_userManager.GetUserId(User));
            usercurent = user;
            group?.GroupUsers?.Add(usercurent);
            db.SaveChanges();
            TempData["group_message"] = "Acum sunteti membru in grup.";
            return RedirectToAction("Index");
        }*/

        [Authorize]
        public ActionResult Join(int id)
        {
            Group group = db.Groups.Find(id);
            UserInGroup useringrup = new UserInGroup();
            List<UserInGroup> useringrup2;
            useringrup2 = db.UserInGroups
                   .Where(p => p.GroupId == id)
                   .ToList();
            List<string> user = new List<string>();
            //var ug = group.UserInGroups;
            foreach (var u in useringrup2)
            {
                user.Add(u.UserId);
            }
            if (_userManager.GetUserId(User) != group.GroupAdminId && !user.Contains(_userManager.GetUserId(User)))
            {
                useringrup.GroupId = group.GroupId;
                useringrup.UserId = _userManager.GetUserId(User);
                useringrup.User = ViewBag.UserCurent;
                useringrup.Group = group;
                useringrup.JoinDate = DateTime.Now;
                db.UserInGroups.Add(useringrup);
                db.SaveChanges();
                TempData["message"] = "Acum sunteti membru in grup.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Sunteti deja membru in grup.";
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        public ActionResult Leave(int id)
        {
            Group group = db.Groups.Find(id);
            UserInGroup useringrup = new UserInGroup();
            List<UserInGroup> useringrup2;
            useringrup2 = db.UserInGroups
                   .Where(p => p.GroupId == id)
                   .ToList();
            List<string> user = new List<string>();
            //var ug = group.UserInGroups;
            foreach (var u in useringrup2)
            {
                user.Add(u.UserId);
            }
            if (_userManager.GetUserId(User) != group.GroupAdminId && user.Contains(_userManager.GetUserId(User)))
            {
                UserInGroup usg = (UserInGroup)db.UserInGroups
                    .Where(p => p.UserId == _userManager.GetUserId(User))
                    .Where(p2 => p2.GroupId == id)
                    .First();
                db.UserInGroups.Remove(usg);
                db.SaveChanges();
                TempData["message"] = "Ati parasit grupul.";
                return RedirectToAction("Index");
            }
            else { return RedirectToAction("Show", "Groups", new { id = group.GroupId }); }

        }

        public ActionResult Members(int id)
        {


            Group group = db.Groups.Find(id);
            //Group group = db.Groups.Find(id);
            //var user = db.Users.Include("Groups").Where(g=>g.GroupId==id);
            List<UserInGroup> useringrup;
            useringrup = db.UserInGroups
                   .Where(p => p.GroupId == id)
                   .ToList();
            List<string> user = new List<string>();
            var ug = group.UserInGroups;
            foreach (var u in ug)
            {
                user.Add(u.UserId);
            }
            IEnumerable<ApplicationUser> userlist = db.ApplicationUsers.Where(p => user.Contains(p.Id));
            ViewBag.Usersss = userlist;
            //ViewBag.Users = user;
            return View(group);
        }
        private void SetAccessRights()
        {
            //ViewBag.EsteInGrup = 0;
            ViewBag.AfisareButoane = false;

            ViewBag.EsteAdmin = User.IsInRole("Admin");

            ViewBag.UserCurent = _userManager.GetUserId(User);

            if (ViewBag.UserCurent == _userManager.GetUserId(User))
            {
                ViewBag.AfisareButoane = true;
            }
        }
    }
}