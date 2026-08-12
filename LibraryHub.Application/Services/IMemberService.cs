using LibraryHub.Application.DTOs;

namespace LibraryHub.Application.Services
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDto>> GetAllAsync();
        Task<MemberDto?> GetByIdAsync(int id);
        Task<MemberDto> CreateAsync(CreateMemberDto dto);
        Task<MemberDto?> UpdateAsync(int id, UpdateMemberDto dto);
        Task<bool> DeleteAsync(int id);
    }
}