using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadTruck.Models
{
    public class Lawtag
    {
        public string TagNum { get; set; }

        public Lawtag()
        {

        }

        public string store()
        {
            return this.TagNum + " already exists";
            //return this.TagNum + " saved";
        }
    }
}