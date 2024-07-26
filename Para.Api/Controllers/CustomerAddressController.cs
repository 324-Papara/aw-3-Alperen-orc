using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerAddressController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<ApiResponse<List<CustomerAddressResponse>>> Get()
        {
            var operation = new GetAllCustomerAddressQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{customeraddressId}")]
        public async Task<ApiResponse<CustomerAddressResponse>> Get([FromRoute] long customeraddressId)
        {
            var operation = new GetCustomerAddressByIdQuery(customeraddressId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerAddressResponse>> Post([FromBody] CustomerAddressRequest value)
        {
            var operation = new CreateCustomerAddressCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{customeraddressId}")]
        public async Task<ApiResponse> Put(long customeraddressId, [FromBody] CustomerAddressRequest value)
        {
            var operation = new UpdateCustomerAddressCommand(customeraddressId, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{customeraddressId}")]
        public async Task<ApiResponse> Delete(long customeraddressId)
        {
            var operation = new DeleteCustomerAddressCommand(customeraddressId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
