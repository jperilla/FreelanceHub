﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;
using Web.Models;

namespace Web.Logging
{
    public static class Logger
    {
        public static void LogException(Exception ex, IDocumentSession session)
        {
            var log = new Log { Exception = ex.GetType().ToString(), Message = ex.Message, Timestamp = DateTime.Now };
            session.Store(log);
            session.SaveChanges();
        }
    }
}