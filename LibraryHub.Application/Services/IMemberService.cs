using LibraryHub.Application.DTOs;
using LibraryHub.Application.Results;

namespace LibraryHub.Application.Services
{
    public interface IMemberService
    {
        Task<Result<IEnumerable<MemberDto>>> GetAllAsync();

        Task<Result<MemberDto>> GetByIdAsync(int id);

        Task<Result<MemberDto>> CreateAsync(CreateMemberDto dto);

        Task<Result<MemberDto>> UpdateAsync(int id, UpdateMemberDto dto);

        Task<Result<bool>> DeleteAsync(int id);
    }
}