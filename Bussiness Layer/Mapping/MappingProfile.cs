
namespace Bussiness_Layer.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            #region Employee Medule
            CreateMap<CreatedEmployeeDto, Employee>();
                 
            CreateMap<Employee, EmployeeToReturnDto>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(Src => Src.Gender.ToString()))
                .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(Src => Src.EmployeeType.ToString()))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(Src => Src.Department.Name ));


            CreateMap<Employee, EmployeeDetailsToReturnDto>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(Src => Src.Gender.ToString()))
                .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(Src => Src.EmployeeType.ToString()))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(Src => Src.Department.Name));
             


            CreateMap<UpdateEmployeeDto, Employee>();
            
            
            #endregion

            #region Department Medule

            CreateMap<CreatedDepartmentDto,Department >()
                .ForMember(dest => dest.CreatedOn, config => config.MapFrom(src => src.CreationDate));
            CreateMap<Department, DepartmentDetailsToReturnDto >();
            CreateMap<UpdateDepartmentDto, Department>()
                .ForMember(dest => dest.CreatedOn, config => config.MapFrom(src => src.CreationDate));
            CreateMap<Department, DepartmentToReturnDto>();

            #endregion
        }
    }
}
