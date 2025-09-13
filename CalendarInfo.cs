class CalendarInfo {
    public string calendarName { get; set; }
    public List<string> monthNames { get; set; }
    public List<int> monthDurations { get; set; }
    public Dictionary<int, int> leapDays { get; set; } // month_with_leapdays, amount_of_leapdays
    public DateTime gregorianStartDate { get; set; } // Month and Day of new year
    public int gregorianYearOffset { get; set; } // Years of offset during new year (e.g. +73 on 01.01. for nyc)

    public CalendarInfo(string calendarName, List<string> monthNames, List<int> monthDurations, Dictionary<int, int> leapDays, int gregorianStartDay, int gregorianStartMonth, int gregorianYearOffset) {
        this.calendarName = calendarName;
        this.monthNames = monthNames;
        this.monthDurations = monthDurations;
        this.leapDays = leapDays;
        this.gregorianStartDate = new DateTime(2000, gregorianStartMonth, gregorianStartDay);
        this.gregorianYearOffset = gregorianYearOffset;
    }
}