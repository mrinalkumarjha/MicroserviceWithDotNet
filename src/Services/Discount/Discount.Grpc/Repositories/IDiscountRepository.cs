﻿using Discount.Grpc.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productName);

        Task<bool> CreateDiscount(Coupon counpan);

        Task<bool> UpdateDiscount(Coupon counpan);

        Task<bool> DeleteDiscount(string productName);
    }
}
