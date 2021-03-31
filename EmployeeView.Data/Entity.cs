using System;

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
                if (id > 0) throw new InvalidOperationException("Id is already defined");
                id = value > 0 ? value : throw new ArgumentOutOfRangeException("Invalid Id value");
            }
        }
    }
}
