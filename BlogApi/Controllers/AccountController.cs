using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogApi.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace BlogApi.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private UserManager<ApplicationUser> _userManager;
        private BlogApiContext _BlogApiContext;

        public AccountController(SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger,UserManager<ApplicationUser> userManager,BlogApiContext BlogApiContext)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _BlogApiContext = BlogApiContext;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Index","Post");
        }

        public ActionResult Profile(string id)
        {
            var currentuser = _BlogApiContext.Users.SingleOrDefault(m =>m.UserName.Equals(id));
            var user = new ApplicationUser()
            {
                Email = currentuser.Email,
                UserName = currentuser.UserName,
                IsActive = currentuser.IsActive,
                PhoneNumber = currentuser.PhoneNumber,
                ProfileImage = currentuser.ProfileImage,
                FbID = currentuser.FbID,
                Gender = currentuser.Gender,
                Id = currentuser.Id,
                FullName = currentuser.FullName,
                AccessFailedCount = currentuser.AccessFailedCount,
                ConcurrencyStamp = currentuser.ConcurrencyStamp,
                EmailConfirmed = currentuser.EmailConfirmed,
                GithubID = currentuser.GithubID,
                InstagramID = currentuser.InstagramID,
                LinkedinID = currentuser.LinkedinID,
                LockoutEnabled = currentuser.LockoutEnabled,
                LockoutEnd = currentuser.LockoutEnd,
                NormalizedEmail = currentuser.NormalizedEmail,
                NormalizedUserName = currentuser.NormalizedUserName,
                PasswordHash = currentuser.PasswordHash,
                PhoneNumberConfirmed = currentuser.PhoneNumberConfirmed,
                SecurityStamp = currentuser.SecurityStamp,
                TwitterID = currentuser.TwitterID,
                TwoFactorEnabled = currentuser.TwoFactorEnabled,
                YoutubeID = currentuser.YoutubeID
            };
            return View(user);
        }
        public ActionResult EditProfile()
        {
            var currentuser = _BlogApiContext.Users.SingleOrDefault(m => m.UserName.Equals(User.Identity.Name));
            var user = new ApplicationUser()
            {
                Id = currentuser.Id,
                Email = currentuser.Email,
                UserName = currentuser.UserName,
                IsActive = currentuser.IsActive,
                PhoneNumber = currentuser.PhoneNumber,
                ProfileImage = currentuser.ProfileImage,
                FbID = currentuser.FbID,
                Gender = currentuser.Gender,
                AccessFailedCount=currentuser.AccessFailedCount,
                ConcurrencyStamp = currentuser.ConcurrencyStamp,
                EmailConfirmed = currentuser.EmailConfirmed,
                FullName = currentuser.FullName,
                GithubID = currentuser.GithubID,
                InstagramID = currentuser.InstagramID,
                LinkedinID = currentuser.LinkedinID,
                LockoutEnabled = currentuser.LockoutEnabled,
                LockoutEnd = currentuser.LockoutEnd,
                NormalizedEmail = currentuser.NormalizedEmail,
                NormalizedUserName = currentuser.NormalizedUserName,
                PasswordHash = currentuser.PasswordHash,
                PhoneNumberConfirmed = currentuser.PhoneNumberConfirmed,
                SecurityStamp = currentuser.SecurityStamp,
                TwitterID = currentuser.TwitterID,
                TwoFactorEnabled = currentuser.TwoFactorEnabled,
                YoutubeID = currentuser.YoutubeID
            };
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationUser applicationUser, IFormFile profilepic)
        {
            
               var path = Path.Combine(
               Directory.GetCurrentDirectory(), "wwwroot/ProfileImagefiles/",
               profilepic.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    profilepic.CopyTo(stream);
                }
                applicationUser.ProfileImage = profilepic.FileName;
                _BlogApiContext.Users.Update(applicationUser);

                
                _BlogApiContext.SavechangesAsyncChanges();

            
            return RedirectToAction(nameof(Profile), new { id = User.Identity.Name });
        }
        public ActionResult BlockedUser()
        {
            var blockeduser = _BlogApiContext.Users.Where(m => m.IsActive == 0).AsEnumerable();
            var user = new List<ApplicationUser>();

            foreach (var users in blockeduser)
            {
                user.Add(new ApplicationUser()
                {
                    Id =users.Id,
                    UserName = users.UserName,
                    AccessFailedCount= users.AccessFailedCount,
                    ConcurrencyStamp= users.ConcurrencyStamp,
                    Email = users.Email,
                    EmailConfirmed = users.EmailConfirmed,
                    FbID = users.FbID,
                    FullName = users.FullName,
                    Gender = users.Gender,
                    GithubID = users.GithubID,
                    InstagramID = users.InstagramID,
                    IsActive = users.IsActive,
                    LinkedinID = users.LinkedinID,
                    LockoutEnabled = users.LockoutEnabled,
                    LockoutEnd = users.LockoutEnd,
                    NormalizedEmail = users.NormalizedEmail,
                    NormalizedUserName = users.NormalizedUserName,
                    PasswordHash = users.PasswordHash,
                    PhoneNumber = users.PhoneNumber,
                    PhoneNumberConfirmed = users.PhoneNumberConfirmed,
                    ProfileImage = users.ProfileImage,
                    SecurityStamp = users.SecurityStamp,
                    TwitterID = users.TwitterID,
                    TwoFactorEnabled = users.TwoFactorEnabled,
                    YoutubeID = users.YoutubeID
                });
            }
            return View(user);
        }

        public ActionResult UnblockUser(string id)
        {
            var users = _BlogApiContext.Users.SingleOrDefault(m => m.Id.Equals(id));
            users.IsActive = 1;
            _BlogApiContext.Users.Update(users);
            _BlogApiContext.SavechangesAsyncChanges();

            return RedirectToAction(nameof(BlockedUser));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult BlockUser(string id)
        {
            var users = _BlogApiContext.Users.SingleOrDefault(m => m.Id.Equals(id));
            users.IsActive = 0;
            _BlogApiContext.Users.Update(users);
            _BlogApiContext.SavechangesAsyncChanges();

            return RedirectToAction(nameof(BlockedUser));
        }
    }
}
