
using src.Models;

namespace SoftwareProjectManager.ViewModels;

public class ProjectViewModel : ViewModelBase
{
    Project _project;
    public Project Project { get; }

    public ProjectViewModel()
    {
        
    }

    public ProjectViewModel(OrgUser user, Project project)
    {
        
    }
}