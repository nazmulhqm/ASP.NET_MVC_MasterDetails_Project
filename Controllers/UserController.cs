//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;

//using MVCProject_Nazmul.Models.ViewModels;
//using MVCProject_Nazmul.Permissions;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using MVCProject_Nazmul.Models;
//using System.Security.Claims;

//namespace MVCProject_Nazmul.Controllers
//{
//    public class UserController : Controller
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly RoleManager<IdentityRole> _roleManager;

//        public UserController()
//        {
//            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
//            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
//        }
        
//        public ActionResult Index()
//        {
//            var users = _userManager.Users.OrderBy(u => u.UserName).ToList();
//            return View(users);
//        }

//        [HttpGet]
//       //[Authorize(Roles = "Admin")]
//        public ActionResult ManageRoles(string userId)
//        {
//            var user = _userManager.FindById(userId);
//            if (user == null)
//                return RedirectToAction("Index");

//            var userRoles = _userManager.GetRoles(user.Id);
//            var allRoles = _roleManager.Roles.OrderBy(x => x.Name).ToList();
//            var allRolesViewModel = allRoles.Select(role => new ManageRoleViewModel
//            {
//                Name = role.Name,
//                Checked = userRoles.Contains(role.Name)
//            }).ToList();

//            var manageUserRolesViewModel = new ManageUserRolesViewModel
//            {
//                UserId = userId,
//                UserName = user.UserName,
//                ManageRolesViewModel = allRolesViewModel
//            };

//            return View(manageUserRolesViewModel);
//        }

//        [HttpPost]
//        [Authorize(Roles = "Admin")]
//        [ValidateAntiForgeryToken]
//        public ActionResult ManageRoles(ManageUserRolesViewModel manageUserRolesViewModel)
//        {
//            if (!ModelState.IsValid)
//                return View(manageUserRolesViewModel);

//            var user = _userManager.FindById(manageUserRolesViewModel.UserId);
//            if (user == null)
//                return View(manageUserRolesViewModel);

//            var existingRoles = _userManager.GetRoles(user.Id);
//            foreach (var roleViewModel in manageUserRolesViewModel.ManageRolesViewModel)
//            {
//                var roleExists = existingRoles.FirstOrDefault(x => x == roleViewModel.Name);
//                switch (roleViewModel.Checked)
//                {
//                    case true when roleExists == null:
//                        _userManager.AddToRole(user.Id, roleViewModel.Name);
//                        break;
//                    case false when roleExists != null:
//                        _userManager.RemoveFromRole(user.Id, roleViewModel.Name);
//                        break;
//                }
//            }
//            return RedirectToAction("Index", "User", new { id = manageUserRolesViewModel.UserId, succeeded = true, message = "Succeeded" });
//        }

//        [HttpGet]
//        [Authorize(Roles = "Admin")]
//        public ActionResult ManagePermissions(string userId, string permissionValue, int? pageNumber, int? pageSize)
//        {
//            var user = _userManager.FindById(userId);
//            if (user == null)
//                return RedirectToAction("Index");

//            var userPermissions = _userManager.GetClaims(user.Id);
//            var allPermissions = PermissionHelper.GetAllPermissions();
//            if (!string.IsNullOrWhiteSpace(permissionValue))
//            {
//                allPermissions = allPermissions.Where(x => x.Value.ToLower().Contains(permissionValue.Trim().ToLower()))
//                    .ToList();
//            }

//            var managePermissionsClaim = new List<ManageUserClaimViewModel>();
//            foreach (var permission in allPermissions)
//            {
//                var managePermissionClaim = new ManageUserClaimViewModel
//                {
//                    Type = permission.Type,
//                    Value = permission.Value,
//                    Checked = userPermissions.Any(x => x.Value == permission.Value)
//                };
//                managePermissionsClaim.Add(managePermissionClaim);
//            }

//            // For pagination, you can implement your custom pagination logic here

//            var manageUserPermissionsViewModel = new ManageUserPermissionsViewModel
//            {
//                UserId = userId,
//                UserName = user.UserName,
//                PermissionValue = permissionValue,
//                ManagePermissionsViewModel = managePermissionsClaim
//            };

//            return View(manageUserPermissionsViewModel);
//        }


//        [HttpPost]
//        //[Authorize(Roles = "Admin")]
//        [ValidateAntiForgeryToken]
//        public ActionResult ManageClaims(ManageUserClaimViewModel manageUserClaimViewModel)
//        {
//            var userById = _userManager.FindById(manageUserClaimViewModel.UserId);
//            var userByName = _userManager.FindByName(manageUserClaimViewModel.UserName);

//            if (userById != userByName)
//                return Json(new { Succeeded = false, Message = "Fail" });

//            var allClaims = _userManager.GetClaims(userById.Id);
//            var claimExists =
//                allClaims.Where(x => x.Type == manageUserClaimViewModel.Type && x.Value == manageUserClaimViewModel.Value).ToList();
//            switch (manageUserClaimViewModel.Checked)
//            {
//                case true when claimExists.Count == 0:
//                    _userManager.AddClaim(userById.Id, new Claim(manageUserClaimViewModel.Type, manageUserClaimViewModel.Value));
//                    break;
//                case false when claimExists.Count > 0:
//                    _userManager.RemoveClaims(userById.Id, claimExists);
//                    break;
//            }

//            return Json(new { Succeeded = true, Message = "Success" });
//        }


//    }
//}