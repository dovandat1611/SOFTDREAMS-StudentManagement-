using FluentNHibernate.Mapping;
using StudentManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.NHibernate.Mappings
{

    public class LopHocMap : ClassMap<LopHoc>
    {
        public LopHocMap()
        {
            Table("LopHoc");
            Id(x => x.MaLopHoc).GeneratedBy.Identity();
            Map(x => x.TenLop).Length(100).Not.Nullable();
            Map(x => x.MonHoc).Length(100).Not.Nullable();
            References(x => x.GiaoVien).Column("MaGiaoVien").Not.Nullable();
            HasMany(x => x.SinhViens).KeyColumn("MaLopHoc").Inverse().Cascade.All();
        }
    }
}
