﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.gRPC.Dtos.Teacher
{
    [DataContract]
    public class GiaoVienDto
    {
        [DataMember(Order = 1)]
        public int MaGiaoVien { get; set; }

        [DataMember(Order = 2)]
        public string TenGiaoVien { get; set; } = null!;

        [DataMember(Order = 3)]
        public DateTime NgaySinh { get; set; }
    }
}
