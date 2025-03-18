using ProtoBuf.Grpc.Configuration;
using StudentManagement.gRPC.Common;
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
    public interface ISinhVienProto
    {
        [OperationContract]
        Task<ResponseWrapper<List<SinhVienDto>>> GetAllSinhVienAsync();

        [OperationContract]
        Task<ResponseWrapper<SinhVienDto>> AddSinhVienAsync(CreateSinhVienRequest request);

        [OperationContract]
        Task<ResponseWrapper<SinhVienDto>> UpdateSinhVienAsync(UpdateSinhVienRequest request);

        [OperationContract]
        Task<ResponseWrapper<bool>> DeleteSinhVienAsync(string maSinhVien);

        [OperationContract]
        Task<ResponseWrapper<IEnumerable<SinhVienDto>>> GetSortedSinhVienByNameAsync();

        [OperationContract]
        Task<ResponseWrapper<SinhVienDto?>> GetSinhVienByIdAsync(string maSinhVien);
    }
}
