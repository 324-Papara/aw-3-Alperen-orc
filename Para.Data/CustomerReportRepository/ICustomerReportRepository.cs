using Para.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Data.CustomerReportRepository
{
    public interface ICustomerReportRepository
    {
        Task<IEnumerable<Customer>> GetCustomerReportsAsync();

    }
}
