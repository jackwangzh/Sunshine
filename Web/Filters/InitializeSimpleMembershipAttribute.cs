﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using Sunshine.Business.Core;
using WebMatrix.WebData;

namespace Sunshine.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
            if (WebSecurity.IsAuthenticated && WebSecurity.CurrentUserId < 0)
            {
                WebSecurity.Logout();
            }
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                
            }
        }
    }
}
