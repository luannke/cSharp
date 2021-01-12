using System;

namespace Property
{
    public static class PropertyProgram
    {
        static void Main(string[] args)
        {
            EmployeeInfo employeeInfo = new EmployeeInfo();

            // 仍然可以使用简单的赋值操作符对姓或名进行赋值
            employeeInfo.FirstName = "Inigo";
            
            Console.WriteLine(employeeInfo.FirstName);
            
            employeeInfo.Initialize(32);
            // error:
            //employeeInfo.Id = "ewr";
        }
    }

    internal class EmployeeInfo
    {
        // 属性的实现由两个可选的部分构成。其中，get标志属性的取值方法（getter）
        // 用表达式主体成员定义属性
        // 有两种不同的语法实现属性
        public string FirstName
        {
            get => _firstName;
            // set => _firstName = value;
            
            // 提供属性验证
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else
                {
                    value = value.Trim();
                    if (value == "")
                    {
                        throw new ArgumentException(
                            "First Name cannot be blank.", "value");
                    }
                }
            }
        }
        private string _firstName;
        
        public string LastName
        {
            get => _lastName;
            // set => _firstName = value;
            
            // nameof操作符
            // nameof操作符的优点在于，以后若标识符名称发生改变，重构工具能自动修改nameof的实参
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                else
                {
                    value = value.Trim();
                    if (value == "")
                    {
                        throw new ArgumentException(
                            "First Name cannot be blank.", nameof(value));
                    }
                }
            }
        }
        private string _lastName;
        
        // 自动属性的实现
        public  string Title { get; set; }
        public EmployeeInfo Manager { get; set; }
        // 正常初始化
        public string Salary { get; set; } = "Not Enough";

        
        // 只读属性        
        public void Initialize(int id)
        {
            _id = id.ToString();
        }

        public string Id
        {
            get => _id;
        }

        private string _id;
    }
    
}