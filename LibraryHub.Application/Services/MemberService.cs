using AutoMapper;
using FluentValidation;
using LibraryHub.Application.DTOs;
using LibraryHub.Application.Interfaces;
using LibraryHub.Application.Results;
using LibraryHub.Domain.Entities;

namespace LibraryHub.Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IGenericRepository<Member> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateMemberDto> _validator;

        public MemberService(
            IGenericRepository<Member> repository,
            IMapper mapper,
            IValidator<CreateMemberDto> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<IEnumerable<MemberDto>>> GetAllAsync()
        {
            var members = await _repository.GetAllAsync();
            var memberDtos = _mapper.Map<IEnumerable<MemberDto>>(members);

            return Result<IEnumerable<MemberDto>>.Success(memberDtos);
        }

        public async Task<Result<MemberDto>> GetByIdAsync(int id)
        {
            var member = await _repository.GetByIdAsync(id);

            if (member == null)
                return Result<MemberDto>.Failure("Üye bulunamadı.");

            var memberDto = _mapper.Map<MemberDto>(member);

            return Result<MemberDto>.Success(memberDto);
        }

        public async Task<Result<MemberDto>> CreateAsync(CreateMemberDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(
                    " ",
                    validationResult.Errors.Select(e => e.ErrorMessage));

                return Result<MemberDto>.Failure(errors);
            }

            var member = _mapper.Map<Member>(dto);

            await _repository.AddAsync(member);
            await _repository.SaveChangesAsync();

            var memberDto = _mapper.Map<MemberDto>(member);

            return Result<MemberDto>.Success(memberDto);
        }

        public async Task<Result<MemberDto>> UpdateAsync(
            int id,
            UpdateMemberDto dto)
        {
            var member = await _repository.GetByIdAsync(id);

            if (member == null)
                return Result<MemberDto>.Failure("Üye bulunamadı.");

            _mapper.Map(dto, member);

            _repository.Update(member);
            await _repository.SaveChangesAsync();

            var memberDto = _mapper.Map<MemberDto>(member);

            return Result<MemberDto>.Success(memberDto);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var member = await _repository.GetByIdAsync(id);

            if (member == null)
                return Result<bool>.Failure("Üye bulunamadı.");

            _repository.Delete(member);
            await _repository.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}