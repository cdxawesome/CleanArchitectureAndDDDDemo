using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuberDinner.Infrastructure.Authentication
{
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        // init 表示这个属性只能在初始化时赋值，之后不能再修改
        // null!表示，这个属性是故意赋值的null，不要警告
        public string Secret { get; init; } = null!;
        public string Issuer { get; init; } = null!;
        public string Audience { get; init; } = null!;
        public int ExpirationInMinutes { get; init; }
    }
}