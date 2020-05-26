using Pinger.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pinger.Repository
{
    public static class SitesRepository
    {
        public static string ConnectionString;

        public static List<Site> GetWebsiteToCheck()
        {
            using (var context = new SiteContext(ConnectionString))
            {
                return context.Sites.Where(s => s.Active).ToList();
            }
        }

        public static List<Site> GetWebsitesWithSameErrorStatus()
        {
            using (var context = new SiteContext(ConnectionString))
            {
                return context.Sites.Where(s => s.Status != System.Net.NetworkInformation.IPStatus.Success.ToString() && ((s.ErrorSent == 0 && s.SameStatus == 2) || (s.SameStatus % 12 * s.ErrorSent == 0))).ToList();
            }
        }

        public static void SaveStatusCheck(short id, string status)
        {
            using (var context = new SiteContext(ConnectionString))
            {
                var today = DateTime.Now;
                var site = context.Sites.Where(s => s.Id == id).SingleOrDefault();
                if (site != null)
                {
                    if (site.Status == status)
                    {
                        site.SameStatus++;
                    }
                    else
                    {
                        site.Status = status;
                        site.SameStatus = 0;
                        site.FirstPingTime = today;
                        site.ErrorSent = 0;
                    }

                    context.SiteStatuses.Add(new SiteStatus() 
                    {
                        Id = id,
                        Status = status,
                        PingTime = today,
                    });

                    context.SaveChanges();
                }
            }
        }

        public static void CheckMailSent(short id)
        {
            using (var context = new SiteContext(ConnectionString))
            {
                var site = context.Sites.Where(s => s.Id == id).SingleOrDefault();
                if (site != null)
                {
                    site.ErrorSent++;
                    context.SaveChanges();
                }
            }
        }
    }
}
