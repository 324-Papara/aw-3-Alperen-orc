using MediatR;
using Para.Base.Response;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public record CreateCustomerAddressCommand(CustomerAddressRequest Request) : IRequest<ApiResponse<CustomerAddressResponse>>;
public record UpdateCustomerAddressCommand(long CustomeraddressId, CustomerAddressRequest Request) : IRequest<ApiResponse>;
public record DeleteCustomerAddressCommand(long CustomeraddressId) : IRequest<ApiResponse>;

public record GetAllCustomerAddressQuery() : IRequest<ApiResponse<List<CustomerAddressResponse>>>;
public record GetCustomerAddressByIdQuery(long CustomeraddressId) : IRequest<ApiResponse<CustomerAddressResponse>>;
public record GetCustomerAddressByParametersQuery(long CustomeraddressId, string Name, string IdentityNumber) : IRequest<ApiResponse<List<CustomerAddressResponse>>>;
