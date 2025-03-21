using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Common.Models
{
    public class GiaoVien
    {
        public virtual int MaGiaoVien { get; set; }
        public virtual string TenGiaoVien { get; set; } = null!;
        public virtual DateTime NgaySinh { get; set; }
        public virtual ICollection<LopHoc> LopHocs { get; set; } = null!;
    }
}
