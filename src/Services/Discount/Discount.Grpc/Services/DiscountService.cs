using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbcontext, ILogger<DiscountService> logger) 
    : DiscounPrototService.DiscounPrototServiceBase
{
    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status( StatusCode.InvalidArgument,"Invalid Request Object"));
        await dbcontext.AddAsync(coupon);
        await dbcontext.SaveChangesAsync();
        logger.LogInformation("Disount is successfully created ProductName : {ProductName}", coupon.ProductName);
        return coupon.Adapt<CouponModel>();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbcontext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount Is Not Found = {request.ProductName}"));
        dbcontext.Coupons.Remove(coupon);
        await dbcontext.SaveChangesAsync();
        logger.LogInformation("Discount is Successfully Deleted , Product Name = {0}",request.ProductName);
        return new() { Success = true};
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon =await dbcontext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
        if (coupon is null) 
            coupon = new() { ProductName = "No Discount",Amount = 0,Description = "No Discount Decs" };
        logger.LogInformation("Discount is retrieved for ProductName : {ProductName}, Amount : {Amount}", coupon.ProductName, coupon.Amount);
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;

    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request Object"));
         dbcontext.Update(coupon);
        await dbcontext.SaveChangesAsync();
        logger.LogInformation("Disount is successfully Update ProductName : {ProductName}", coupon.ProductName);
        return coupon.Adapt<CouponModel>();
    }
}
