﻿using System;

namespace InnoShop.Users.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(Guid userId, string email);
}
