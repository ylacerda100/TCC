﻿using Microsoft.AspNetCore.Identity;
using NetDevPack.Domain;

namespace TCC.Domain.Models
{
    public class Usuario : IdentityUser, IAggregateRoot
    {
        public string Nome { get; set; }
    }
}
