namespace ReadRssFeeds.Domain.Migrations
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ReadRssFeeds.Domain.Concrete.EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ReadRssFeeds.Domain.Concrete.EFDbContext context)
        {
            var rsses = new List<ResurseRSS>
            {
                new ResurseRSS{ Name = "���������", Url="https://habrahabr.ru/rss" },
                new ResurseRSS{ Name = "���������", Url="http://www.interfax.by/news/feed" }
            };
            rsses.ForEach(s => context.ResurseRSSs.Add(s));
            context.SaveChanges();

            var items = new List<NewsItem>
            {
                new NewsItem {
                    Title = "����1",
                    Description = "�������� ������ 1 �� �����",
                    PublicDate = new DateTime(2015,01,23),
                    Url = "https:\\test.te",
                    ResurseRSS = context.ResurseRSSs.FirstOrDefault(c => c.Name == "���������")
                },
                new NewsItem {
                    Title = "����2",
                    Description = "�������� ������ 2 �� ����������",
                    PublicDate = new DateTime(2015,01,3),
                    Url = "https:\\test.te",
                    ResurseRSS = context.ResurseRSSs.FirstOrDefault(c => c.Name == "���������")
                }
            };
            items.ForEach(s => context.NewsItems.Add(s));
            context.SaveChanges();
        }
    }
}
