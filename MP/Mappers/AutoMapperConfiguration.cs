using AutoMapper;
using MP.Model.Models;
using MP.Models;

namespace MP.Mappers
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToViewModelMappingProfile>();
                x.AddProfile<ViewModelToDomainMappingProfile>();
            });
        }
    }

    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<TripModel, Trip>();
            Mapper.CreateMap<PassengerModel, Passenger>();
            Mapper.CreateMap<ItemModel, Item>();
        }
    }

    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Trip, TripModel>();
            Mapper.CreateMap<Passenger, PassengerModel>().ForMember(a => a.Town, o => o.MapFrom(a => a.Town.ToString()));
            Mapper.CreateMap<Item, ItemModel>()
                .ForMember(a => a.TripDepartureDate, o => o.MapFrom(a => a.Trip.DepartureDate.ToString("dd/MM/yyyy")));
        }
    }
}