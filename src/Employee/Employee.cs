using System;
using System.IO;

// 封装，员工信息
namespace Employee
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // 实例化
            EmployeeInfo employeeInfo = new EmployeeInfo
            {
                FirstName = "Inigo", LastName = "Montoya", Salary = "Too little"
            };

            employeeInfo.SetName("Thomas", "Santiago");
            employeeInfo.Save();
            
            IncreaseSalary(employeeInfo);
            // Console.WriteLine($"{employeeInfo.FirstName} {employeeInfo.LastName} {employeeInfo.Salary}");
            EmployeeInfo employeeInfo1 = DataStorage.Load("Thomas", "Santiago");
            
            Console.WriteLine($"{employeeInfo1.GetName()}: {employeeInfo1.Salary}");
        }

        private static void IncreaseSalary(EmployeeInfo employeeInfo)
        {
            employeeInfo.Salary = "Enough to survive on";
        }
    }

    internal class EmployeeInfo
    {
        public string FirstName;
        public string LastName;
        public string Salary;
        // 使用 private 访问修饰符隐藏
        // 外部不可见，类外部不可见的成员称为私有成员
        private string Password;
        private bool IsAuthenticated;

        // 实例方法
        public string GetName()
        {
            return $"{FirstName} {LastName}";
        }
        
        // this 关键字
        // 指出字段是类的实例成员
        public void SetName(string newFirstName, string newLastName)
        {
            this.FirstName = newFirstName;
            this.LastName = newLastName;
            Console.WriteLine($"Name changed to '{this.GetName()}'");
        }

        // 通过 this 传递对象实例
        public void Save()
        {
            DataStorage.Store(this);
        }

        public bool Logon(string password)
        {
            if (Password == password)
            {
                IsAuthenticated = true;
            }

            return IsAuthenticated;
        }

        public bool GetIsAuthenticated()
        {
            return IsAuthenticated;
        }

    }

    internal static class DataStorage
    {
        // save an employee obj to a file
        public static void Store(EmployeeInfo employeeInfo)
        {
            // 使用员工名作为文件名称实例化 FileStream
            FileStream stream = new FileStream(
                employeeInfo.FirstName + employeeInfo.LastName + ".dat",
                FileMode.Create);
            // 实例化写入
            StreamWriter writer = new StreamWriter(stream);
            
            writer.WriteLine(employeeInfo.FirstName);
            writer.WriteLine(employeeInfo.LastName);
            writer.WriteLine(employeeInfo.Salary);
            
            // 自动关闭 fileStream
            writer.Dispose();
        }

        public static EmployeeInfo Load(string firstName, string lastName)
        {
            EmployeeInfo employeeInfo = new EmployeeInfo();
            FileStream stream = new FileStream(
                firstName + lastName + ".dat", FileMode.Open);

            StreamReader reader = new StreamReader(stream);

            employeeInfo.FirstName = reader.ReadLine();
            employeeInfo.LastName = reader.ReadLine();
            employeeInfo.Salary = reader.ReadLine();
            
            reader.Dispose();

            return employeeInfo;
        }
    }
}