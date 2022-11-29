export interface CalendarEvent {
    id: string;
    date: Date;
    title: string;
    startHour: number;
    startMinute: number;
    endHour?: number;
    endMinute?: number;
    allDay?: boolean;
}