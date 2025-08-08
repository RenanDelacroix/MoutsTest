using DeveloperStore.Application.DTOs;
using MediatR;
using System;

namespace DeveloperStore.Application.Queries.Branches
{
    public class GetBranchByIdQuery : IRequest<BranchesDto>
    {
        public Guid Id { get; }

        public GetBranchByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}