using AutoMapper;
using StudentManagement.gRPC.Common;
using StudentManagement.gRPC.Dtos.Class;
using StudentManagement.gRPC.Dtos.Student;
using StudentManagement.gRPC.IServices;
using StudentManagement.NHibernate.Models;
using StudentManagement.NHibernate.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.gRPC.Services
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
