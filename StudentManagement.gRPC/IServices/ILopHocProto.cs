using StudentManagement.gRPC.Common;
using StudentManagement.gRPC.Dtos.Class;
using StudentManagement.gRPC.Dtos.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.gRPC.IServices
{
    [ServiceContract]
    public interface ILopHocProto
    {
        [OperationContract]
        Task<ResponseWrapper<List<LopHocDto>>> GetAllLopHocAsync();
    }
}
