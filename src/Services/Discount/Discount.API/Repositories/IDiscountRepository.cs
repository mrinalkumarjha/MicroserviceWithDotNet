using Discount.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupan> GetDiscount(string productName);

        Task<bool> CreateDiscount(Coupan counpan);

        Task<bool> UpdateDiscount(Coupan counpan);

        Task<bool> DeleteDiscount(string productName);
    }
}
