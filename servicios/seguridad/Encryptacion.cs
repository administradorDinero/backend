using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.seguridad
{
    internal class Encryptacion
    {

            public static string HashPassword(string password)
            {
                // Crea un hash bcrypt para la contraseña
                return BCrypt.Net.BCrypt.HashPassword(password);
            }

            public static bool VerifyPassword(string password, string hashedPassword)
            {
                // Verifica si la contraseña coincide con el hash
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
        }
    }

