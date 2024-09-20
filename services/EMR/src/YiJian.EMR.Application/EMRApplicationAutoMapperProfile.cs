using AutoMapper;
using EMRService;
using Google.Protobuf.WellKnownTypes;
using System;
using YiJian.EMR.Characters.Dto;
using YiJian.EMR.Characters.Entities;
using YiJian.EMR.DailyExpressions.Dto;
using YiJian.EMR.DailyExpressions.Entities;
using YiJian.EMR.Libs;
using YiJian.EMR.Libs.Dto;
using YiJian.EMR.Libs.Entities;
using YiJian.EMR.Templates;
using YiJian.EMR.Templates.Dto;
using YiJian.EMR.Templates.Entities;
using YiJian.EMR.Writes.Dto;
using YiJian.EMR.Writes.Entities;
using YiJian.EMR.XmlHistories.Entities;

namespace YiJian.EMR
{
    public class EMRApplicationAutoMapperProfile : Profile
    {
        public EMRApplicationAutoMapperProfile()
        {
            CreateMap<Catalogue, CatalogueDto>();
            CreateMap<CatalogueDto, Catalogue>()
                .ForMember(m => m.IsDeleted, opt => opt.Ignore())
                .ForMember(m => m.DeleterId, opt => opt.Ignore())
                .ForMember(m => m.DeletionTime, opt => opt.Ignore())
                .ForMember(m => m.LastModificationTime, opt => opt.Ignore())
                .ForMember(m => m.LastModifierId, opt => opt.Ignore())
                .ForMember(m => m.CreatorId, opt => opt.Ignore())
                .ForMember(m => m.CreationTime, opt => opt.Ignore())
                .ForMember(m => m.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(m => m.ExtraProperties, opt => opt.Ignore());
            CreateMap<Catalogue, CatalogueTreeDto>()
                .ForMember(m => m.Catalogues, opt => opt.Ignore());
            CreateMap<PersonalCatalogueListDto, PersonalCatalogueTreeDto>()
                .ForMember(m => m.Catalogues, opt => opt.Ignore());
            CreateMap<MyXmlTemplate, MyXmlTemplateDto>();

            CreateMap<XmlTemplate, XmlTemplateDto>();
            CreateMap<TemplateCatalogue, GeneralCatalogueTreeDto>()
                .ForMember(m => m.Catalogues, opt => opt.Ignore());
            //CreateMap<TemplateCatalogue, DepartmentCatalogueTreeDto>()
            //    .ForMember(m => m.Catalogues, opt => opt.Ignore());
            //CreateMap<TemplateCatalogue, PersonalCatalogueTreeDto>()
            //    .ForMember(m => m.Catalogues, opt => opt.Ignore());

            CreateMap<PatientEmr, PatientEmrDto>()
                .ForMember(m => m.Title, opt => opt.MapFrom(src => src.EmrTitle))
                .ForMember(dest => dest.PdfUrl, opt => opt.Ignore());
            CreateMap<XmlHistory, XmlHistoryFullDto>();

            CreateMap<PhraseCatalogue, PhraseCatalogueInfoDto>()
                .ForMember(dest => dest.Role, opt => opt.Ignore());
            CreateMap<PhraseCatalogue, PhraseCatalogueSampleDto>()
                .ForMember(dest => dest.Role, opt => opt.Ignore());

            CreateMap<PhraseCatalogueInfoDto, PhraseCatalogue>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id ?? 0))
                .ForMember(dest => dest.Phrases, opt => opt.Ignore())
                .ForMember(m => m.IsDeleted, opt => opt.Ignore())
                .ForMember(m => m.DeleterId, opt => opt.Ignore())
                .ForMember(m => m.DeletionTime, opt => opt.Ignore())
                .ForMember(m => m.LastModificationTime, opt => opt.Ignore())
                .ForMember(m => m.LastModifierId, opt => opt.Ignore())
                .ForMember(m => m.CreatorId, opt => opt.Ignore())
                .ForMember(m => m.CreationTime, opt => opt.Ignore())
                .ForMember(m => m.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(m => m.ExtraProperties, opt => opt.Ignore());
              
            CreateMap<Phrase, PhraseDto>()
                .ForMember(dest => dest.Role, opt => opt.Ignore());
            CreateMap<PhraseDto, Phrase>()
                .ForMember(dest => dest.Catalogue, opt => opt.Ignore());

            CreateMap<UniversalCharacter, UniversalCharacterDto>();
            CreateMap<UniversalCharacter, UniversalCharacterSampleDto>();
            CreateMap<UniversalCharacterNode, UniversalCharacterNodeDto>();
            CreateMap<MergeTemplateWhiteList, MergeTemplateWhiteListDto>();

            // this.CreateGrpcMaps();
        }


        //public void CreateGrpcMaps()
        //{
        //    AllowNullDestinationValues = false;
        //    CreateMap<PatientEmr, GrpcPatientEmrModel>()
        //        .ForMember(dest => dest.CreationTime,
        //            opt => { opt.MapFrom(src => Timestamp.FromDateTime(src.CreationTime.ToUniversalTime())); });
        //}
    }
}