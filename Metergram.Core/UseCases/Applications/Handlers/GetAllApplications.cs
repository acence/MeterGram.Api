using MediatR;
using MeterGram.Core.Models;
using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Interfaces.Database;

namespace MeterGram.Core.UseCases.Applications.Handlers;

public class GetAllApplications : IRequestHandler<GetAllApplications.Query, PagedResult<CompanyApplication>>
{
    private readonly ICompanyApplicationRepository _companyApplicationRepository;

    public GetAllApplications(ICompanyApplicationRepository companyApplicationRepository)
    {
        ArgumentNullException.ThrowIfNull(companyApplicationRepository);

        _companyApplicationRepository = companyApplicationRepository;
    }
    public async Task<PagedResult<CompanyApplication>> Handle(Query request, CancellationToken cancellationToken)
    {
        var result = await _companyApplicationRepository.GetAllAsync(request.PageNumber, request.PageSize, request.Name, request.Email, request.Phone, cancellationToken);
        return new PagedResult<CompanyApplication>
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalItems = result.TotalItems,
            Data = result.Data
        };
    }

    public class Query : IRequest<PagedResult<CompanyApplication>>
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 100;
    }
}