import addMonths from "date-fns/addMonths";
import getDaysInMonth from "date-fns/getDaysInMonth";

export const getMonth = (year: number, month: number) => {

    if (month < 1 || month > 12) {
        throw new Error('Month must be 1 through 12.');
    }

    if (year < 1) {
        throw new Error('Year must be greater than zero.');
    }

    const dt = new Date(year, month - 1, 1);
    const daysInMonth = getDaysInMonth(dt);
    const monthName = dt.toLocaleString('default', { month: 'long' });
    const shortMonthName = dt.toLocaleString('default', { month: 'short' });

    const offset = dt.getDay();

    const lastMonth = addMonths(dt, -1);
    const daysInLastMonth = getDaysInMonth(lastMonth);

    return {
        daysInMonth,
        daysInLastMonth,
        monthName,
        shortMonthName,
        offset
    }
}

export const dayNames = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];

export const getDay = (year: number, month: number, day: number) => {

    const monthInfo = getMonth(year, month);

    const dt = new Date(year, month, day);

    const dayName = dayNames[dt.getDay()];

    return {
        ...monthInfo,
        dayName,
        dayOfWeek: dt.getDay(),
        suffix: getSuffix(day)
    }
}

export const getSuffix = (day: number) => {
    const dayStr = day.toString();
    if (dayStr.endsWith('1')) {
        return 'st';
    }
    if (dayStr.endsWith('2')) {
        return 'nd';
    }
    if (dayStr.endsWith('3')) {
        return 'rd';
    }

    return 'th';
}