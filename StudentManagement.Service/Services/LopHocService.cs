using AutoMapper;
using StudentManagement.Common.Dtos;
using StudentManagement.Common.Dtos.Class;
using StudentManagement.Common.Dtos.Student;
using StudentManagement.Common.IServices;
using StudentManagement.Common.Models;
using StudentManagement.NHibernate.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Service.Services
{
    public class LopHocService : ILopHocProto
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LopHocService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseWrapper<List<LopHocDto>>> GetAllLopHocAsync()
        {
            var lopHocs = await _unitOfWork.LopHoc.GetAllAsync();

            var lopHocDtos = _mapper.Map<List<LopHocDto>>(lopHocs);

            return new ResponseWrapper<List<LopHocDto>>("Lấy danh sách lớp học thành công", lopHocDtos);
        }
    }
}
