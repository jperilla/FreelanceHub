using System;
using Raven.Client;
using Web.Models;

namespace Web.Logging
{
    public static class Logger
    {
        public static void LogException(Exception ex, IDocumentSession session)
        {
            var log = new Log { Exception = ex.GetType().ToString(), Message = ex.Message, Timestamp = DateTime.Now, StackTrace = ex.StackTrace };
            session.Store(log);
            session.SaveChanges();
        }
    }
}