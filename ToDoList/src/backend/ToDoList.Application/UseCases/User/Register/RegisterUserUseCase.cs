﻿using AutoMapper;
using ToDoList.Communication.Requests;
using ToDoList.Communication.Responses;
using ToDoList.Domain.Repositories;
using ToDoList.Domain.Security.Tokens;
using ToDoList.Exception.ExceptionsBase;

namespace ToDoList.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public RegisterUserUseCase(
        IUserRepository userRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<RegisterUserResponse> Execute(RegisterUserRequest request)
    {
        Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        
        await _userRepository.Add(user);
        await _unitOfWork.Commit();

        var token = _accessTokenGenerator.Generate(user.Identifier);

        return new RegisterUserResponse()
        {
            Name = user.Name,
            Tokens = new TokensResponse()
            {
                AccessToken = token
            }
        };
    }

    private static void Validate(RegisterUserRequest request)
    {
        var result = new RegisterUserValidator().Validate(request);

        if (!result.IsValid)
            throw new ErrorOnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}