using AutoMapper;
using LibraryHub.Application.DTOs;
using LibraryHub.Application.Interfaces;
using LibraryHub.Domain.Entities;

namespace LibraryHub.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _repository;
        private readonly IMapper _mapper;

        public BookService(
            IGenericRepository<Book> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null)
                return null;

            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> CreateAsync(CreateBookDto dto)
        {
            var book = _mapper.Map<Book>(dto);

            await _repository.AddAsync(book);

            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto?> UpdateAsync(int id, UpdateBookDto dto)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null)
                return null;

            _mapper.Map(dto, book);

            _repository.Update(book);

            return _mapper.Map<BookDto>(book);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null)
                return false;

            _repository.Delete(book);

            return true;
        }
    }
}