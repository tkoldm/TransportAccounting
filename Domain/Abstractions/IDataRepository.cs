namespace Domain.Abstractions;

public interface IDataRepository<T>
{
    /// <summary>
    /// Create new record in datastore
    /// </summary>
    Task<bool> AddRecord(T record);
    /// <summary>
    /// Delete record by id
    /// </summary>
    /// <param name="id">Identifier of record</param>
    /// <typeparam name="TId">Guid || int (depends on datamodel)</typeparam>
    /// <returns>true - if record deleted<br/>false - if something went wrong</returns>
    Task<bool> DeleteRecord<TId>(TId id);
    /// <summary>
    /// Update record
    /// </summary>
    /// <param name="record">Entity that need be updated</param>
    /// <returns>true - if record updated<br/>false - if something went wrong</returns>
    Task<bool> UpdateRecord(T record);
    /// <summary>
    /// Getting record from datastore by id
    /// </summary>
    /// <param name="id">Identifier of record</param>
    /// <typeparam name="TId">Guid || int (depends on datamodel)</typeparam>
    /// <returns>Found model</returns>
    Task<T> GetRecord<TId>(TId id);
    /// <summary>
    /// Getting records from datastore using pagination
    /// </summary>
    /// <param name="count">Count of element that need to get</param>
    /// <param name="page">Number of page</param>
    /// <returns>Array of found records</returns>
    Task<T[]> GetRecords(int count, int page);
}