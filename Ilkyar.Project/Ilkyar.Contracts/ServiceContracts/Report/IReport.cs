using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Services;
using System.Collections.Generic;

namespace Ilkyar.Contracts.ServiceContracts.Report
{
    public interface IReport
    {
        ServiceResult<long> CreateNewReport(CreateNewReportDTO model);
        ServiceResult<List<ReportDTO>> GetReportList(ReportFilterDTO filter);
        ServiceResult<ReportDTO> GetReport(long reportId);
    }
}
