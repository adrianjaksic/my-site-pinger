using Pinger.Repository;
using Pinger.Services;
using System;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace Pinger
{
    public partial class MySitePingerService : ServiceBase
    {
        public MySitePingerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Set up a timer that triggers every minute.
            Timer timer = new Timer();
            timer.Interval = 60000; // 60 seconds
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            PingWebsites();
            SendEmail();
        }

        private void PingWebsites()
        {
            var ping = new System.Net.NetworkInformation.Ping();
            var sites = SitesRepository.GetWebsiteToCheck();
            foreach (var item in sites)
            {
                try
                {
                    var result = ping.Send(item.Url);
                    SitesRepository.SaveStatusCheck(item.Id, result.Status.ToString());
                }
                catch (Exception exc)
                {
                    SitesRepository.SaveStatusCheck(item.Id, exc.Message);
                }
            }
        }

        private void SendEmail()
        {
            try
            {
                var sites = SitesRepository.GetWebsitesWithSameErrorStatus();             
                foreach (var item in sites)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Dear,");
                    sb.AppendLine($"On {item.FirstPingTime} url {item.Url} is pinged with status {item.Status}.");
                    sb.AppendLine($"Number of pings with this status {item.SameStatus}.");
                    MailService.SendEmail(item.Emails, $"Website {item.Url} down!", sb.ToString());
                    SitesRepository.CheckMailSent(item.Id);
                }               
            }
            catch (Exception)
            {
                
            }
        }

        protected override void OnStop()
        {
        }
    }
}
