import { CalendarEvent as CalendarEventDomain } from "../domain/calendar-event";
import { CalendarEvent } from "./calendar-event";

export const mapApplicationModel = (
  calendarEventDomain: CalendarEventDomain[]
): CalendarEvent[] => {
  return calendarEventDomain.map(
    (calendarEventDomain: CalendarEventDomain) => ({
      id: calendarEventDomain.id,
      date: calendarEventDomain.date,
      title: calendarEventDomain.title,
      startHour: calendarEventDomain.startHour,
      startMinute: calendarEventDomain.startMinute,
      endHour: calendarEventDomain.endHour,
      endMinute: calendarEventDomain.endMinute,
      allDay: calendarEventDomain.allDay,
    })
  );
};
