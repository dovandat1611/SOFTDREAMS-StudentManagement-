using StudentManagement.NHibernate.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.NHibernate.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGiaoVienRepository GiaoVien { get; }
        ILopHocRepository LopHoc { get; }
        ISinhVienRepository SinhVien { get; }

        void BeginTransaction();   
        Task CommitAsync();        
        Task RollbackAsync();     
        Task<int> SaveChangesAsync(); 
    }
}
