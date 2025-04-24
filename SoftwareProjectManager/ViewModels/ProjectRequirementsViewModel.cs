using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using System.Xml;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using SoftwareProjectManager.Views;
using src.Models;

namespace SoftwareProjectManager.ViewModels;

public class ProjectRequirementsViewModel : ViewModelBase
{
    Project _project;
    public Project Project { get; }

    public ProjectRequirementsViewModel()
    {
        
    }

    public ProjectRequirementsViewModel(OrgUser user, Project project)
    {
        
    }
}