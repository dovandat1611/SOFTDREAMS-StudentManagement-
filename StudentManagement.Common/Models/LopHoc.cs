using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Common.Models
{
    public class LopHoc
    {
        public virtual int MaLopHoc { get; set; }
        public virtual string TenLop { get; set; } = null!;
        public virtual string MonHoc { get; set; } = null!;
        public virtual int MaGiaoVien { get; set; }
        public virtual GiaoVien GiaoVien { get; set; } = null!;
        public virtual ICollection<SinhVien> SinhViens { get; set; } = null!;
    }
}
