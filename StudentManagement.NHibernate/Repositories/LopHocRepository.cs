using NHibernate;
using NHibernate.Linq;
using StudentManagement.Common.Dtos;
using StudentManagement.NHibernate.IRepositories;
using StudentManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.NHibernate.Repositories
{
    public class LopHocRepository : GenericRepository<LopHoc>, ILopHocRepository
    {
        private readonly ISession _session;
        public LopHocRepository(ISession session) : base(session) { 
            _session = session;
        }

    }
}
