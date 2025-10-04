using AutoMapper;
using RentManagement.Api.DTOs;
using RentManagement.Api.Interfaces;
using RentManagement.Api.Models;

namespace RentManagement.Api.Services
{
    public class RentRecordService : IRentRecordService
    {
        private readonly IRentRecordRepository _repo;
        private readonly IRentalAgreementRepository _agreements;
        private readonly IMapper _mapper;

        public RentRecordService(
            IRentRecordRepository repo,
            IRentalAgreementRepository agreements,
            IMapper mapper)
        {
            _repo = repo;
            _agreements = agreements;
            _mapper = mapper;
        }

        public async Task<(RentRecordDto? record, string? error)> CreateAsync(RentRecordCreateDto dto)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);
            int year = dto.Year ?? today.Year;
            int month = dto.Month ?? today.Month;

            var onDate = new DateOnly(year, month, 1);
            var agreement = await _agreements.GetActiveAgreementForShopAsync(dto.ShopId, onDate);
            if (agreement == null)
                return (null, "No active rental agreement found for the selected shop in the specified period.");

            var existing = await _repo.FindByAgreementAndPeriodAsync(agreement.Id, year, month);
            if (existing != null)
                return (null, "A rent record for this agreement and period already exists.");

            var amount = dto.Amount ?? agreement.RentAmount;

            var record = new RentRecord
            {
                AgreementId = agreement.Id,
                ShopId = agreement.ShopId,
                TenantId = agreement.TenantId,
                Year = year,
                Month = month,
                Amount = amount,
                PaidOn = DateOnly.FromDateTime(DateTime.UtcNow.Date),
                Notes = dto.Notes
            };

            await _repo.AddAsync(record);
            await _repo.SaveChangesAsync();

            var saved = await _repo.GetByIdAsync(record.Id);
            return (_mapper.Map<RentRecordDto>(saved), null);
        }

        public async Task<IEnumerable<RentRecordDto>> GetAllAsync(int? shopId, int? tenantId, int? year, int? month)
        {
            var list = await _repo.GetAllAsync(shopId, tenantId, year, month);
            return _mapper.Map<IEnumerable<RentRecordDto>>(list);
        }

        public async Task<RentRecordDto?> GetByIdAsync(int id)
        {
            var record = await _repo.GetByIdAsync(id);
            return record == null ? null : _mapper.Map<RentRecordDto>(record);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var record = await _repo.GetByIdAsync(id);
            if (record == null) return false;
            _repo.Delete(record);
            return await _repo.SaveChangesAsync();
        }
    }
}
