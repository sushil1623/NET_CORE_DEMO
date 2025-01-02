using DemoAPI.DAL;
using DemoAPI.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RefreshTokenDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(RefreshTokenContext context, IConfiguration configuration) : ControllerBase
    {
        private RefreshTokenContext _context = context;
        private IConfiguration _configuration = configuration;


        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser([FromBody] UserModel user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("User has been created successfully");
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] UserAuthenticateModel user)
        {
            var userDetails = _context.Users.Where(x => x.UserId == user.UserId
            && x.Password == user.Password).FirstOrDefault();
            if (userDetails is not null)
            {

                return Ok(new LoginResponse
                {
                    AccessToken = GetAccessToken(userDetails),
                    RefreshToken = GetRefreshToken(userDetails)
                });
            }
            else
            {
                return BadRequest("User id or password is invalid");
            }
        }

        [HttpPost]
        [Route("RefreshToken")]
        public IActionResult RefreshToken([FromBody] LoginResponse refreshToken)
        {
            var refreshTokens = _context.RefreshTokens.Where(x => x.RefreshToken == refreshToken.RefreshToken).FirstOrDefault();
            if (refreshTokens is not null)
            {
                var userDetails = _context.Users.Where(x => x.UserId == refreshTokens.UserId).FirstOrDefault();
                if (userDetails is not null)
                {

                    return Ok(new LoginResponse
                    {
                        AccessToken = GetAccessToken(userDetails),
                        RefreshToken = GetRefreshToken(userDetails)
                    });
                }
                else
                {
                    return BadRequest("User id or password is invalid");
                }
            }
            else
            {
                return BadRequest("Refresh Token is not valid");
            }
        }

        private string GetRefreshToken(UserModel user)
        {
            var randomBytes = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
                string refreshToken = Convert.ToBase64String(randomBytes);
                _context.RefreshTokens.Add(new RefreshTokenModel
                {
                    RefreshToken = refreshToken,
                    UserId = user.UserId
                });
                _context.SaveChanges();
                return refreshToken;
            }
        }

        private string GetAccessToken(UserModel userDetails)
        {
            var jwtSecurity = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurity.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                [
                    new(ClaimTypes.Email,userDetails.EmailId.ToString()),
                        new(ClaimTypes.NameIdentifier,userDetails.UserId.ToString())
                ]),
                Issuer = _configuration["JWTSettings:Issuer"],
                Audience = _configuration["JWTSettings:Audience"],
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(
                     new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:Key"])), SecurityAlgorithms.HmacSha512),
            });
            string accessToken = jwtSecurity.WriteToken(securityToken);
            return accessToken;
        }

        [HttpPost]
        [Authorize]
        [Route("AddProduct")]
        public IActionResult AddProduct()
        {
            var products = new List<ProductModel>()
            {
                new ProductModel{ ProductId=1, ProductName="Product 1", ProductDescription="Product Description 1"},
                new ProductModel{ ProductId=2, ProductName="Product 2", ProductDescription="Product Description 2"},
                new ProductModel{ ProductId=3, ProductName="Product 3", ProductDescription="Product Description 3"},
            };
            _context.Products.AddRange(products);
            _context.SaveChanges();
            return Ok("Product has been addedd successfully");
        }

        [HttpGet]
        [Authorize]
        [Route("GetProducts")]
        public IActionResult GetProduct()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

    }
}

