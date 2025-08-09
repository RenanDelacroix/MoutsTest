using AutoMapper;
using DeveloperStore.Application.DTOs;
using DeveloperStore.Application.Queries.Branches;
using DeveloperStore.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DeveloperStore.Application.Handlers.Branches
{
    public class GetBranchesHandler : IRequestHandler<GetBranchesQuery, List<BranchesDto>>
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;

        public GetBranchesHandler(IBranchRepository branchRepository, IMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        public async Task<List<BranchesDto>> Handle(GetBranchesQuery request, CancellationToken cancellationToken)
        {
            var branches = await _branchRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<BranchesDto>>(branches);
        }
    }
}