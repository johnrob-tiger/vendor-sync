import { CalendarEventService } from "../domain/calendar-event.service";
import { CalendarEvent } from "../domain/calendar-event";
import { CalendarEvent as CalendarEventInfra } from "./calendar-event";
import type { AddCalendarEventRequest } from "../domain/calendar-event.service";
import { UUID } from "../../../utils/uuid";

export class CalendarEventsLocalStorage implements CalendarEventService {
  setLocalEvents(calendarEvents: CalendarEventInfra[] | undefined): void {
    localStorage.setItem("calendarEvents", JSON.stringify(calendarEvents));
  }

  getLocalEvents(): CalendarEvent[] {
    const localEvents: string | null = localStorage.getItem("calendarEvents");

    return localEvents ? JSON.parse(localEvents) : [];
  }

  getEvents(year: number, month: number): Promise<CalendarEvent[]> {
    const events = this.getLocalEvents().filter(
      (x) => x.year === year && x.month === month
    );

    return Promise.resolve(events);
  }

  getDayEvents(year: number, month: number, day: number): Promise<CalendarEvent[]> {
    const events = this.getLocalEvents().filter(
      (x) => x.year === year && x.month === month && x.day === day
    );

    return Promise.resolve(events);
  }

  addEvent(request: AddCalendarEventRequest): Promise<CalendarEvent> {
    if (!request.year) {
      throw new Error("A year is required.");
    }

    if (!request.month || request.month < 1 || request.month > 12) {
      throw new Error("Month is required and must be between 1 and 12.");
    }

    if (!request.day || request.day < 1 || request.day > 31) {
      throw new Error("Day is required and must be between 1 and 31.");
    }

    if (!request.title) {
      throw new Error("Title is required.");
    }

    if (!request.startHour) {
      throw new Error("Start hour is required.");
    }

    if (!request.startMinute) {
      request.startMinute = 0;
    }

    const newEvent: CalendarEvent = {
      id: UUID.create().toString(),
      title: request.title,
      year: request.year,
      month: request.month,
      day: request.day,
      startHour: request.startHour,
      startMinute: request.startMinute,
      endHour: request.endHour,
      endMinute: request.endMinute,
      allDay: request.allDay,
    };

    const events = [...this.getLocalEvents(), newEvent];

    this.setLocalEvents(events);

    return Promise.resolve(newEvent);
  }

  removeEvent(eventId: string): Promise<boolean> {
    if (!eventId || !UUID.isGuid(eventId)) {
      throw new Error("Invalid event id");
    }

    const events = [...this.getLocalEvents().filter((x) => x.id !== eventId)];

    this.setLocalEvents(events);

    return Promise.resolve(true);
  }

  updateEvent(event: CalendarEvent): Promise<CalendarEvent> {
    const curr = this.getLocalEvents().find((x) => x.id === event.id);

    if (!curr) {
      throw new Error(`Missing event: ${event.id}`);
    }

    const events = [
      ...this.getLocalEvents().filter((x) => x.id !== curr.id),
      {
        ...curr,
        year: event.year ?? curr.year,
        month: event.month ?? curr.month,
        day: event.day ?? curr.day,
        title: event.title ?? curr.title,
        startHour: event.startHour ?? curr.startHour,
        startMinute: event.startMinute ?? curr.startMinute,
        endHour: event.endHour ?? curr.endHour,
        endMinute: event.endMinute ?? curr.endMinute,
        allDay: event.allDay ?? curr.allDay,
      },
    ];

    this.setLocalEvents(events);

    return Promise.resolve({ ...curr, ...event });
  }
}
