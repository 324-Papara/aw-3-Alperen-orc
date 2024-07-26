using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.CustomerReportRepository;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.Query;

public class CustomerQueryHandler : 
    IRequestHandler<GetAllCustomerQuery,ApiResponse<List<CustomerResponse>>>,
    IRequestHandler<GetCustomerByIdQuery,ApiResponse<CustomerResponse>>,
    IRequestHandler<GetCustomerByParametersQuery,ApiResponse<List<CustomerResponse>>>,
    IRequestHandler<GetCustomerReportQuery, ApiResponse<List<Customer>>>

{
    private readonly ICustomerReportRepository _customerReportRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CustomerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICustomerReportRepository customerReportRepository)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this._customerReportRepository = customerReportRepository;

    }

    public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        List<Customer> entityList = await unitOfWork.CustomerRepository.GetAll();
        var mappedList = mapper.Map<List<CustomerResponse>>(entityList);
        return new ApiResponse<List<CustomerResponse>>(mappedList);
    }

    public async Task<ApiResponse<CustomerResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CustomerRepository.GetById(request.CustomerId);
        var mapped = mapper.Map<CustomerResponse>(entity);
        return new ApiResponse<CustomerResponse>(mapped);
    }

    public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetCustomerByParametersQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<List<Customer>>> Handle(GetCustomerReportQuery request, CancellationToken cancellationToken)
    {
        var reports = await _customerReportRepository.GetCustomerReportsAsync();
        return new ApiResponse<List<Customer>>(reports.ToList());
    }
}