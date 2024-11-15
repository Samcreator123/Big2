namespace Big2.Controller.Extension;

public static class GenericTypeExtensions
{
    // 因為要對物件直接使用，所以有這一層
    public static string GetGenericTypeName(this object obj)
    {
        return obj.GetType().GetGenericTypeName();
    }


    /// <summary>
    /// 不支援層層遞進.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static string GetGenericTypeName(this Type type)
    {
        if (type.IsGenericType)
        {
            var genericArguments = string.Join(",", type.GetGenericArguments().Select(o => o.Name).ToArray());
            return $"{type.Name.Remove('`')}<{genericArguments}>";
        }
        else
        { 
            return type.Name;
        }
    }

    
}
