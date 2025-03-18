using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.gRPC.Dtos.Student
{
    [DataContract]
    public class SinhVienDto
    {
        [DataMember(Order = 1)]
        public int MaSinhVien { get; set; }
        [DataMember(Order = 2)]
        public string TenSinhVien { get; set; } = null!;
        [DataMember(Order = 3)]
        public DateTime NgaySinh { get; set; }
        [DataMember(Order = 4)]
        public string DiaChi { get; set; } = null!;
        [DataMember(Order = 5)]
        public int MaLopHoc { get; set; }
        [DataMember(Order = 6)]
        public string TenLop { get; set; } = null!;
        [DataMember(Order = 7)]
        public string MonHoc { get; set; } = null!;

        public SinhVienDto() { }

    }
}
