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
    public class CustomerPhoneController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerPhoneController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<ApiResponse<List<CustomerPhoneResponse>>> Get()
        {
            var operation = new GetAllCustomerPhoneQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerphoneId}")]
        public async Task<ApiResponse<CustomerPhoneResponse>> Get([FromRoute] long customerphoneId)
        {
            var operation = new GetCustomerPhoneByIdQuery(customerphoneId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerPhoneResponse>> Post([FromBody] CustomerPhoneRequest value)
        {
            var operation = new CreateCustomerPhoneCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{customerphoneId}")]
        public async Task<ApiResponse> Put(long customerphoneId, [FromBody] CustomerPhoneRequest value)
        {
            var operation = new UpdateCustomerPhoneCommand(customerphoneId, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{customerphoneId}")]
        public async Task<ApiResponse> Delete(long customerphoneId)
        {
            var operation = new DeleteCustomerPhoneCommand(customerphoneId);
            var result = await mediator.Send(operation);
            return result;
        }
    }

}
