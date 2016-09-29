using System;
using Ninject;
using ReadRssFeeds.DataProvider.Abstract;
using ReadRssFeeds.ConsoleControl.Infrastructure;

namespace ReadRssFeeds.ConsoleControl
{
    class Program
    {
        private static Bindings kernel;
        private static RssReader readerRSSFeeds;

        private static void initialize()
        {
            kernel = new Bindings();
            readerRSSFeeds = new RssReader(kernel.Configure().Get<IResurseRSSRepository>());
        }

        static int Main(string[] args)
        {
            initialize();

            if (args.Length != 1)
            {
                Help();
                return 0;
            }

            switch (args[0])
            {
                case "read":
                    {
                        try
                        {
                            var items = readerRSSFeeds.ReadRssFeeds();
                            readerRSSFeeds.PrintToConsole(items);
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine(ex.Message);
                        }
                        break;
                    }
                case "update":
                    {
                        try
                        {
                            var items = readerRSSFeeds.ReadRssFeeds();
                            readerRSSFeeds.SaveRssFeeds(items);
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine(ex.Message);
                        }
                        break;
                    }
                default:
                    {
                        Help();
                        break;
                    }
            }
            return 0;
        }

        private static void Help()
        {
            Console.WriteLine("usage: rssfeeds [--help]");
            Console.CursorLeft = 16;
            Console.WriteLine("<command> [<args>]\n");
            Console.WriteLine("Used git commands are:");
            Console.WriteLine(" read\n update");
        }
    }
}