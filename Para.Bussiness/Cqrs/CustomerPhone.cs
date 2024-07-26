using MediatR;
using Para.Base.Response;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public record CreateCustomerPhoneCommand(CustomerPhoneRequest Request) : IRequest<ApiResponse<CustomerPhoneResponse>>;
public record UpdateCustomerPhoneCommand(long CustomerphoneId, CustomerPhoneRequest Request) : IRequest<ApiResponse>;
public record DeleteCustomerPhoneCommand(long CustomerphoneId) : IRequest<ApiResponse>;

public record GetAllCustomerPhoneQuery() : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;
public record GetCustomerPhoneByIdQuery(long CustomerphoneId) : IRequest<ApiResponse<CustomerPhoneResponse>>;
public record GetCustomerPhoneByParametersQuery(long CustomerphoneId, string Name, string IdentityNumber) : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;
