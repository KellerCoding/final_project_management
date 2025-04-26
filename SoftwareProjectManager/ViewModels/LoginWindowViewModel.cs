using System;
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
using SoftwareProjectManager.Views;
using src.Models;

namespace SoftwareProjectManager.ViewModels;

public class LoginWindowViewModel : ViewModelBase
{
    public string? _inputUsername;
    public string? _inputPassword;
    
    public string? InputUsername
    {
        get => _inputUsername;
        set => this.RaiseAndSetIfChanged(ref _inputUsername, value);
    }

    public string? InputPassword
    {
        get => _inputPassword;
        set => this.RaiseAndSetIfChanged(ref _inputPassword, value);
    }
    
    OrgUser _default = new OrgUser(1, "mainuser", "password");
    OrgUser _patrick = new OrgUser("pcox", "password");
    OrgUser _khamin = new OrgUser("kkeller", "password");
    OrgUser _jr = new OrgUser("jstraiton", "password");
    OrgUser _jared = new OrgUser("jlouss", "password");
    OrgUser _kevin = new OrgUser("ksyn", "password");
    
    List<OrgUser> users = new List<OrgUser>();

    
    
    public ICommand VerifyLogin { get; }
    
    

    public LoginWindowViewModel()
    {
        users.Add(_default);
        users.Add(_patrick);
        users.Add(_khamin);
        users.Add(_jr);
        users.Add(_jared);
        users.Add(_kevin);
        
        VerifyLogin = ReactiveCommand.Create(() =>
        {
            foreach (OrgUser user in users)
            {
                if (_inputUsername == user.GetUsername() && user.Login(user.GetDatabasePassword()))
                {

                    var mainWindow =
                        (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)
                        ?.MainWindow;
                    if (mainWindow != null)
                    {
                        Console.WriteLine(mainWindow.Title);
                        mainWindow.Hide();
                    }
                    


                    if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                    {
                        desktop.MainWindow = new MainWindow()
                        {
                            DataContext = new MainWindowViewModel(user)
                        };

                        desktop.MainWindow.Show();
                    }
                }
            }
            
        });

    }
}