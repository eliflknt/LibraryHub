using AutoMapper;
using FluentValidation;
using LibraryHub.Application.DTOs;
using LibraryHub.Application.Interfaces;
using LibraryHub.Application.Results;
using LibraryHub.Domain.Entities;

namespace LibraryHub.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBookDto> _validator;

        public BookService(
            IGenericRepository<Book> repository,
            IMapper mapper,
            IValidator<CreateBookDto> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<PagedResult<BookDto>>> GetAllAsync(
            int page,
            int pageSize,
            int? categoryId,
            string? search)
        {
            if (page < 1)
                page = 1;

            if (pageSize < 1)
                pageSize = 10;

            if (pageSize > 100)
                pageSize = 100;

            System.Linq.Expressions.Expression<Func<Book, bool>>? predicate = null;

            if (categoryId.HasValue && !string.IsNullOrWhiteSpace(search))
            {
                predicate = book =>
                    book.CategoryId == categoryId.Value &&
                    book.Title.ToLower().Contains(search.ToLower());
            }
            else if (categoryId.HasValue)
            {
                predicate = book =>
                    book.CategoryId == categoryId.Value;
            }
            else if (!string.IsNullOrWhiteSpace(search))
            {
                predicate = book =>
                    book.Title.ToLower().Contains(search.ToLower());
            }

            var skip = (page - 1) * pageSize;

            var result = await _repository.GetPagedAsync(
                predicate,
                skip,
                pageSize);

            var bookDtos = _mapper.Map<List<BookDto>>(result.Items);

            var pagedResult = new PagedResult<BookDto>
            {
                Items = bookDtos,
                Page = page,
                PageSize = pageSize,
                TotalCount = result.TotalCount
            };

            return Result<PagedResult<BookDto>>.Success(pagedResult);
        }

        public async Task<Result<BookDto>> GetByIdAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null)
                return Result<BookDto>.Failure("Kitap bulunamadı.");

            var bookDto = _mapper.Map<BookDto>(book);

            return Result<BookDto>.Success(bookDto);
        }

        public async Task<Result<BookDto>> CreateAsync(CreateBookDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(
                    " ",
                    validationResult.Errors.Select(e => e.ErrorMessage));

                return Result<BookDto>.Failure(errors);
            }

            var book = _mapper.Map<Book>(dto);

            await _repository.AddAsync(book);
            await _repository.SaveChangesAsync();

            var bookDto = _mapper.Map<BookDto>(book);

            return Result<BookDto>.Success(bookDto);
        }

        public async Task<Result<BookDto>> UpdateAsync(
            int id,
            UpdateBookDto dto)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null)
                return Result<BookDto>.Failure("Kitap bulunamadı.");

            _mapper.Map(dto, book);

            _repository.Update(book);
            await _repository.SaveChangesAsync();

            var bookDto = _mapper.Map<BookDto>(book);

            return Result<BookDto>.Success(bookDto);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null)
                return Result<bool>.Failure("Kitap bulunamadı.");

            _repository.Delete(book);
            await _repository.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}