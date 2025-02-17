﻿using AutoMapper;
using Get_A_Taxi.Models;
using Get_A_Taxi.Web.Infrastructure.Mapping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Get_A_Taxi.Web.Models
{
    public class OrderDetailsDTO : OrderDTO, IHaveCustomMappings
    {
        // Customer details
        [JsonProperty(PropertyName = "customerId")]
        public string CustomerId { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "custPhone")]
        public string CustomerPhoneNumber { get; set; }

        [JsonProperty(PropertyName = "orderedAt")]
        public DateTime OrderedAt { get; set; }

        // Properties, updated by taxi assignment
        [JsonProperty(PropertyName = "taxiId")]
        public int TaxiId { get; set; }

        [JsonProperty(PropertyName = "taxiPlate")]
        public string TaxiPlate { get; set; }

        [JsonProperty(PropertyName = "driverPhone")]
        public string DriverPhone { get; set; }

        [JsonProperty(PropertyName = "driverName")]
        public string DriverName { get; set; }

        [JsonProperty(PropertyName = "arrivalTime")] // in minutes
        public int ArrivalTime { get; set; }

        [JsonProperty(PropertyName = "bill")]
        public decimal Bill { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Order, OrderDetailsDTO>()
                .ForMember(vm => vm.CustomerId, opt => opt.MapFrom(m => m.Customer.Id))
                .ForMember(vm => vm.FirstName, opt => opt.MapFrom(m => m.Customer.FirstName))
                .ForMember(vm => vm.LastName, opt => opt.MapFrom(m => m.Customer.LastName))
                .ForMember(vm => vm.CustomerPhoneNumber, opt => opt.MapFrom(m => m.Customer.PhoneNumber))
                .ForMember(vm => vm.Status, opt => opt.MapFrom(m => (int)m.OrderStatus))
                .ForMember(vm => vm.TaxiId, opt => opt.MapFrom(m => (m.AssignedTaxi != null) ? m.AssignedTaxi.TaxiId : -1))
                .ForMember(vm => vm.TaxiPlate, opt => opt.MapFrom(m => (m.AssignedTaxi != null) ? m.AssignedTaxi.Plate : ""))
                .ForMember(vm => vm.DriverPhone, opt => opt.MapFrom(m => (m.AssignedTaxi != null && m.AssignedTaxi.Driver != null) ? m.AssignedTaxi.Driver.PhoneNumber : String.Empty))
                .ForMember(vm => vm.DriverName, opt => opt.MapFrom(m => m.Driver.FirstName + " " + m.Driver.LastName));
        }

        // TODO: Replace with automapper and custom mappings
        public static Func<OrderDetailsDTO, ApplicationUser, Order> ToOrderModel
        {
            get
            {
                return (o, u) => new Order
                {
                    OrderAddress = o.OrderAddress,
                    OrderLatitude = o.OrderLatitude,
                    OrderLongitude = o.OrderLongitude,
                    OrderedAt = DateTime.Now,
                    DestinationAddress = o.DestinationAddress,
                    DestinationLatitude = o.DestinationLatitude,
                    DestinationLongitude = o.DestinationLongitude,
                    UserComment = o.UserComment,
                    OrderStatus = (OrderStatus)o.Status,
                    Customer = u
                };
            }
        }
    }
}