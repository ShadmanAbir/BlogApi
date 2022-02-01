using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BlogApi.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [DefaultValue(null)]
        public string ProfileImage { get; set; }
        public int IsActive { get; set; }
        [DefaultValue(null)]
        public string FbID { get; set; }
        [DefaultValue(null)]
        public string Gender { get; set; }
        [DefaultValue(null)]
        public string TwitterID { get; set; }
        [DefaultValue(null)]
        public string LinkedinID { get; set; }
        [DefaultValue(null)]
        public string InstagramID { get; set; }
        [DefaultValue(null)]
        public string YoutubeID { get; set; }
        [DefaultValue(null)]
        public string GithubID { get; set; }
        [DefaultValue(null)]
        public string FullName { get; set; }


    }
}
