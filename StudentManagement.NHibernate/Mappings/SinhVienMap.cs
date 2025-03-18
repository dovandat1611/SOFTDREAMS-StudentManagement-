
using FluentNHibernate.Mapping;
using StudentManagement.NHibernate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.NHibernate.Mappings
{
    public class SinhVienMap : ClassMap<SinhVien>
    {
        public SinhVienMap()
        {
            Table("SinhVien");
            Id(x => x.MaSinhVien).GeneratedBy.Identity(); // ID tự động tăng
            Map(x => x.TenSinhVien).Length(100).Not.Nullable();
            Map(x => x.NgaySinh).Not.Nullable();
            Map(x => x.DiaChi).Length(255).Not.Nullable();
            References(x => x.LopHoc).Column("MaLopHoc").Not.Nullable();
        }
    }
}
