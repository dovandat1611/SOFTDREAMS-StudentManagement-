using FluentNHibernate.Mapping;
using StudentManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.NHibernate.Mappings
{
    public class GiaoVienMap : ClassMap<GiaoVien>
    {
        public GiaoVienMap()
        {
            Table("GiaoVien");
            Id(x => x.MaGiaoVien).GeneratedBy.Identity();
            Map(x => x.TenGiaoVien).Length(100).Not.Nullable();
            Map(x => x.NgaySinh).Not.Nullable();
            HasMany(x => x.LopHocs).KeyColumn("MaGiaoVien").Inverse().Cascade.All();
        }
    }
}
