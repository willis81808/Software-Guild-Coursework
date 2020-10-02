using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using System.Web;

public static class ExtensionMethods
{
    public static void Clear<T>(this DbSet<T> dbSet) where T : class
    {
        dbSet.RemoveRange(dbSet);
    }

    public static T ReturnMax<T>(this IList<T> collection, Func<T, decimal> comparator)
    {
        T result = default(T);
        decimal largest = decimal.MinValue;
        foreach (var item in collection)
        {
            if (comparator(item) > largest)
                result = item;
        }
        return result;
    }

    public static bool ContainsSubstring(this string str, string compareValue, int charsToCompare)
    {
        var subString = compareValue.Substring(0, Math.Min(charsToCompare, compareValue.Length));
        if (str.Contains(subString))
        {
            return true;
        }
        else if (compareValue.Length > charsToCompare)
        {
            return str.ContainsSubstring(compareValue.Substring(1), charsToCompare);
        }
        return false;
    }

    public static string GetFirstName(this IIdentity identity)
    {
        if (identity == null)
        {
            throw new ArgumentNullException("identity");
        }
        var ci = identity as ClaimsIdentity;
        if (ci != null)
        {
            return ci.FindFirstValue("FirstName");
        }
        return null;
    }
    public static string GetLastName(this IIdentity identity)
    {
        if (identity == null)
        {
            throw new ArgumentNullException("identity");
        }
        var ci = identity as ClaimsIdentity;
        if (ci != null)
        {
            return ci.FindFirstValue("LastName");
        }
        return null;
    }
}