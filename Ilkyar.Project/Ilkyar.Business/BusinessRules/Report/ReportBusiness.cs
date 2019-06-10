using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Entities.Enums;
using Ilkyar.Contracts.ServiceContracts.Report;
using Ilkyar.Contracts.Services;
using Ilkyar.Contracts.UnitOfWork;
using System;
using System.Linq;
using Ilkyar.Contracts.Repositories;
using System.Collections.Generic;
using System.Linq.Expressions;
using Ilkyar.Business.System.Extentions;

namespace Ilkyar.Business.BusinessRules.Report
{
    public class ReportBusiness : IReport
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Contracts.Entities.EF.Report> _reportRepository;

        public ReportBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _reportRepository = _unitOfWork.GetRepository<Contracts.Entities.EF.Report>();
        }

        public ServiceResult<long> CreateNewReport(CreateNewReportDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            long result = -1;
            try
            {
                var newReport = new Contracts.Entities.EF.Report
                {
                    YonDerId = model.YonDerId,
                    ScholarshipHolderId = model.ScholarshipHolderId,
                    Subject = model.Subject,
                    ReportDate = model.ReportDate,
                    ReportText = model.ReportText
                };

                var reportResult = _reportRepository.Add(newReport);
                _unitOfWork.SaveChanges();

                result = reportResult.Id;
                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<long> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<List<ReportDTO>> GetReportList(ReportFilterDTO model)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            List<ReportDTO> result = null;
            try
            {
                Expression<Func<Contracts.Entities.EF.Report, bool>> expReport = p => true;

                if (model.YonDerId != null)
                {
                    expReport = expReport.And(p => p.YonDerId == model.YonDerId);
                }

                if (model.ScholarshipHolderId != null)
                {
                    expReport = expReport.And(p => p.ScholarshipHolderId == model.ScholarshipHolderId);
                }

                if (!string.IsNullOrEmpty(model.Subject))
                {
                    expReport = expReport.And(p => p.Subject.Contains(model.Subject));
                }

                if (model.ReportDate != null)
                {
                    expReport = expReport.And(p => p.ReportDate == model.ReportDate);
                }

                if (!string.IsNullOrEmpty(model.ReportText))
                {
                    expReport = expReport.And(p => p.Subject.Contains(model.ReportText));
                }

                var reportList = _reportRepository.Entities.Where(expReport).ToList();

                result = reportList.Select(p => new ReportDTO
                {
                    Id = p.Id,
                    YonDerId = p.YonDerId,
                    YonDerName = $"{p.YonDer.FirstName} {p.YonDer.LastName}",
                    ScholarshipHolderId = p.ScholarshipHolderId,
                    ScholarshipHolderName = $"{p.ScholarshipHolder.FirstName} {p.ScholarshipHolder.LastName}",
                    Subject = p.Subject,
                    ReportDate = p.ReportDate,
                    ReportText = p.ReportText,
                }).ToList();

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<List<ReportDTO>> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }

        public ServiceResult<ReportDTO> GetReport(long reportId)
        {
            string errorMessage = string.Empty;
            EnumServiceResultType serviceResultType = EnumServiceResultType.Unspecified;
            ReportDTO result = null;
            try
            {
                var existingReport = _reportRepository.Entities.Where(p => p.Id == reportId).SingleOrDefault();

                if (existingReport == null)
                    throw new Exception("Rapor bulunamadı.");

                //if (existingReport.YonDer == null)
                //    throw new Exception("Rapora ait YönDer bilgisi bulunamadı.");

                //if (existingReport.ScholarshipHolder == null)
                //    throw new Exception("Rapora ait bursiyer bilgisi bulunamadı.");

                result = new ReportDTO
                {
                    Id = existingReport.Id,
                    YonDerId = existingReport.YonDerId,
                    YonDerName = $"{existingReport.YonDer.FirstName} {existingReport.YonDer.LastName}",
                    ScholarshipHolderId = existingReport.ScholarshipHolderId,
                    ScholarshipHolderName = $"{existingReport.ScholarshipHolder.FirstName} {existingReport.ScholarshipHolder.LastName}",
                    Subject = existingReport.Subject,
                    ReportDate = existingReport.ReportDate,
                    ReportText = existingReport.ReportText
                };

                serviceResultType = EnumServiceResultType.Success;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                serviceResultType = EnumServiceResultType.Error;
            }
            return new ServiceResult<ReportDTO> { ErrorMessage = errorMessage, Result = result, ServiceResultType = serviceResultType };
        }
    }
}
