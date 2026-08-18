using AutoMapper;
using LibraryHub.Application.DTOs;
using LibraryHub.Domain.Entities;

namespace LibraryHub.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Book
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Baslik, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.YayinYili, opt => opt.MapFrom(src => src.PublishYear));

            CreateMap<CreateBookDto, Book>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Baslik))
                .ForMember(dest => dest.PublishYear, opt => opt.MapFrom(src => src.YayinYili));

            CreateMap<UpdateBookDto, Book>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Baslik))
                .ForMember(dest => dest.PublishYear, opt => opt.MapFrom(src => src.YayinYili));

            // Category
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Ad, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Aciklama, opt => opt.MapFrom(src => src.Description));

            CreateMap<CreateCategoryDto, Category>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Ad))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Aciklama));

            CreateMap<UpdateCategoryDto, Category>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Ad))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Aciklama));

            // Member
            CreateMap<Member, MemberDto>()
                .ForMember(dest => dest.AdSoyad, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Telefon, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.AktifMi, opt => opt.MapFrom(src => src.IsActive));

            CreateMap<CreateMemberDto, Member>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AdSoyad))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Telefon))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.AktifMi));

            CreateMap<UpdateMemberDto, Member>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AdSoyad))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Telefon))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.AktifMi));
        }
    }
}