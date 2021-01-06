using Microsoft.AspNet.Identity.Owin;
using SinanDolaymanAdmin.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SinanDolaymanAdmin.Controllers
{
    public class DolaymanUserController : Controller
    {
        public DolaymanUserController()
        {
        }

        public DolaymanUserController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        //
        // GET: /Users/
        [HttpGet]
        public async Task<ActionResult> Index()
        {

            return View(await UserManager.Users.ToListAsync());
        }

        public ActionResult Details(string id)
        {
            var user = UserManager.Users.Where(a => a.Id == id).FirstOrDefault();
            var userRoles = RoleManager.Roles.Where(a => a.Users.Any(x => x.UserId == id));
            ViewBag.UserRoles = userRoles.ToList();
            ViewBag.RolSayisi = userRoles.ToList().Count();
            return View(user);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            var butunRoller = await RoleManager.Roles.ToListAsync();
            List<SelectListItem> butunRollerS = new List<SelectListItem>();
            foreach (var item in butunRoller)
            {
                SelectListItem rol = new SelectListItem();
                rol.Value = item.Name;
                rol.Text = item.Name;
                butunRollerS.Add(rol);
            }

            ViewBag.ButunRoller = butunRollerS;

            var userRoles = await UserManager.GetRolesAsync(user.Id);

            List<SelectListItem> roller = new List<SelectListItem>();
            foreach (var item in userRoles)
            {
                SelectListItem rol = new SelectListItem();
                rol.Value = item;
                rol.Text = item;
                roller.Add(rol);
            }

            return View(new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,

                Rolleri = roller
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Email")] EditUserViewModel editUser, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }


                var userRoles = await UserManager.GetRolesAsync(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return Redirect("~/DolaymanUser/index");
            }
            else
            {
                ModelState.AddModelError("", "Bir hata oluştu");
                var user = await UserManager.FindByIdAsync(editUser.Id);

                var butunRoller = await RoleManager.Roles.ToListAsync();
                List<SelectListItem> butunRollerS = new List<SelectListItem>();
                foreach (var item in butunRoller)
                {
                    SelectListItem rol = new SelectListItem();
                    rol.Value = item.Id;
                    rol.Text = item.Name;
                    butunRollerS.Add(rol);
                }

                ViewBag.ButunRoller = butunRollerS;

                var userRoles = await UserManager.GetRolesAsync(user.Id);

                List<SelectListItem> roller = new List<SelectListItem>();
                foreach (var item in userRoles)
                {
                    SelectListItem rol = new SelectListItem();
                    rol.Value = item;
                    rol.Text = item;
                    roller.Add(rol);
                }

                return View(new EditUserViewModel()
                {
                    Id = user.Id,
                    Email = user.Email,

                    Rolleri = roller
                });
            }

        }

        //
        // GET: /Users/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var result = await UserManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return Redirect("~/usersadmin/index");
            }
            return View();
        }

    }
}
