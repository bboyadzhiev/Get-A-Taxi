﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Get_A_Taxi.Models;

namespace Get_A_Taxi.Data
{
    public interface IGetATaxiDbContext
    {
        IDbSet<Taxi> Taxies { get; set; }
        IDbSet<Order> Orders { get; set; }
        IDbSet<TaxiStand> Stands { get; set; }
        IDbSet<Photo> Photos { get; set; }
        IDbSet<District> Districts { get; set; }
        IDbSet<Location> Locations { get; set; }
        IDbSet<OperatorOrder> OperatorsOrders { get; set; }
        IDbSet<T> Set<T>() where T : class;
        DbEntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
