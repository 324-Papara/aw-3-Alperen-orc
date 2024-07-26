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
    public class CustomerDetailController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerDetailController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<ApiResponse<List<CustomerDetailResponse>>> Get()
        {
            var operation = new GetAllCustomerDetailQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerdetailId}")]
        public async Task<ApiResponse<CustomerDetailResponse>> Get([FromRoute] long customerdetailId)
        {
            var operation = new GetCustomerDetailByIdQuery(customerdetailId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerDetailResponse>> Post([FromBody] CustomerDetailRequest value)
        {
            var operation = new CreateCustomerDetailCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{customerdetailId}")]
        public async Task<ApiResponse> Put(long customerdetailId, [FromBody] CustomerDetailRequest value)
        {
            var operation = new UpdateCustomerDetailCommand(customerdetailId, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{customerdetailId}")]
        public async Task<ApiResponse> Delete(long customerdetailId)
        {
            var operation = new DeleteCustomerDetailCommand(customerdetailId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
