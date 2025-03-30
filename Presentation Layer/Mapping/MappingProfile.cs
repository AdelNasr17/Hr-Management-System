using AutoMapper;
using Bussiness_Layer.Data_Transfer_Object.Department;
using Bussiness_Layer.Data_Transfer_Object.Employee;
using Presentation_Layer.ViewModels.Departments;
using Presentation_Layer.ViewModels.Employees;

namespace Presentation_Layer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
            {
            #region Employee Medule
            CreateMap<EmployeeViewModel, CreatedEmployeeDto>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(Src => Src.Gender.ToString()))
                .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(Src => Src.EmployeeType.ToString()))
                .ForMember(dest => dest.DepartmentID, opt => opt.MapFrom(Src => Src.DepartmentID))
                .ForMember(dest => dest.ImageURL, config => config.MapFrom(src => src.ImageURL));

            CreateMap<EmployeeDetailsToReturnDto, EmployeeViewModel>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(Src => Src.Gender.ToString()))
                .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(Src => Src.EmployeeType.ToString()))
                .ForMember(dest => dest.ImageURL, config => config.MapFrom(src => src.ImageURL));

            CreateMap<EmployeeViewModel,UpdateEmployeeDto>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(Src => Src.Gender.ToString()))
                .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(Src => Src.EmployeeType.ToString()))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(Src => Src.DepartmentID))
                .ForMember(dest => dest.ImageURL, config => config.MapFrom(src => src.ImageURL));


            #endregion

            #region Department Medule
            ////Map DepartmentViewModel to CreatedDepartmentDto
            CreateMap<DepartmentViewModel, CreatedDepartmentDto>();
            //.ForMember(dest=> dest.CreationDate,config=>config.MapFrom(src=> src.CreationDate));


            CreateMap<DepartmentDetailsToReturnDto, DepartmentViewModel>();
            CreateMap<DepartmentViewModel, UpdateDepartmentDto>();


            #endregion
        }

    }
}
