using System.Collections.Generic;
using System.Linq;
using AntiqueStore.DAL;
using AntiqueStore.Models;

namespace AntiqueStore.BLL
{
    public class InventoryService : IInventoryService
    {
        private readonly IDataRepository _repository;
        private List<Antique> _items;

        public InventoryService(IDataRepository repository)
        {
            _repository = repository;
            _items = _repository.LoadAll();
        }

        // Алгоритм 1: Получить все записи
        public List<Antique> GetAll()
        {
            return _items.ToList();
        }

        // Алгоритм: Обновить существующую запись по ID
        public void Update(Antique item)
        {
            int index = _items.FindIndex(x => x.Id == item.Id);
            if (index != -1)
            {
                _items[index] = item;
                _repository.SaveAll(_items);
            }
        }
        // Алгоритм 2: Добавить новую запись с автогенерацией ID
        public void Add(Antique item)
        {
            if (_items.Any())
            {
                item.Id = _items.Max(x => x.Id) + 1;
            }
            else
            {
                item.Id = 1;
            }

            _items.Add(item);
            _repository.SaveAll(_items);
        }

        // Алгоритм 3: Удалить запись по ID
        public void Remove(int id)
        {
            _items.RemoveAll(x => x.Id == id);
            _repository.SaveAll(_items);
        }

        // Алгоритм 4: Фильтрация по диапазону цен
        public List<Antique> FilterByPrice(decimal min, decimal max)
        {
            return _items.Where(x => x.Price >= min && x.Price <= max).ToList();
        }

        // Алгоритм 5: Поиск по названию
        public List<Antique> SearchByName(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return _items.ToList();
            }

            return _items
                .Where(x => x.Name.ToLower().Contains(query.ToLower()))
                .ToList();
        }

        // Алгоритм 6: Подсчет общей стоимости коллекции
        public decimal CalculateTotalValue()
        {
            return _items.Sum(x => x.Price);
        }

        // Алгоритм 7: Подсчет количества записей
        public int GetCount()
        {
            return _items.Count;
        }
    }
}