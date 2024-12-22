﻿using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Repositories;

public interface IUserRepository
{
    Task Add(User user);
    Task<User?> GetByEmail(string email);
}