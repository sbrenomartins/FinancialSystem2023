namespace Domain.Interfaces.Generic;

public interface IGeneric<T> where T : class
{
    Task Add(T Object);
    Task Update(T Object);
    Task Delete(T Object);
    Task<T> ReadById(int Id);
    Task<List<T>> ReadAll();
}
