using Dapper;
using Microsoft.EntityFrameworkCore;
using Para.Data.Context;
using Para.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Data.CustomerReportRepository
{
    public class CustomerReportRepository : ICustomerReportRepository
    {
        private readonly ParaDbContext _dbContext;

        public CustomerReportRepository(ParaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Customer>> GetCustomerReportsAsync()
        {
            using (var connection = _dbContext.Database.GetDbConnection())
            {
                var sql = @"
                SELECT c.Id, c.FirstName, c.LastName, c.IdentityNumber, c.Email, c.CustomerNumber, c.DateOfBirth,
                       cd.Id, cd.CustomerId, cd.FatherName, cd.MotherName, cd.EducationStatus, cd.MontlyIncome, cd.Occupation,
                       ca.Id, ca.CustomerId, ca.Country, ca.City, ca.AddressLine, ca.ZipCode, ca.IsDefault,
                       cp.Id, cp.CustomerId, cp.CountyCode, cp.Phone, cp.IsDefault
                FROM Customer c
                LEFT JOIN CustomerDetail cd ON c.Id = cd.CustomerId
                LEFT JOIN CustomerAddress ca ON c.Id = ca.CustomerId
                LEFT JOIN CustomerPhone cp ON c.Id = cp.CustomerId";

                var customerDictionary = new Dictionary<long, Customer>();

                var customers = await connection.QueryAsync<Customer, CustomerDetail, CustomerAddress, CustomerPhone, Customer>(
                    sql,
                    (customer, detail, address, phone) =>
                    {
                        if (!customerDictionary.TryGetValue(customer.Id, out var currentCustomer))
                        {
                            currentCustomer = customer;
                            currentCustomer.CustomerAddresses = new List<CustomerAddress>();
                            currentCustomer.CustomerPhones = new List<CustomerPhone>();
                            customerDictionary.Add(currentCustomer.Id, currentCustomer);
                        }

                        currentCustomer.CustomerDetail = detail;

                        if (address != null)
                        {
                            currentCustomer.CustomerAddresses.Add(address);
                        }

                        if (phone != null)
                        {
                            currentCustomer.CustomerPhones.Add(phone);
                        }

                        return currentCustomer;
                    },
                    splitOn: "Id,CustomerId,Id,CustomerId,Id,CustomerId"
                );

                return customers.Distinct().ToList();
            }
        }
    }
}
