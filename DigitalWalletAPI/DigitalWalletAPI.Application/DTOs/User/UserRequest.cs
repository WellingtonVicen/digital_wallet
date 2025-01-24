namespace DigitalWalletAPI.Application.DTOs.User
{
    public class CreateUserRequest
    {
        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Endereço de e-mail do usuário (deve ser único).
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário.
        /// </summary>
        public string Password { get; set; }
    }
}
