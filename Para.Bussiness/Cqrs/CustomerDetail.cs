using MediatR;
using Para.Base.Response;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public record CreateCustomerDetailCommand(CustomerDetailRequest Request) : IRequest<ApiResponse<CustomerDetailResponse>>;
public record UpdateCustomerDetailCommand(long CustomerdetailId, CustomerDetailRequest Request) : IRequest<ApiResponse>;
public record DeleteCustomerDetailCommand(long CustomerdetailId) : IRequest<ApiResponse>;

public record GetAllCustomerDetailQuery() : IRequest<ApiResponse<List<CustomerDetailResponse>>>;
public record GetCustomerDetailByIdQuery(long CustomerdetailId) : IRequest<ApiResponse<CustomerDetailResponse>>;
public record GetCustomerDetailByParametersQuery(long CustomerdetailId, string Name, string IdentityNumber) : IRequest<ApiResponse<List<CustomerDetailResponse>>>;
