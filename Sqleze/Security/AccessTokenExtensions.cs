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


    ///// <summary>
    ///// Specify access token
    ///// </summary>
    ///// <param name="sqlezeConnectionBuilder"></param>
    ///// <param name="accessToken"></param>
    ///// <returns></returns>
    //public static ISqlezeBuilder WithAccessToken(
    //    this ISqleze sqleze,
    //    string accessToken
    //    )
    //{
    //    return sqleze.Reconfigure().WithAccessToken(accessToken);
    //}

}
