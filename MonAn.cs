using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTapDesktop
{
    public class MonAn
    {
        public int maMonAn { get; set; }
        public string tenMonAn { get; set; }
        public int nhom { get; set; }

        public MonAn()
        {

        }

        public MonAn(int maMonAn, string tenMonAn, int nhom)
        {
            this.maMonAn = maMonAn;
            this.tenMonAn = tenMonAn;
            this.nhom = nhom;
        }
    }
}
