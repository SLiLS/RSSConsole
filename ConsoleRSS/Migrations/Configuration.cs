namespace ConsoleRSS.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ConsoleRSS.EF.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ConsoleRSS.EF.Context";
        }

        protected override void Seed(ConsoleRSS.EF.Context context)
        {
            //  This method will be called after migrating to the latest version.
            context.RSSSources.AddOrUpdate(new Models.RSSSource { RSSSourceURL = "https://habr.com/ru/rss/all/all/", SourceName = "Хабрахабр" });
            context.RSSSources.AddOrUpdate(new Models.RSSSource { RSSSourceURL = "https://www.interfax.by/news/feed", SourceName = "Интерфакс" });
           
            context.RSSSources.AddOrUpdate(new Models.RSSSource { RSSSourceURL = "https://news.tut.by/rss/", SourceName = "TUT.BY" });


            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
