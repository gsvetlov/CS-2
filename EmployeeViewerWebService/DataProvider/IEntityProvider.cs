using System.Collections.Generic;

using EmployeeViewer.Data;

namespace EmployeeViewerWebService.DataProvider
{
    public interface IEntityProvider
    {
        int Add(Employee employee); // возвращает Id записи или -1, если запись не прошла
        int Remove(Employee employee); // возвращает количество обработанных записей
        int Update(Employee employee); // возвращает количество обработанных записей

        int Add(Department department); // возвращает Id записи или -1, если запись не прошла
        int Remove(Department department); // возвращает количество обработанных записей
        int Update(Department department); // возвращает количество обработанных записей

        List<Department> GetDepartments(); // колекция всех отделов
        List<Employee> GetEmployees(); // коллекция всех сотрудников

        Employee GetEmployee(int id); // сотрудник по id, null - если не найден
        List<Employee> FindEmployee(Employee pattern);  // коллекция сотрудников попадающих в шаблон
        Department GetDepartment(int id);  // отдел по id, null - если не найден
        List<Department> FindDepartment(Department pattern);  // коллекция отделов попадающих в шаблон

    }
}