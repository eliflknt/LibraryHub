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

        public async Task<Result<IEnumerable<BookDto>>> GetAllAsync()
        {
            var books = await _repository.GetAllAsync();
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books);

            return Result<IEnumerable<BookDto>>.Success(bookDtos);
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
                var errors = string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage));

                return Result<BookDto>.Failure(errors);
            }

            var book = _mapper.Map<Book>(dto);

            await _repository.AddAsync(book);

            var bookDto = _mapper.Map<BookDto>(book);

            return Result<BookDto>.Success(bookDto);
        }

        public async Task<Result<BookDto>> UpdateAsync(int id, UpdateBookDto dto)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null)
                return Result<BookDto>.Failure("Kitap bulunamadı.");

            _mapper.Map(dto, book);

            _repository.Update(book);

            var bookDto = _mapper.Map<BookDto>(book);

            return Result<BookDto>.Success(bookDto);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null)
                return Result<bool>.Failure("Kitap bulunamadı.");

            _repository.Delete(book);

            return Result<bool>.Success(true);
        }
    }
}