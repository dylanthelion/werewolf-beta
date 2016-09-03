namespace Werewolf_Beta.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Werewolf_Beta.Models.WerewolfContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Werewolf_Beta.Models.WerewolfContext context)
        {
            if(context.AllUsers.ToList().Count <= 0)
            {
                for (int x = 0; x < 100; x++)
                {
                    User u = new User();
                    u.ID = x;
                    u.UserName = String.Format("TestName{0}", x);
                    u.Password = String.Format("TestPassword{0}", x);
                    context.AllUsers.Add(u);
                }
                context.SaveChanges();
            }
        }
    }
}
