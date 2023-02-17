using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;

namespace Sqleze.Util;

// from https://alistairevans.co.uk/2020/11/01/detecting-init-only-properties-with-reflection-in-c-9/

public static class PropertyExtensions
{
    /// <summary>
    /// Determines if this property is marked as init-only.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns>True if the property is init-only, false otherwise.</returns>
    public static bool IsInitOnly(this PropertyInfo property)
    {
        if(!property.CanWrite)
            return false;

        var setMethod = property.SetMethod;

        if(setMethod == null)
            return false;

        // Get the modifiers applied to the return parameter.
        var setMethodReturnParameterModifiers = setMethod.ReturnParameter.GetRequiredCustomModifiers();

        // Init-only properties are marked with the IsExternalInit type.
        return setMethodReturnParameterModifiers.Contains(typeof(System.Runtime.CompilerServices.IsExternalInit));
    }
}