using System;
using System.Collections.Generic;
using System.Linq;
using MP.Data.Infrastructure;
using MP.Data.Repository;
using MP.Model.Models;
using MP.Model.SearchModels;

namespace MP.Data.Service
{
    public interface IItemService
    {
        IEnumerable<Item> GetItems();
        IEnumerable<Item> GetItems(ItemSearchModel parameter);
        bool AddOrUpdateItem(Item item);
    }
    public class ItemService : IItemService
    {
        private readonly IItemRepository itemRepository;
        private readonly IUnitOfWork unitOfWork;

        public ItemService(IItemRepository itemRepository, IUnitOfWork unitOfWork)
        {
            this.itemRepository = itemRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Item> GetItems()
        {
            var items = itemRepository.GetAll();
            return items;
        }

        public IEnumerable<Item> GetItems(ItemSearchModel parameter)
        {
            var items = itemRepository.GetMany(a => (a.Trip.DepartureDate >= parameter.fromDate || parameter.fromDate == DateTime.MinValue)
                && (a.Trip.DepartureDate <= parameter.toDate || parameter.toDate == DateTime.MinValue)
                && (a.Trip.DepartureTime >= parameter.fromTime || parameter.fromTime == 0)
                && (a.Trip.DepartureTime <= parameter.toTime || parameter.toTime == 0));
            return items;
        }

        public bool AddOrUpdateItem(Item item)
        {
            if (item.Id != 0)
            {
                itemRepository.Update(item);
                unitOfWork.Commit();
            }
            else
            {
                itemRepository.Add(item);
                unitOfWork.Commit();
            }
            return true;
        }
    }
}
