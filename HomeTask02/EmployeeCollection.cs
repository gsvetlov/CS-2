
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTask02
{
    class EmployeeCollection : IEnumerable<BaseEmployee>
    {
        private readonly List<BaseEmployee> employees;

        public EmployeeCollection()
        {
            employees = new List<BaseEmployee>();
        }
        public EmployeeCollection(IEnumerable<BaseEmployee> collection) : this()
        {
            employees.AddRange(collection);
        }
        public BaseEmployee this[int index]
        {
            get => IndexIsValid(index) ? employees[index] : throw new IndexOutOfRangeException(nameof(index));
            set => employees[index] = IndexIsValid(index) ? value : throw new IndexOutOfRangeException(nameof(index));
        }

        private bool IndexIsValid(int index) => index < employees.Count + 1 && index > -1;

        public int Count => employees.Count;
        public void Add(BaseEmployee person) => employees.Add(person);
        public void Remove(BaseEmployee person) => employees.Remove(person);
        public bool Contains(BaseEmployee person) => employees.Contains(person);

        public IEnumerator<BaseEmployee> GetEnumerator() => new EmployeeEnumerator<BaseEmployee>(this);
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private class EmployeeEnumerator<T> : IEnumerator<BaseEmployee>
        {
            private readonly EmployeeCollection collection;
            private int index;
            public EmployeeEnumerator(EmployeeCollection collection)
            {
                this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
                index = -1;
            }
            public BaseEmployee Current => index > -1 ? collection[index] : default;
            public void Dispose() { }
            public bool MoveNext() => ++index < collection.Count;
            public void Reset() => index = -1;

            object IEnumerator.Current => Current;
        }
    }
}
