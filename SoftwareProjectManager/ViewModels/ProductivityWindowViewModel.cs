using System.Collections.ObjectModel;
using ReactiveUI;
using System.Reactive;
using System;
using System.Collections;
using System.Collections.Generic;
using SoftwareProjectManager.Models;
using src.Models;

namespace SoftwareProjectManager.ViewModels
{


    public class ProductivityWindowViewModel : ViewModelBase
    {
        private static Project _project = new Project(2, "no", "no");
        public ObservableCollection<Phase>? phases { get; set; }

        // Constructor
        public ProductivityWindowViewModel()
        {

            Phase engDesign = new Phase(1, "Design", "Plan for the future of the design");

            Phase implementation = new Phase(2, "Implementation", "Here is where the planning is put to action");
            try
            {
                _project.AddPhase(engDesign);
                _project.AddPhase(implementation);
            }
            catch (Exception e)
            {

            }

            var hold = new ArrayList();


            
            
            try
            {
               _project.UpdatePhaseWeeklyHours(1, 20.0);
               _project.UpdatePhaseTotalHours(1, 105.0);
               _project.UpdatePhaseWeeklyHours(2, 20.0);
               _project.UpdatePhaseTotalHours(2, 105.0); 
                
                hold = _project.GetPhases();
            }
            catch (Exception e)
            {


                int weeklyHours = 0;
                var name = "";
                int totalHours = 0;
                List<Phase> pha = new List<Phase>();

                int i = 0;
                while (i < hold.Count - 2)
                {
                    name = hold[i].ToString();
                    i++;
                    weeklyHours = int.Parse(hold[i].ToString());
                    i++;
                    totalHours = int.Parse(hold[i].ToString());
                    i++;
                    pha.Add(new Phase(i/3,name, weeklyHours, totalHours));
                }

                phases = new ObservableCollection<Phase>(pha);
                foreach (Phase phase in pha)
                {
                    Console.WriteLine(phase.ToString() + "\n");
                    Console.WriteLine(_project.GetPhase(1)[1]);
                }

            }
        }
    }
}

       
    

