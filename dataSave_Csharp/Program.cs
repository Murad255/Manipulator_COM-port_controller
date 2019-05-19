using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class Program
{
    static void Main(string[] args)
    {

        Users users = new Users();
        users.Load("users.txt");
        foreach (var item in users)
        {
            Console.WriteLine("У пользователя с логином:{0}, пароль:{1}", item.Login, item.Password);
        }
        Console.Read();
    }
}
public class User
{
    public string Login { get; set; }
    public string Password { get; set; }
}
public class Users : List<User>
{
    public void Add(string login, string Pass)
    {
        User user = new User() { Login = login, Password = Pass };
        base.Add(user);
    }
    public void Add(params string[]data)
    {
        User user = new User() { Login = data[0], Password = data[1] };
        base.Add(user);
    }
    public void Load(string Path)
    {
        using (StreamReader sr = new StreamReader(Path))
        {
            // Считываем файл
            // В файле каждая строчка должна соотвествовать
            // Логину и паролю через ";"
            while (!sr.EndOfStream)
            {
                string[] tmp = sr.ReadLine().Split('z').ToArray();
                Add(tmp);
            }
        }

    }
    public void Save(string Path)
    {
        using (StreamWriter sr = new StreamWriter(Path))
        {
            foreach (var item in this)
            {
                sr.WriteLine(item.Login + ";" + item.Password);
            }
            {

            }
        }
    }
}