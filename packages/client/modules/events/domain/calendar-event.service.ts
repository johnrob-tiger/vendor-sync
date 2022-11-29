import { CalendarEvent } from "./calendar-event";

export type AddCalendarEventRequest = Partial<CalendarEvent>;

export interface CalendarEventService {
    getEvents(year: number, month: number): Promise<CalendarEvent[]>;
    getDayEvents(year: number, month: number, day: number): Promise<CalendarEvent[]>;
    addEvent(request: AddCalendarEventRequest): Promise<CalendarEvent>;
    removeEvent(eventId: string): Promise<boolean>;
    updateEvent(event: CalendarEvent): Promise<CalendarEvent>;
}