﻿namespace Application.Account.DTOs;

public class AccountDto
{
    public string Id { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Token { get; set; }
    public string? PictureUrl { get; set; }
}
