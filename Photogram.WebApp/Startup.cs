﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Photogram.WebApp.Startup))]
namespace Photogram.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}