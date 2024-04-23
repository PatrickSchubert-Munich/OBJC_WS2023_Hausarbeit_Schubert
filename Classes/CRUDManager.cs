namespace Werkzeugverleih.Classes
{
    /// <summary>
    /// Template for classes which sould implements CRUD-Operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CRUDManager<T>
    {
        public abstract void CreateItem();

        public abstract void EditItem(int selectedId, string element, string newContent);

        public abstract void DeleteItem(int categoryIdToRemove);

        public abstract void ShowItem();

        public abstract List<T> GetItem();

    }
}
