using System;
using AutoMapper;
using Shipments.Shipments;
using Shipments.Shipments.Models;

namespace Shipments;

public class ShipmentsApplicationAutoMapperProfile : Profile
{
    public ShipmentsApplicationAutoMapperProfile()
    {
        CreateMap<CreateShipmentRequest, Shipment>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => ShipmentStatus.Created))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => (DateTime?)null))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ForMember(dest => dest.ExtraProperties, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore());

        CreateMap<ShipmentItemRequest, ShipmentItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ShipmentId, opt => opt.Ignore())
            .ForMember(dest => dest.Shipment, opt => opt.Ignore());

        CreateMap<Shipment, ShipmentResponse>();

        CreateMap<ShipmentItem, ShipmentItemResponse>();
    }
}
