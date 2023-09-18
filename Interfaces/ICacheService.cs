namespace Payroll.Interfaces
{
    public interface ICacheService
    {
        List<T> GetData<T>(string key);

        void SaveData<T>(string key, List<T> data, TimeSpan timeSpan);
    }
}