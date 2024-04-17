using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace Infrastructure.EF.Entities;

public class UserEntity:IdentityUser<int>
{
}