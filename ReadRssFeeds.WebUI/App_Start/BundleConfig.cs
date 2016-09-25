using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ReadRssFeeds.WebUI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            //jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-3.1.0.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include("~/Scripts/jquery.unobtrusive-ajax.js"));

            //Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.min.js"));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.min.css"));

           //Application
            //bundles.Add(new ScriptBundle("~/bundles/application").Include("~/Scripts/application.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/base.css"));
        }
    }
}