using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Common.Models
{
    public class SinhVien
    {
        public virtual int MaSinhVien { get; set; }
        public virtual string TenSinhVien { get; set; } = null!;
        public virtual DateTime NgaySinh { get; set; }
        public virtual string DiaChi { get; set; } = null!;
        public virtual int MaLopHoc { get; set; }
        public virtual LopHoc? LopHoc { get; set; } 
    }
}
