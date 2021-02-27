using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using LoadTruck.ljcbed;

namespace LoadTruck.Models
{

    public class TruckLoad
    {
        public bool hasError { get; set; }
        public int LoadID { get; set; }
        public string Ship_to_Route_Code { get; set; }
        public string TrailerID { get; set; }
        public string LawTag { get; set; }
        public string LawTagStatus { get; set; }
        public string LawTagDelete { get; set; }
        public DateTime Ship_Date { get; set; }
        public byte Closed { get; set; }
        public TruckLoad()
        {
            this.hasError = false;
            LawTagStatus = "none";
            LawTagDelete = "no";
        }


        public string store()
        {

            ljcbed.Load_Mgt itemService = new Load_Mgt();
            itemService.Url = "http://jcbed-2014.jcbed.local:6047/JCBED/WS/JCBTestCo/Codeunit/Load_Mgt";
            NetworkCredential myCredentials = new NetworkCredential("Administrator", "1934parker");
            itemService.UseDefaultCredentials = false;
            itemService.Credentials = myCredentials;
            try
            {
                //string result = itemService.CreateLoad("02", "t1", DateTime.Today.AddDays(1));
                string result = itemService.CreateLoad(this.Ship_to_Route_Code, this.TrailerID, this.Ship_Date);
               
                return result;
            }
            catch (Exception ex)
            {
                this.hasError = true;
                return "ERROR";
            }
        }


        public string checkTrailer()
        {

            ljcbed.Load_Mgt itemService = new Load_Mgt();
            itemService.Url = "http://jcbed-2014.jcbed.local:6047/JCBED/WS/JCBTestCo/Codeunit/Load_Mgt";
            NetworkCredential myCredentials = new NetworkCredential("Administrator", "1934parker");
            itemService.UseDefaultCredentials = false;
            itemService.Credentials = myCredentials;
            try
            {
                //string result = itemService.CreateLoad("02", "t1", DateTime.Today.AddDays(1));
                string result = itemService.VerifyTrailer( this.TrailerID);

                return result;
            }
            catch (Exception ex)
            {
                this.hasError = true;
                return "ERROR";
            }
        }
        public string closeTrailer()
        {
            this.hasError = false;
            ljcbed.Load_Mgt itemService = new Load_Mgt();
            itemService.Url = "http://jcbed-2014.jcbed.local:6047/JCBED/WS/JCBTestCo/Codeunit/Load_Mgt";
            NetworkCredential myCredentials = new NetworkCredential("Administrator", "1934parker");
            itemService.UseDefaultCredentials = false;
            itemService.Credentials = myCredentials;
            try
            {
                //string result = itemService.CreateLoad("02", "t1", DateTime.Today.AddDays(1));
                string result = itemService.CloseLoad(this.TrailerID);

                return result;
            }
            catch (Exception ex)
            {
                this.hasError = true;
                return "ERROR";
            }
        }



    }
}