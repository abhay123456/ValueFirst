using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace WebApi.Models
{
    public class MailSetup
    {
        public void Sendmail(string userid,string name,string mailto)
        {
            bool mailstatus = false;
            try
            {
                

                MailMessage Msg = new MailMessage();
                Msg.From = new MailAddress(Convert.ToString(ConfigurationManager.AppSettings["mailfrom"]));
                Msg.To.Add(mailto);
                Msg.Subject = "Regarding credential";
                Msg.Body = "Dear " + name + ",<br/><br/>Please find your credential as follows:<br/><br/>UserID="+ userid + " and password is "+userid+"<br/>Thanks and Regards";
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Convert.ToString(ConfigurationManager.AppSettings["server"]);
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]); ;
                smtp.Credentials = new System.Net.NetworkCredential(Convert.ToString(ConfigurationManager.AppSettings["mailfrom"]), Convert.ToString(ConfigurationManager.AppSettings["password"]));
                smtp.EnableSsl = true;
                Msg.IsBodyHtml = true;
                smtp.Send(Msg);

            }

            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }
        }
    }
    }
