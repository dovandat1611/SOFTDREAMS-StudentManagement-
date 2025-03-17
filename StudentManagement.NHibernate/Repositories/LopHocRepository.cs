﻿using NHibernate;
using StudentManagement.NHibernate.IRepositories;
using StudentManagement.NHibernate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.NHibernate.Repositories
{
    public class LopHocRepository : GenericRepository<LopHoc>, ILopHocRepository
    {
        public LopHocRepository(ISession session) : base(session) { }
    }
}
