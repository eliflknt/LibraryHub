using LibraryHub.Application.DTOs;
using LibraryHub.Application.Results;

namespace LibraryHub.Application.Services
{
    public interface IBookService
    {
        Task<Result<IEnumerable<BookDto>>> GetAllAsync();

        Task<Result<BookDto>> GetByIdAsync(int id);

        Task<Result<BookDto>> CreateAsync(CreateBookDto dto);

        Task<Result<BookDto>> UpdateAsync(int id, UpdateBookDto dto);

        Task<Result<bool>> DeleteAsync(int id);
    }
}