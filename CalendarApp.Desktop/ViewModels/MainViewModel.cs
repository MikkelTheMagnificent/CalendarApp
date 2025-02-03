using System;
using System.Collections.ObjectModel;
using CalendarApp.Desktop.Models;

namespace CalendarApp.Desktop.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<EventModel> Events { get; set; }

        public MainViewModel()
        {
            // Eksempeldata - Du kan hente dette fra EventService
            Events = new ObservableCollection<EventModel>
            {
                new EventModel { EventName = "Meeting", Description = "Project meeting", StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(1) },
                new EventModel { EventName = "Lunch", Description = "Team lunch", StartDate = DateTime.Now.AddHours(2), EndDate = DateTime.Now.AddHours(3) }
            };
        }
    }
}
