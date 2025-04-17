using System;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context,ITokenService tokenService):BaseApiController
{
    [HttpPost("register")] //account/register
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if(await UserExists(registerDto.Username))
            return BadRequest("Username is taken");
            
        using var hmac= new HMACSHA512();

        var user=new AppUser
        {
            UserName=registerDto.Username.ToLower(),
            PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt=hmac.Key
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return new UserDto
        {
            UserName=user.UserName,
            Token=tokenService.CreateToken(user)
        };
    }   

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user=await context.Users.FirstOrDefaultAsync(u=>u.UserName== loginDto.UserName.ToLower());
        if(user==null)
            return Unauthorized("Invalid username or password");
        
        using var hmac= new HMACSHA512(user.PasswordSalt);
        var computedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if(computedHash[i] != user.PasswordHash[i])
                return Unauthorized("Invalid username or password");
        }

        return new UserDto
        {
            UserName=user.UserName,
            Token=tokenService.CreateToken(user)
        };
    }

    private async Task<bool> UserExists(string UserName)
    {
        return await context.Users.AnyAsync(x=> x.UserName.ToLower()==UserName.ToLower());
    }
}
