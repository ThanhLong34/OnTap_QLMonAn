using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTapDesktop
{
    public class QuanLyNhomMonAn
    {
        public List<NhomMonAn> dsNhomMonAn;

        public QuanLyNhomMonAn()
        {
            dsNhomMonAn = new List<NhomMonAn>();
        }

        public void themNhomMonAn(NhomMonAn i)
        {
            dsNhomMonAn.Add(i);
        }

        public NhomMonAn timNhomMonAnTheoMa(int maNhom)
        {
            return dsNhomMonAn.Find(i => i.maNhom == maNhom);
        }

        public void clear()
        {
            dsNhomMonAn.Clear();
        }
    }
}
