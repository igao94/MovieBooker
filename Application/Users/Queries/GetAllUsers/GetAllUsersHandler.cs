using Application.Core;
using Application.Users.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Queries.GetAllUsers;

public class GetAllUsersHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllUsersQuery, Result<IReadOnlyList<UserDto>>>
{
    public async Task<Result<IReadOnlyList<UserDto>>> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var users = await unitOfWork.UserRepository.GetAllAsync(request.UserParams.Search);

        return Result<IReadOnlyList<UserDto>>.Success(mapper.Map<IReadOnlyList<UserDto>>(users));
    }
}
