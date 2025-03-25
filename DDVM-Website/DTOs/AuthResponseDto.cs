namespace DDVM_Website.DTOs
{
    public class AuthResponseDto
    {
        public string UserId { get; set; }    // User's unique ID
        public string Email { get; set; }     // Email of the user
        public string UserName { get; set; }  // Username of the user
        public string Token { get; set; }     // JWT token (if using authentication)
        public List<string> Roles { get; set; } = new();  // User roles (Admin, Teacher, etc.)
        public bool IsSuccess { get; set; }   // Indicates success/failure
        public string Message { get; set; }   // Any error/success message

        public AuthResponseDto()
        {
            Roles = new List<string>();
        }
    }
}