namespace TransientDI
{
    public class Employee : IEmployee
    {
        List<string> employees;
        public Employee()
        {
            employees = new List<string>();
        }
        public void AddEmployee(string name)
        {
            employees.Add(name);
        }

        public List<string> GetEmployee()
        {
            return employees;
        }
    }
}
