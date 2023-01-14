﻿namespace BlackList.Application.Services;

using AutoMapper;
using BlackList.Application.Abstractions;
using BlackList.Application.Dtos;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly Random _random;
    private const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public UserService(IUserRepository repository, Random random, IMapper mapper, IDateTimeProvider dateTimeProvider)
    {
        _repository = repository;
        _random = random;
        _mapper = mapper;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<UserDto> CreateUserAsync(CancellationToken cancellationToken)
    {
        int num = _random.Next();

        var randomString = new string(Enumerable.Repeat(chars, 20)
            .Select(s => s[_random.Next(s.Length)])
            .ToArray());

        var timestamp = _dateTimeProvider.UtcNow.ToUnixTimeMilliseconds();

        var plainTextBytes = Encoding.UTF8.GetBytes(randomString + timestamp);

        var token = Convert.ToBase64String(plainTextBytes);

        var user = await _repository.CreateUserAsync(token, cancellationToken);

        return _mapper.Map<UserDto>(user);
    }
}
