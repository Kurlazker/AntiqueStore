using System.Collections.Generic;
using AntiqueStore.Models;

namespace AntiqueStore.BLL
{
    public interface IInventoryService
    {
        List<Antique> GetAll();
        void Add(Antique item);
        void Remove(int id);
        List<Antique> FilterByPrice(decimal min, decimal max);
        List<Antique> SearchByName(string query);
        decimal CalculateTotalValue();
        int GetCount();
    }
}