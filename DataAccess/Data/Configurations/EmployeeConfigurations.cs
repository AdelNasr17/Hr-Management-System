
namespace DataAccess.Data.Configurations
{
    internal class EmployeeConfigurations : BaseEntityConfiguration<Employee>, IEntityTypeConfiguration<Employee>
    {


        public new  void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Id).UseIdentityColumn(1, 1);
            builder.Property(E => E.Name).HasColumnType("nvarchar(50)");
            builder.Property(E => E.Address).HasColumnType("nvarchar(150)");
            builder.Property(E => E.Salary).HasColumnType("decimal(10,2)");
            builder.Property(E => E.Gender).HasConversion((EmpGender) => EmpGender.ToString()
                            , (_gender) => (Gender)Enum.Parse(typeof(Gender), _gender));
            builder.Property(E => E.EmployeeType).HasConversion((EmpType) => EmpType.ToString()
                           , (_Type) => (EmployeeType)Enum.Parse(typeof(EmployeeType), _Type));

            base.Configure(builder);


        }
   
    }
}
