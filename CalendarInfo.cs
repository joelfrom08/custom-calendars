class CalendarInfo {
    public string calendarName { get; set; }
    public List<string> monthNames { get; set; }
    public List<int> monthDurations { get; set; }
    public Dictionary<int, int> leapDays { get; set; } // month_with_leapdays, amount_of_leapdays

    public CalendarInfo(string calendarName, List<string> monthNames, List<int> monthDurations, Dictionary<int, int> leapDays) {
        this.calendarName = calendarName;
        this.monthNames = monthNames;
        this.monthDurations = monthDurations;
        this.leapDays = leapDays;
    }
}