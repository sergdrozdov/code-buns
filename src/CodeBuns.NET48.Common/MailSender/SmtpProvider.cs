﻿namespace CodeBuns.NET48.Common
{
    public class SmtpProvider
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Enabled { get; set; } = true;
    }
}
