using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Web.Models;
using AutoMapper;

namespace Agilisium.TalentManager.Web.App_Start
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                //x.AddProfile(new DomainToViewModelMappingProfile());
                //x.AddProfile(new ViewModelToDomainMappingProfile());
            });
        }
    }

    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName => "DomainToViewModelMappingProfile";

        public DomainToViewModelMappingProfile()
        {
            CreateMap<DropDownCategoryDto, CategoryModel>();
            CreateMap<DropDownSubCategoryDto, SubCategoryModel>();
            CreateMap<EmployeeDto, EmployeeModel>();
            CreateMap<PracticeDto, PracticeModel>();
            CreateMap<SubPracticeDto, SubPracticeModel>();
            CreateMap<ProjectDto, ProjectModel>();
            CreateMap<ProjectAllocationDto, AllocationModel>();
        }
    }

    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName => "ViewModelToDomainMappingProfile";

        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CategoryModel, DropDownCategoryDto>();
            CreateMap<SubCategoryModel, DropDownSubCategoryDto>();
            CreateMap<EmployeeModel, EmployeeDto>();
            CreateMap<PracticeModel, PracticeDto>();
            CreateMap<SubPracticeModel, SubPracticeDto>();
            CreateMap<ProjectModel, ProjectDto>();
            CreateMap<AllocationModel, ProjectAllocationDto>();
        }
    }
}