using RentManagement.Api.DTOs;

namespace RentManagement.Api.Interfaces
{
    public interface IRentRecordService
    {
        Task<(RentRecordDto? record, string? error)> CreateAsync(RentRecordCreateDto dto);
        Task<IEnumerable<RentRecordDto>> GetAllAsync(int? shopId, int? tenantId, int? year, int? month);
        Task<RentRecordDto?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
