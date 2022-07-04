namespace RoomBooking.Core.Utils;

internal static class ReflectionExtensionMethods
{
    public static bool CheckIfPropertiesMatch<T, U>(this T obj, U obj2)
    {
        foreach (var requestProp in obj.GetType().GetProperties())
        {
            foreach (var resultProp in obj2.GetType().GetProperties())
            {
                if (requestProp.Name != resultProp.Name)
                    continue;

                if (resultProp.PropertyType == typeof(DateTime))
                {
                    var dateRequestProp = (DateTime)requestProp.GetValue(obj);
                    var dateResultProp = (DateTime)resultProp.GetValue(obj2);

                    if (DateTime.Compare(dateResultProp, dateRequestProp) != 0)
                        return false;

                    continue;
                }

                if (requestProp.GetValue(obj) != resultProp.GetValue(obj2))
                {
                    return false;
                }
            }            
        }

        return true;
    }

    public static bool CheckIfPropertiesMatch<T>(this T obj, T obj2)
    {
        foreach (var requestProp in obj.GetType().GetProperties())
        {
            foreach (var resultProp in obj2.GetType().GetProperties())
            {
                if (requestProp.Name != resultProp.Name)
                    continue;

                if (resultProp.PropertyType == typeof(DateTime))
                {
                    var dateRequestProp = (DateTime)requestProp.GetValue(obj);
                    var dateResultProp = (DateTime)resultProp.GetValue(obj2);

                    if (DateTime.Compare(dateResultProp, dateRequestProp) != 0)
                        return false;

                    continue;
                }

                if (requestProp.GetValue(obj) != resultProp.GetValue(obj2))
                {
                    return false;
                }
            }
        }

        return true;
    }
}
