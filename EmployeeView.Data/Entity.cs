namespace EmployeeViewer.Data
{
    public abstract class Entity
    {
        protected int id = 0;
        public int Id
        {
            get => id;
            set
            {
                if (id > 0) return; // throw new InvalidOperationException("Id is already defined");
                id = value;
            }
        }
    }
}
