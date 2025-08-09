using AutoMapper;
using DeveloperStore.Application.DTOs;
using DeveloperStore.Application.Queries.Branches;
using DeveloperStore.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeveloperStore.Application.Handlers.Branches
{
    public class GetBranchByIdHandler : IRequestHandler<GetBranchByIdQuery, BranchesDto>
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;

        public GetBranchByIdHandler(IBranchRepository branchRepository, IMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        public async Task<BranchesDto> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.GetByIdAsync(request.Id, cancellationToken);

            if (branch is null)
                throw new KeyNotFoundException($"Branch with ID {request.Id} not found.");

            return _mapper.Map<BranchesDto>(branch);
        }
    }
}