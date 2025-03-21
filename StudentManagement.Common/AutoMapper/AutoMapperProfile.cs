using AutoMapper;
using StudentManagement.Common.Dtos.Class;
using StudentManagement.Common.Dtos.Student;
using StudentManagement.Common.Dtos.Teacher;
using StudentManagement.Common.Dtos;
using StudentManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Common.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SinhVien, SinhVienDto>()
                    .ForMember(dest => dest.MaLopHoc, opt => opt.MapFrom(src => src.LopHoc.MaLopHoc))
                    .ForMember(dest => dest.TenLop, opt => opt.MapFrom(src => src.LopHoc.TenLop))
                    .ForMember(dest => dest.MonHoc, opt => opt.MapFrom(src => src.LopHoc.MonHoc));

            CreateMap<SinhVienDto, SinhVien>()
                    .ForMember(dest => dest.LopHoc, opt => opt.Ignore());

            CreateMap<LopHoc, LopHocDto>();

            CreateMap<GiaoVien, GiaoVienDto>().ReverseMap();
        }
    }
}
