using System;
using System.Collections.Generic;
using System.Text;

namespace Pegasus.Model
{
    public class Image
    {
        public int ImageID { get; set; }
        public string Thumbnail { get; set; }
        public bool Tiny { get; set; }
        public bool Small { get; set; }
        public bool Medium { get; set; }
        public bool Orginal { get; set; }
        public string CreateBy { get; set; }

    }
}
