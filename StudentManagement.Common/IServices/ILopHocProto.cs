using StudentManagement.Common.Dtos;
using StudentManagement.Common.Dtos.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Common.IServices
{
    [ServiceContract]
    public interface ILopHocProto
    {
        [OperationContract]
        Task<ResponseWrapper<List<LopHocDto>>> GetAllLopHocAsync();
    }
}
