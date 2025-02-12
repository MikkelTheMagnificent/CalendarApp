using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using CalendarApp.Desktop.Helpers;

namespace CalendarApp.Desktop.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private DateTime _currentMonth;
        public ObservableCollection<CalendarDay> CalendarDays { get; set; }

        public ICommand PreviousMonthCommand { get; set; }
        public ICommand NextMonthCommand { get; set; }
        public ICommand AbsenceCommand { get; set; }
        public ICommand NavigateToEmployees { get; set; }

        private object _currentViewModel;
        public object CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public string CurrentMonthYear => _currentMonth.ToString("MMMM yyyy", CultureInfo.InvariantCulture);

        public MainViewModel()
        {
            _currentMonth = DateTime.Today;
            CalendarDays = new ObservableCollection<CalendarDay>();

            PreviousMonthCommand = new RelayCommand(_ => ChangeMonth(-1));
            NextMonthCommand = new RelayCommand(_ => ChangeMonth(1));
            AbsenceCommand = new RelayCommand(OpenAbsenceView);
            NavigateToEmployees = new RelayCommand(OpenEmployeesView);

            GenerateCalendar();
        }

        private void ChangeMonth(int offset)
        {
            _currentMonth = _currentMonth.AddMonths(offset);
            OnPropertyChanged(nameof(CurrentMonthYear));
            GenerateCalendar();
        }

        private void GenerateCalendar()
        {
            CalendarDays.Clear();

            int daysInMonth = DateTime.DaysInMonth(_currentMonth.Year, _currentMonth.Month);
            DateTime firstDayOfMonth = new DateTime(_currentMonth.Year, _currentMonth.Month, 1);

            // Skift startdag: Gør mandag til første dag (0 = Mandag, 6 = Søndag)
            int startDayIndex = ((int)firstDayOfMonth.DayOfWeek + 6) % 7;

            // ** Hent forrige måneds data ** (til at fylde pladsen før den første dag)
            DateTime prevMonth = _currentMonth.AddMonths(-1);
            int prevMonthDays = DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);

            for (int i = startDayIndex - 1; i >= 0; i--)
            {
                CalendarDays.Add(new CalendarDay
                {
                    Day = (prevMonthDays - i).ToString(),
                    IsCurrentMonth = false
                });
            }

            // ** Hent indeværende måneds data **
            for (int day = 1; day <= daysInMonth; day++)
            {
                CalendarDays.Add(new CalendarDay
                {
                    Day = day.ToString(),
                    IsCurrentMonth = true
                });
            }

            // ** Hent næste måneds data for at fylde grid’et **
            int nextMonthDay = 1;
            while (CalendarDays.Count % 7 != 0)
            {
                CalendarDays.Add(new CalendarDay
                {
                    Day = nextMonthDay.ToString(),
                    IsCurrentMonth = false
                });
                nextMonthDay++;
            }

            OnPropertyChanged(nameof(CalendarDays));
        }


        // ** Model til kalenderdage **
        public class CalendarDay
        {
            public string Day { get; set; }
            public bool IsCurrentMonth { get; set; }
        }

        

        private void OpenAbsenceView(object obj)
        {
            CurrentViewModel = new AbsenceViewModel();
        }

        private void OpenEmployeesView(object obj)
        {
            CurrentViewModel = new EmployeeViewModel();
        }
    }
}
