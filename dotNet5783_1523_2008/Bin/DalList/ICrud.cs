namespace DalList
{
    interface ICrud<T>
    {
        void Update(T t1);
        int Create(T t1);
        void Delete(int id);
        T Read(int id);
        T[] ReadAll();

        void Print(T t1);
    }
}