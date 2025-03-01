﻿using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Query;

public class CustomerDetailQueryHandler :
    IRequestHandler<GetAllCustomerDetailQuery, ApiResponse<List<CustomerDetailResponse>>>,
    IRequestHandler<GetCustomerDetailByIdQuery, ApiResponse<CustomerDetailResponse>>,
    IRequestHandler<GetCustomerDetailByParametersQuery, ApiResponse<List<CustomerDetailResponse>>>

{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CustomerDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<CustomerDetailResponse>>> Handle(GetAllCustomerDetailQuery request, CancellationToken cancellationToken)
    {
        List<CustomerDetail> entityList = await unitOfWork.CustomerDetailRepository.GetAll();
        var mappedList = mapper.Map<List<CustomerDetailResponse>>(entityList);
        return new ApiResponse<List<CustomerDetailResponse>>(mappedList);
    }

    public async Task<ApiResponse<CustomerDetailResponse>> Handle(GetCustomerDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CustomerDetailRepository.GetById(request.CustomerdetailId);
        var mapped = mapper.Map<CustomerDetailResponse>(entity);
        return new ApiResponse<CustomerDetailResponse>(mapped);
    }

    public async Task<ApiResponse<List<CustomerDetailResponse>>> Handle(GetCustomerDetailByParametersQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

