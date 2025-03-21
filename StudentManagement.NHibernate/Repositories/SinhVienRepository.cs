using NHibernate;
using NHibernate.Linq;
using StudentManagement.Common.Dtos;
using StudentManagement.NHibernate.IRepositories;
using StudentManagement.NHibernate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.NHibernate.Repositories
{
    public class SinhVienRepository : GenericRepository<SinhVien>, ISinhVienRepository
    {
        private readonly ISession _session;
        public SinhVienRepository(ISession session) : base(session) {
            _session = session;
        }

        public async Task<GetSearchObjectDto<SinhVien>> GetStudentListWithClassAsync(int pageNumber, int pageSize, string searchBySomething, bool? sort)
        {
            try
            {
                var query = _session.Query<SinhVien>().Fetch(s => s.LopHoc).AsQueryable();

                // Lọc theo từ khóa tìm kiếm
                if (!string.IsNullOrEmpty(searchBySomething))
                {
                    query = query.Where(s => s.TenSinhVien.Contains(searchBySomething) ||
                                             s.MaSinhVien.ToString().Contains(searchBySomething) ||
                                             s.DiaChi.Contains(searchBySomething));
                }

                // Sắp xếp nếu có yêu cầu
                if (sort.HasValue)
                {
                    query = sort.Value ? query.OrderBy(s => s.TenSinhVien) : query.OrderByDescending(s => s.TenSinhVien);
                }

                // Lấy tổng số bản ghi không bị ảnh hưởng bởi phân trang
                int totalRecord = await query.CountAsync();

                // Lấy dữ liệu theo trang
                var sinhViens = await query.Skip((pageNumber - 1) * pageSize)
                                           .Take(pageSize)
                                           .ToListAsync();

                return new GetSearchObjectDto<SinhVien>
                {
                    totalRecord = totalRecord, // Số lượng bản ghi hợp lệ
                    Data = sinhViens
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new GetSearchObjectDto<SinhVien>(); // Trả về đối tượng rỗng nếu có lỗi
            }
        }



    }
}
