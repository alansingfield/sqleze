using Sqleze;
using Sqleze.Security;
using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;
public static class AccessTokenExtensions
{
    public static ISqlezeBuilder WithAccessToken(
        this ISqlezeBuilder sqlezeConnectionBuilder,
        string? accessToken)
    {
        return sqlezeConnectionBuilder.With<AccessTokenRoot>(
        (root, scope) =>
        {
            scope.Use(new AccessTokenOptions(accessToken));
        });
    }
}
