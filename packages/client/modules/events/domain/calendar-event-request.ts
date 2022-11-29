export interface CalendarEventRequest {
    year: number;
    month: number;
    day: number;
    title: string;
    startHour: number;
    startMinute: number;
    endHour?: number;
    endMinute?: number;
    allDay?: boolean;
}