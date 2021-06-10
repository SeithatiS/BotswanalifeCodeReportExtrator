using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.Entity;

namespace BotswanaLifeTransactionsExtractor
{
    class Program
    {
        public static IEnumerable<Request> ExtractDailyTransactions(DateTime day)
        {
            using (var dbContext = new Model1())
            {
                return dbContext.
                    Requests.
                    Where(x => DbFunctions.TruncateTime(x.DateTime) == day.Date && x.RequestType == 1 && (x.IsSuccessful ?? false)).                  
                    OrderByDescending(x => x.MessageIDDateTimeRequest).
                    ToList();
            }
        }

        public static string WriteToFile(IEnumerable<Request> requests, DateTime dateTime)
        {
            var sb = new StringBuilder();
            var header = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", 
                ConfigurationManager.AppSettings["Date"].ToString(),
                ConfigurationManager.AppSettings["PostOffice"].ToString(),
                ConfigurationManager.AppSettings["TransactionId"].ToString(),
                ConfigurationManager.AppSettings["CustomerName"].ToString(),
                ConfigurationManager.AppSettings["IdNumber"].ToString(),
                ConfigurationManager.AppSettings["ContractId"].ToString(),
                ConfigurationManager.AppSettings["ContactNumber"].ToString(),
                ConfigurationManager.AppSettings["Amount"].ToString());

            sb.AppendLine(header);

            foreach(var request in requests)
            {
                var postOfficeName = "";
                using (var dbContext = new Model1())
                {
                    var postOfficeCode = request.GroupId;
                    postOfficeName = dbContext.PostOffices.First(x => x.IPSCode == postOfficeCode).PostOfficeName;
                }

                var newLine = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", 
                    request.DateTime.ToString("dd/MM/yyyy"),
                    postOfficeName,
                    request.TransactionID,
                    request.CustomerName,
                    request.IdNumber,
                    request.ContractID,
                    request.MobileNumber,
                    Convert.ToDecimal(request.TransactionValue).ToString("0.00"));
                sb.AppendLine(newLine);
            }

            var fullDirectory = string.Format(@"{0}\{1}_{2}.{3}",
                 @"Y:\Users\ssefako\source\repos\BotswanaLifeTransactionsExtractor\Botswana Life\Botswanalife", dateTime.ToString("ddMMyyyy"), "botswana_life_daily_report", "txt");
            // @"Y:\source\repos\BotswanaLifeTransactionsExtractor\Botswana Life\Botswanalife", dateTime.ToString("ddMMyyyy"), "botswana_life_daily_report", "txt");
            // @"Z:\Database Backups\Botswana Life", dateTime.ToString("ddMMyyyy"), "botswana_life_daily_report", "txt");

            //@"C:\Users\Administrator\Documents\BotswanaLifeDump"

            //var fullDirectory = string.Format(@"{0}\{1}_{2}.{3}",
            //    @"Z:\Database Backups\Botswana Life", DateTime.Today.AddDays(-1).ToString("ddMMyyyy"), "botswana_life_daily_report", "txt");

            File.WriteAllText(fullDirectory, sb.ToString());

            return fullDirectory;
        }

        static void Main(string[] args)
        {
            var day = new DateTime(2021, 05, 14); //DateTime.Today.AddDays(-1);
           // var day = new DateTime(2019, 7, 30); //DateTime.Today.AddDays(-1);
            var emailList = ConfigurationManager.AppSettings["EmailAddresses"].ToString().Split(',') as IEnumerable<string>;
            var transactions = ExtractDailyTransactions(day);
            
            
            
            var dailyFile = WriteToFile(transactions, day);

            EmailSender emailSender = new EmailSender
            {
                SmtpIPAddress = "172.17.3.63",
                SmtpPort = 25,
                Body = string.Format("To whom it may concern, <br/> <br/> Please find attached the report for Mosako Premiums transactions done at BotswanaPost during {0}. <br/> <br/> Kind regards", DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy")),
                Subject = "Botswana Life Transactions Report"
            };

            //emailSender.SendEmail("mosakopremiums@botswanapost.co.bw", dailyFile, emailList);
        }
    }
}
