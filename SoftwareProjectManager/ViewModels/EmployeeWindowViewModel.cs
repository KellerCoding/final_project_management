using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Xml;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using SoftwareProjectManager.Models;
using SoftwareProjectManager.Views;
using src.Models;

namespace SoftwareProjectManager.ViewModels;

public class EmployeeWindowViewModel : ViewModelBase
{
    //private static Project _project = new Project(2,"no","no");
    public ObservableCollection<Employee>? Employees { get; set; }

    public EmployeeWindowViewModel(){}
    
    public EmployeeWindowViewModel(Project _project)
    {
        Employee Kahmin = new Employee(6, "Kahmin Keller", "Project Manager");
        Employee RJ = new Employee(7, "RJ Straiton", "Programmer");
        Employee Patrick = new Employee(8, "Patrick Cox", "Programmer");
        Employee Jared = new Employee(9, "Jared Louissaint", "Programmer");
        Employee Kevin = new Employee(10, "Kevin Syhavong", "Programmer");

        try
        {
            _project.AddEmployee(Kahmin);
            _project.AddEmployee(RJ);
            _project.AddEmployee(Patrick);
            _project.AddEmployee(Jared);
            _project.AddEmployee(Kevin);
        }
        catch (Exception e)
        {
            
        }

        var hold = new ArrayList();
        hold = _project.GetTeamMembers();
        
        int ID=0;
        var name="";
        var jobTitle="";
        List<Employee> emp = new List<Employee>();

      
            int i = 0;
            while(i<hold.Count-2)
            {
                
                    ID = int.Parse(hold[i].ToString());
                    i++;
                    name = hold[i].ToString();
                    i++;
                    jobTitle = hold[i].ToString();
                    i++;
                    emp.Add(new Employee(ID, name, jobTitle));
                
            }

       
        

        Employees = new ObservableCollection<Employee>(emp);
        foreach (Employee em in emp)
        {
            Console.WriteLine(em.ToString()+"\n");
        }
    }
    
}