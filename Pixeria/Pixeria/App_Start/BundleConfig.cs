﻿using System.Web;
using System.Web.Optimization;

namespace Pixeria
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/welcome").Include(
                      "~/Scripts/jquery.interactive_bg.js",
                      "~/Scripts/document.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/bootswatch/yeti/bootstrap.min.css",
                      "~/Content/site.css", "~/Content/style.css"));

            bundles.Add(new StyleBundle("~/Content/welcomecss").Include(
                "~/Content/welcome_style.css"));

            bundles.Add(new StyleBundle("~/Content/errorcss").Include(
               "~/Content/error.css"));
        }
    }
}
