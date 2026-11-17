using System.Collections.Generic;
using AntiqueStore.Models;

namespace AntiqueStore.DAL
{
    public interface IDataRepository
    {
        List<Antique> LoadAll();
        void SaveAll(List<Antique> items);
    }
}