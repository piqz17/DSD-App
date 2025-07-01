using AutoMapper;
using DSD_WinformsApp.Core.DTOs;
using DSD_WinformsApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSD_WinformsApp.Core.Mapping_Profiles
{
    public class DocumentMappingProfiles: Profile
    {
        public DocumentMappingProfiles()
        {
            CreateMap<DocumentModel, DocumentDto>().ReverseMap();
            CreateMap<BackUpFileModel, BackUpFileDto>().ReverseMap();
            CreateMap<DocumentModel, DocumentDto>()
                .ForMember(dest => dest.FileData, opt => opt.MapFrom(src => System.IO.File.ReadAllBytes(src.FilePath)));
            CreateMap<UserCredentialsModel, UserCredentialsDto>().ReverseMap();

        }
       
    }
}
