using Sample.Domains;
using Sample.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Services
{
    public interface IEmployeeService
    {
        void Save(Employee employee);
        List<Employee> GetAll(string firstNameFilter, string lastNameFilter, string genderFilter);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void Save(Employee employee)
        {
            if (employee.Id == 0)
            {
                // Insert new employee
                _employeeRepository.Insert(employee);
            }
            else
            {
                // Update existing employee
                _employeeRepository.Update(employee);
            }
        }

        public List<Employee> GetAll(string firstNameFilter, string lastNameFilter, string genderFilter)
        {
            // Apply filters to query employees
            var query = _employeeRepository.GetAll();

            if (!string.IsNullOrEmpty(firstNameFilter))
                query = query.Where(e => e.FirstName.Contains(firstNameFilter));

            if (!string.IsNullOrEmpty(lastNameFilter))
                query = query.Where(e => e.LastName.Contains(lastNameFilter));

            if (!string.IsNullOrEmpty(genderFilter))
                query = query.Where(e => e.Gender == genderFilter);

            return query.ToList();
        }
    }
}
