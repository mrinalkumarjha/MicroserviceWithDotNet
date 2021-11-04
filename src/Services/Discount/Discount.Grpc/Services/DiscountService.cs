using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    // DiscountProtoService.DiscountProtoServiceBase this is base class which make this class as service class.
    public class DiscountService: DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(_discountRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
            _mapper = mapper;
        }


        public override async Task<CoupanModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);
            if(coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with productname={request.ProductName} is not found"));
            }
            _logger.LogInformation($"Discount is RS-{coupon.Amount} retrieved for  productname-{request.ProductName} ");
            var coupanModel = _mapper.Map<CoupanModel>(coupon);
            return coupanModel;
        }

        public override async Task<CoupanModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _discountRepository.CreateDiscount(coupon);
            _logger.LogInformation($"Discount is successfully created for  productname-{coupon.ProductName} ");

            var couponModel = _mapper.Map<CoupanModel>(coupon);
            return couponModel;
        }
        public override async Task<CoupanModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _discountRepository.UpdateDiscount(coupon);
            _logger.LogInformation($"Discount is successfully updated for  productname-{coupon.ProductName} ");

            var couponModel = _mapper.Map<CoupanModel>(coupon);
            return couponModel;

        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _discountRepository.DeleteDiscount(request.ProductName);
            var response = new DeleteDiscountResponse
            {
                Success = deleted
            };
            return response;
        }

    }
}
