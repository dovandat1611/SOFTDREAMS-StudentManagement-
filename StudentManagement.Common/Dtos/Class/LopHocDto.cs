using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Common.Dtos.Class
{
    [DataContract]
    public class LopHocDto
    {
        [DataMember(Order = 1)]
        public int MaLopHoc { get; set; }

        [DataMember(Order = 2)]
        public string TenLop { get; set; } = null!;

        [DataMember(Order = 3)]
        public string MonHoc { get; set; } = null!;
    }
}
