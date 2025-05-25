using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CourseNotesManagement.Application.Features.Auth.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResultDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public LoginCommandHandler(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<LoginResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // User bulma (her user tipinde deniyoruz)
            object? user = await _context.Students.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

            if (user == null)
                user = await _context.Teachers.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

            if (user == null)
                user = await _context.Parents.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

            if (user == null)
                user = await _context.Admins.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

            if (user == null)
                throw new UnauthorizedAccessException("Kullanıcı bulunamadı veya şifre hatalı.");

            // Örnek: User şifre kontrolü (burada düz karşılaştırma, prod'da hash ile kontrol et!)
            string password = (string)user.GetType().GetProperty("Password")?.GetValue(user);
            if (password != request.Password)
                throw new UnauthorizedAccessException("Kullanıcı bulunamadı veya şifre hatalı.");

            // JWT oluşturma
            var userId = (Guid)user.GetType().GetProperty("Id")?.GetValue(user);
            var fullName = ((string)user.GetType().GetProperty("FirstName")?.GetValue(user)) + " " +
                           ((string)user.GetType().GetProperty("LastName")?.GetValue(user));
            var role = user.GetType().Name; // Student/Teacher/Admin/Parent

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(ClaimTypes.Name, fullName),
                new Claim(ClaimTypes.Role, role),
                new Claim("UserId", userId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddDays(6); // 6 gün geçerli

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResultDto
            {
                Token = tokenString,
                Expiration = expires,
                UserId = userId,
                FullName = fullName,
                Role = role
            };
        }
    }
}
