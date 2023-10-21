﻿namespace Ingresso.Application.Services.Interfaces
{
    public interface IPasswordHasherWrapper
    {
        public bool Verify(string hash, string password, short keySize = 32, int iterations = 10000, char splitChar = '.', string privateKey = "");
        public string Hash(string password, short saltSize = 16, short keySize = 32, int iterations = 10000, char splitChar = '.', string privateKey = "");    
    }
}
