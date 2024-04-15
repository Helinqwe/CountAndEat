﻿namespace CoundAndEat.Api.Entities
{
    public class TokenInfo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
