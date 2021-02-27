using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using LoadTruck.ljcbed;


namespace LoadTruck.Models
{
    public class Lawtag
    {
        public string TagNum { get; set; }
        public string TrailerID { get; set; }
        public bool hasError { get; set; }

        public Lawtag()
        {

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
                string result = itemService.ScanTag(this.TrailerID,int.Parse(this.TagNum));
               
                return result;
            }
            catch (Exception ex)
            {
                this.hasError = true;
                return "ERROR";
            }
        }
        public string delete()
        {
            ljcbed.Load_Mgt itemService = new Load_Mgt();
            itemService.Url = "http://jcbed-2014.jcbed.local:6047/JCBED/WS/JCBTestCo/Codeunit/Load_Mgt";
            NetworkCredential myCredentials = new NetworkCredential("Administrator", "1934parker");
            itemService.UseDefaultCredentials = false;
            itemService.Credentials = myCredentials;
            try
            {
                //string result = itemService.CreateLoad("02", "t1", DateTime.Today.AddDays(1));
                string result = itemService.UnloadTag( int.Parse(this.TagNum));

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