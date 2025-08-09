using DeveloperStore.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace DeveloperStore.Application.Queries.Branches
{
    public class GetBranchesQuery : IRequest<List<BranchesDto>>
    {
        
    }
}