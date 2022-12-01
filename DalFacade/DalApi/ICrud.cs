using DO;
namespace DalApi;
    public  interface ICrud<T> where T: struct
    {
        int Add(T item);
        void Delete(int id);
        void Update(T item);
        T GetById(int id);
        //שם חוקי??
        IEnumerable<T?> GetAll();
    }



