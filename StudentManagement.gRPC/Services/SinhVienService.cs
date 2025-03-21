using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using NHibernate.Linq;
using StudentManagement.Common.Dtos;
using StudentManagement.gRPC.Common;
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
    public class SinhVienService : ISinhVienProto
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SinhVienService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseWrapper<SinhVienDto>> AddSinhVienAsync(CreateSinhVienRequest request)
        {

            var lopHoc = await _unitOfWork.LopHoc.GetByIdAsync(request.MaLopHoc);
            if (lopHoc == null)
            {
                return new ResponseWrapper<SinhVienDto>("Lớp học không tồn tại!", null);
            }

            SinhVien sinhVien = new SinhVien()
            {
                DiaChi = request.DiaChi,
                NgaySinh = request.NgaySinh,
                TenSinhVien = request.TenSinhVien,
                MaLopHoc = request.MaLopHoc,
                LopHoc = lopHoc 
            };

            try
            {
                var create =  await _unitOfWork.SinhVien.CreateAndReturnAsync(sinhVien);
                await _unitOfWork.SaveChangesAsync();
                var createdSinhVien = create;
                if (createdSinhVien == null)
                {
                    return new ResponseWrapper<SinhVienDto>("Thêm sinh viên thất bại", null);
                }

                return new ResponseWrapper<SinhVienDto>("Thêm sinh viên thành công", _mapper.Map<SinhVienDto>(createdSinhVien));
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<SinhVienDto>("Thêm sinh viên thất bại", null);
            }
        }

        public async Task<ResponseWrapper<bool>> DeleteSinhVienAsync(string maSinhVien)
        {
            var sinhVien = await _unitOfWork.SinhVien.GetByIdAsync(int.Parse(maSinhVien));
            if (sinhVien == null)
            {
                return new ResponseWrapper<bool>("Sinh viên không tồn tại", false);
            }

            await _unitOfWork.SinhVien.DeleteAsync(sinhVien.MaSinhVien);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseWrapper<bool>("Xóa sinh viên thành công", true);
        }

        public async Task<ResponseWrapper<PagedResult<SinhVienDto>>> GetAllSinhVienAsync(SinhVienRequest request)
        {

            var response = await _unitOfWork.SinhVien.GetStudentListWithClassAsync(request.PageNumber, request.PageSize, request.searchBySomething, request.sort);
            var sinhVienDtos = _mapper.Map<List<SinhVienDto>>(response.Data);

            int totalCount = response.totalRecord;
            int totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

            var pagedSinhVienDtos = new PagedResult<SinhVienDto>
            {
                Items = sinhVienDtos,
                TotalPages = totalPages,
                CurrentPage = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };

            return new ResponseWrapper<PagedResult<SinhVienDto>>("Lấy danh sách sinh viên thành công", pagedSinhVienDtos);
        }

        public async Task<ResponseWrapper<SinhVienDto?>> GetSinhVienByIdAsync(string maSinhVien)
        {
            var sinhVien = await _unitOfWork.SinhVien.GetByIdAsync(int.Parse(maSinhVien));
            if (sinhVien == null)
            {
                return new ResponseWrapper<SinhVienDto?>("Sinh viên không tồn tại", null);
            }

            var sinhVienDto = _mapper.Map<SinhVienDto>(sinhVien);
            return new ResponseWrapper<SinhVienDto?>("Lấy thông tin sinh viên thành công", sinhVienDto);
        }

        //public async Task<ResponseWrapper<PagedResult<SinhVienDto>>> GetSortedSinhVienByNameAsync(SinhVienRequest request)
        //{

        //    var sinhViens = await _unitOfWork.SinhVien.GetStudentSortByNameAsync( );
        //    var sortedSinhViens = sinhViens.OrderBy(s => s.TenSinhVien);
        //    var sinhVienDtos = _mapper.Map<IEnumerable<SinhVienDto>>(sortedSinhViens);

        //    return new ResponseWrapper<IEnumerable<SinhVienDto>>("Sắp xếp sinh viên thành công", sinhVienDtos);
        //}

        public async Task<ResponseWrapper<SinhVienDto>> UpdateSinhVienAsync(UpdateSinhVienRequest request)
        {
            var sinhVien = await _unitOfWork.SinhVien.GetByIdAsync(request.MaSinhVien);

            if (sinhVien == null)
            {
                return new ResponseWrapper<SinhVienDto>("Sinh viên không tồn tại", null);
            }

            var lopHoc = await _unitOfWork.LopHoc.GetByIdAsync(request.MaLopHoc);
            if (lopHoc == null)
            {
                return new ResponseWrapper<SinhVienDto>("Lớp học không tồn tại!", null);
            }

            sinhVien.TenSinhVien = request.TenSinhVien;
            sinhVien.NgaySinh = request.NgaySinh;
            sinhVien.DiaChi = request.DiaChi;
            sinhVien.MaLopHoc = request.MaLopHoc;
            sinhVien.LopHoc = lopHoc;

            await _unitOfWork.SinhVien.UpdateAsync(sinhVien);
            await _unitOfWork.SaveChangesAsync();
            var sinhVienDto = _mapper.Map<SinhVienDto>(sinhVien);

            return new ResponseWrapper<SinhVienDto>("Cập nhật sinh viên thành công", sinhVienDto);
        }
    }
}
