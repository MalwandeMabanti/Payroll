namespace Payroll.Interfaces
{
    public interface IGenericEnumService<T> : IGenericService<T>
        where T : class
    {
    }
}
