using Booking.Core.Domain.RepositoryContracts;
using Booking.Core.DTO;
using Booking.Core.Helpers.EntitesExtensions;
using Booking.Core.ServicesContract;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CompanyService> _logger;
        private readonly UploadImageService _uploadImageService;

        public CompanyService(IUnitOfWork unitOfWork, ILogger<CompanyService> logger, UploadImageService uploadImageService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _uploadImageService = uploadImageService;
        }
        public async Task CreateAsync(CompanyDTO companyDTO)
        {
            Company company = CompanyDTO.ToCompany(companyDTO);
            try
            {
                if (companyDTO.ImageFile != null)
                {
                    if (companyDTO.ImageFile.ContentType.StartsWith("image/"))
                        company.Image = await _uploadImageService.UploadFileAsync(companyDTO.ImageFile);
                }

                await _unitOfWork.Companies.Add(company);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var company = await _unitOfWork.Companies.GetById(id);
                if (company != null)
                {
                    company.IsDeleted = true;
                    _unitOfWork.Complete();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task<IEnumerable<CompanyDTO>> GetAll()
        {
            try
            {
                var companies = await _unitOfWork.Companies.GetAll();
                var CompaniesDto = companies.Select(c => c.ToCustomerDTO()).ToList();
                return CompaniesDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Enumerable.Empty<CompanyDTO>();
            }
        }

        public async Task<CompanyDTO> GetCompanyById(Guid id)
        {
            try
            {
                var company = await _unitOfWork.Companies.GetById(id);
                CompanyDTO companyDTO = company.ToCustomerDTO();
                return companyDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new CompanyDTO();
            }
        }

        public async Task UpdateAsync(CompanyDTO companyDTO)
        {
            Company company = CompanyDTO.ToCompany(companyDTO);
            Company companyDataBase = await _unitOfWork.Companies.GetById(companyDTO.Id);
            try
            {
                if (companyDTO.ImageFile != null)
                {
                    if (companyDTO.ImageFile.ContentType.StartsWith("image/"))
                    {
                        company.Image = await _uploadImageService.UploadFileAsync(companyDTO.ImageFile);
                        if(companyDataBase.Image != null)
                            await _uploadImageService.DeleteFileAsync(companyDataBase.Image);
                    }
                }

                await _unitOfWork.Companies.Add(company);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
