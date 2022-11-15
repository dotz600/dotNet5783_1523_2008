
namespace DalApi;

public interface ICrud<T>
{
    void Update(T obj);
    int Create(T obj);
    void Delete(int id);
    T Read(int id);
    
}