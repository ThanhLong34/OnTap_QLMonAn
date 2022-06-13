using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTapDesktop
{
    public class QuanLyMonAn
    {
        public List<MonAn> dsMonAn;

        public QuanLyMonAn()
        {
            dsMonAn = new List<MonAn>();
        }

        public void themMonAn(MonAn i)
        {
            dsMonAn.Add(i);
        }

        public void clear()
        {
            dsMonAn.Clear();
        }

        public MonAn timMonAnTheoMa(int ma)
        {
            return dsMonAn.Find(i => i.maMonAn == ma);
        }

        public List<MonAn> timKiemMonAnTheoTen(string ten)
        {
            string value = ten.ToLower();
            return dsMonAn.FindAll(i => i.tenMonAn.ToLower().Contains(value));
        }
    }
}
