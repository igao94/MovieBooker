using Application.Core;
using Application.Users.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Queries.GetUserById;

public class GetUserByIdHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(request.UserId);

        if (user is null) return Result<UserDto>.Failure("User not found.", 404);

        return Result<UserDto>.Success(mapper.Map<UserDto>(user));
    }
}
