using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventHub.Tec.Avanzadas.Controllers
{
    /// <summary>
    /// Controlador encargado de la Autenticación de Usuarios en el sistema.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Inicia sesión y genera un token JWT válido por una hora.
        /// </summary>
        /// <param name="request">Objeto que contiene las credenciales del usuario (Usuario y Contraseña).</param>
        /// <returns>
        /// Devuelve un objeto JSON con el token y la fecha de expiración si las credenciales son correctas.  
        /// Retorna <c>401 Unauthorized</c> si las credenciales no son válidas.
        /// </returns>
        /// <response code="200">Autenticación exitosa. Se devuelve el token JWT.</response>
        /// <response code="401">Credenciales inválidas.</response>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(TokenResponse), 200)]
        [ProducesResponseType(401)]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Validación simple (en una aplicación real se verifica en la base de datos)
            if (request.Usuario == "admin" && request.Password == "1234")
            {
                var key = Encoding.ASCII.GetBytes("clave-super-secreta-eventhub-2025-tecavanzadas");

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, request.Usuario)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new
                {
                    token = tokenString,
                    expiraEn = tokenDescriptor.Expires
                });
            }

            return Unauthorized(new { mensaje = "Credenciales inválidas" });
        }
    }

    /// <summary>
    /// Representa las credenciales de inicio de sesión.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Nombre de usuario o identificador de acceso.
        /// </summary>
        public string Usuario { get; set; }

        /// <summary>
        /// Contraseña del usuario.
        /// </summary>
        public string Password { get; set; }
    }

    /// <summary>
    /// Respuesta generada al autenticarse correctamente.
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// Token JWT generado tras la autenticación.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Fecha y hora (UTC) en la que expira el token.
        /// </summary>
        public DateTime? ExpiraEn { get; set; }
    }
}
