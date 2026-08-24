using LibraryHub.Application.DTOs;
using LibraryHub.Application.Results;

namespace LibraryHub.Application.Services
{
    public interface IBookService
    {
        Task<Result<PagedResult<BookDto>>> GetAllAsync(
            int page,
            int pageSize,
            int? categoryId,
            string? search);

        Task<Result<BookDto>> GetByIdAsync(int id);

        Task<Result<BookDto>> CreateAsync(CreateBookDto dto);

        Task<Result<BookDto>> UpdateAsync(int id, UpdateBookDto dto);

        Task<Result<bool>> DeleteAsync(int id);
    }
}