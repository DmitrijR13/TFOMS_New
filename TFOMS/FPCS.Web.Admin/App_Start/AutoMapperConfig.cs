using AutoMapper;
using FPCS.Data.Entities;
using FPCS.Web.Admin.Models.HandAppeal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPCS.Web.Admin.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<HandAppealCreateModel, HandAppeal>();
                cfg.CreateMap<HandAppeal, HandAppealEditModel>()
                    .ForMember(x => x.TypeOfAddressingName, opt => opt.Ignore())
                    .ForMember(x => x.WayOfAddressingName, opt => opt.Ignore())
                    .ForMember(x => x.AppealTheme, opt => opt.Ignore())
                    .ForMember(x => x.OrganizationNameList, opt => opt.Ignore())
                    .ForMember(x => x.MOName, opt => opt.Ignore())
                    .ForMember(x => x.OrganizationId, opt => opt.Ignore())
                    .ForMember(x => x.MedicalOrganizationId, opt => opt.Ignore())
                    .ForMember(x => x.Organizations, opt => opt.Ignore())
                    .ForMember(x => x.MedicalOrganizations, opt => opt.Ignore())
                    .ForMember(x => x.ComplaintName, opt => opt.Ignore())
                    .ForMember(x => x.SMOOrganizationName, opt => opt.Ignore())
                    .ForMember(x => x.AppealResultName, opt => opt.Ignore())
                    .ForMember(x => x.PassedEventName, opt => opt.Ignore())
                    .ForMember(x => x.WorkerName, opt => opt.Ignore());

                cfg.CreateMap<HandAppealEditModel, HandAppeal>();
            });
        }
    }
}