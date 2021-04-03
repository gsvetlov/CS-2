using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using EmployeeViewer.Data;

using EmployeeViewerWebService.DataProvider;

namespace EmployeeViewerWebService
{
    /// <summary>
    /// Summary description for EmployeeViewerService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class EmployeeViewerService : System.Web.Services.WebService
    {
        private static string dbConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["employeeDb"].ConnectionString;
        private IEntityProvider database = new SqlExpressDbProvider(dbConnectionString);

        [WebMethod]
        public int AddEmployee(Employee employee) => database.Add(employee); // возвращает Id записи или -1, если запись не прошла

        [WebMethod]
        public int RemoveEmployee(Employee employee) => database.Remove(employee); // возвращает количество обработанных записей

        [WebMethod]
        public int UpdateEmployee(Employee employee) => database.Update(employee); // возвращает количество обработанных записей

        [WebMethod]
        public int AddDepartment(Department department) => database.Add(department); // возвращает Id записи или -1, если запись не прошла
        
        [WebMethod]
        public int RemoveDepartment(Department department) => database.Remove(department); // возвращает количество обработанных записей
        
        [WebMethod]
        public int UpdateDepartment(Department department) => database.Update(department); // возвращает количество обработанных записей

        [WebMethod]
        public List<Department> GetDepartments() => database.GetDepartments(); // колекция всех отделов

        [WebMethod]
        public List<Employee> GetEmployees() => database.GetEmployees(); // коллекция всех сотрудников

        [WebMethod]
        public Employee GetEmployee(int id) => database.GetEmployee(id); // сотрудник по id, null - если не найден

        [WebMethod]
        public List<Employee> FindEmployee(Employee pattern) => database.FindEmployee(pattern);  // коллекция сотрудников попадающих в шаблон

        [WebMethod]
        public Department GetDepartment(int id) => database.GetDepartment(id);  // отдел по id, null - если не найден

        [WebMethod]
        public List<Department> FindDepartment(Department pattern) => database.FindDepartment(pattern);  // коллекция отделов попадающих в шаблон
    }
}
