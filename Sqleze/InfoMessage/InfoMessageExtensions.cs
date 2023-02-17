using Sqleze.InfoMessage;
using Sqleze;
using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;
public static class InfoMessageExtensions
{
    public static ISqlezeBuilder WithInfoMessagesTo(
        this ISqlezeBuilder sqlezeConnectionBuilder,
        Action<MS.SqlInfoMessageEventArgs> action)
    {
        return sqlezeConnectionBuilder.With<InfoMessageRoot>(
        (root, scope) =>
        {
            // Chain our action onto any previous one.
            Action<MS.SqlInfoMessageEventArgs> newAction = x =>
            {
                var parentAction = root.ParentOptions.InfoMessageAction;

                if(parentAction != null)
                    parentAction(x);

                action(x);
            };

            scope.Use(new InfoMessageActionOptions(newAction));
        });
    }
}
