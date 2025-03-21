using StudentManagement.Common.Dtos.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Common.IServices
{
    [ServiceContract]
    public interface IGiaoVienProto
    {
        [OperationContract]
        Task<List<GiaoVienDto>> GetAllGiaoVienAsync();

        [OperationContract]
        Task<List<ChartDto>> GetChartGiaoVienAsync(string teacherId);
    }
}
