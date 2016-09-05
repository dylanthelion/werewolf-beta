using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Werewolf_Beta.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Index(IsUnique = true)]
        [MaxLength(20)]
        [MinLength(6)]
        [Required]
        public string UserName { get; set; }
        public string FBLoginToken { get; set; }
        public string GoogleLoginToken { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(32)]
        public string Password { get; set; }
        [ForeignKey("Nemesis")]
        public int? NemesisID { get; set; }
        public virtual User Nemesis { get; set; }
        [DefaultValue(0)]
        public int Experience { get; set; }
        [DefaultValue(1)]
        public int Level { get; set; }
        [DefaultValue(0)]
        public int Tokens { get; set; }

        public override string ToString()
        {
            return String.Format("Name: {0} Password: {1}", UserName, Password);
        }
    }

    public class WerewolfContext : DbContext
    {
        public WerewolfContext() : base("DefaultConnection")
        {

        }

        public DbSet<User> AllUsers { get; set; }
    }
}