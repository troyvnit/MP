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
        Item GetItem(int id);
        IEnumerable<Item> GetItems();
        IEnumerable<Item> GetItems(ItemSearchModel parameter, ref int total);
        bool AddOrUpdateItem(Item item);
        bool DeleteItem(Item item);
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

        public Item GetItem(int id)
        {
            return itemRepository.GetById(id);
        }
        public IEnumerable<Item> GetItems()
        {
            var items = itemRepository.GetAll();
            return items;
        }

        public IEnumerable<Item> GetItems(ItemSearchModel parameter, ref int total)
        {
            var items = itemRepository.GetMany(a => (a.Trip.DepartureDate >= parameter.fromDate || parameter.fromDate == DateTime.MinValue)
                && (a.Trip.DepartureDate <= parameter.toDate || parameter.toDate == DateTime.MinValue)
                && (a.Trip.DepartureTime >= parameter.fromTime || parameter.fromTime == 0)
                && (a.Trip.DepartureTime <= parameter.toTime || parameter.toTime == 0) 
                && a.Trip.TripName == parameter.TripName).Skip(parameter.skip).Take(parameter.take);
            total = itemRepository.GetMany(a => (a.Trip.DepartureDate >= parameter.fromDate || parameter.fromDate == DateTime.MinValue)
                && (a.Trip.DepartureDate <= parameter.toDate || parameter.toDate == DateTime.MinValue)
                && (a.Trip.DepartureTime >= parameter.fromTime || parameter.fromTime == 0)
                && (a.Trip.DepartureTime <= parameter.toTime || parameter.toTime == 0)
                && a.Trip.TripName == parameter.TripName).Count();
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

        public bool DeleteItem(Item item)
        {
            itemRepository.Delete(item);
            unitOfWork.Commit();
            return true;
        }
    }
}
