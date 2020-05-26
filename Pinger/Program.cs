using Pinger.Model;
using Pinger.Repository;
using Pinger.Services;
using System.Data.Entity;
using System.ServiceProcess;

namespace Pinger
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length >= 5)
            {
                SitesRepository.ConnectionString = args[0];
                MailService.EmailHostName = args[1];
                MailService.EmailPort = int.Parse(args[2]);
                MailService.EmailUsername = args[3];
                MailService.EmailPassword = args[4];
                MailService.EmailUseSsl = bool.Parse(args[5]);
                Database.SetInitializer(new CreateDatabaseIfNotExists<SiteContext>());
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new MySitePingerService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
