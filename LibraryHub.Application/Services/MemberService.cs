using AutoMapper;
using LibraryHub.Application.DTOs;
using LibraryHub.Application.Interfaces;
using LibraryHub.Domain.Entities;

namespace LibraryHub.Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IGenericRepository<Member> _repository;
        private readonly IMapper _mapper;

        public MemberService(
            IGenericRepository<Member> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MemberDto>> GetAllAsync()
        {
            var members = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<MemberDto>>(members);
        }

        public async Task<MemberDto?> GetByIdAsync(int id)
        {
            var member = await _repository.GetByIdAsync(id);

            if (member == null)
                return null;

            return _mapper.Map<MemberDto>(member);
        }

        public async Task<MemberDto> CreateAsync(CreateMemberDto dto)
        {
            var member = _mapper.Map<Member>(dto);

            await _repository.AddAsync(member);

            return _mapper.Map<MemberDto>(member);
        }

        public async Task<MemberDto?> UpdateAsync(int id, UpdateMemberDto dto)
        {
            var member = await _repository.GetByIdAsync(id);

            if (member == null)
                return null;

            _mapper.Map(dto, member);

            _repository.Update(member);

            return _mapper.Map<MemberDto>(member);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var member = await _repository.GetByIdAsync(id);

            if (member == null)
                return false;

            _repository.Delete(member);

            return true;
        }
    }
}