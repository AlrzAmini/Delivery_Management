namespace DriversManagement.Utilities.ExtensionMethods
{
    public static class CollectionExtensions
    {
        public static bool CollectionIsNullOrEmpty<T>(this ICollection<T>? collection)
        {
            if (collection is null || collection.Count == 0)
            {
                return true;
            }

            return false;
        }
    }
}
