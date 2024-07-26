using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DapperCustomerReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DapperCustomerReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<Customer>>> Get()
        {
            var query = new GetCustomerReportQuery();
            var result = await _mediator.Send(query);
            return result;
        }
    }
}
