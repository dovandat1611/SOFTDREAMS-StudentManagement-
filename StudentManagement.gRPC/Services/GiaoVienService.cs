using AutoMapper;
using StudentManagement.Common;
using StudentManagement.Common.Dtos.Class;
using StudentManagement.Common.Dtos.Teacher;
using StudentManagement.Common.IServices;
using StudentManagement.Common.Models;
using StudentManagement.NHibernate.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.gRPC.Services
{
    public class GiaoVienService : IGiaoVienProto
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GiaoVienService(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GiaoVienDto>> GetAllGiaoVienAsync()
        {
            var giaoViens = await _unitOfWork.GiaoVien.GetAllAsync();

            var giaoVienDtos = _mapper.Map<List<GiaoVienDto>>(giaoViens);

            return new List<GiaoVienDto>(giaoVienDtos);
        }

        public async Task<List<ChartDto>> GetChartGiaoVienAsync(string teacherId)
        {
            var query = await _unitOfWork.GiaoVien.GetByIdAsync(int.Parse(teacherId));
            var countClassOfTeacher = query.LopHocs.Count();
            var chartGiaoVien = new List<ChartDto>();
            if (countClassOfTeacher > 0)
            {
                foreach(var item in query.LopHocs)
                {
                    ChartDto chartDto = new ChartDto()
                    {
                        MaGiaoVien = item.MaGiaoVien,
                        TenLop = item.TenLop,
                        SoSinhVien = item.SinhViens.Count(),
                    };
                    chartGiaoVien.Add(chartDto);
                }
            }
            return chartGiaoVien;
        }

    }
}
