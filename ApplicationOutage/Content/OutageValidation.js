function validateDates(startDateString, endDatestring)
{
    var startDate = new Date(startDateString);
    var startEnd = new Date(endDatestring);
    if (startEnd <= startDate)
    {
        return false;
    }
    return true;
}
