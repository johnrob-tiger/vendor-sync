import { CalendarEventService } from "../domain/calendar-event.service";
import { CalendarEvent } from "../domain/calendar-event";
import { CalendarEvent as CalendarEventInfra } from "./calendar-event";
import type { AddCalendarEventRequest } from "../domain/calendar-event.service";
import { UUID } from "../../../utils/uuid";

export class CalendarEventsInMemory implements CalendarEventService {
  private calendarEvents: CalendarEventInfra[] | undefined = [];

  setCalendarEvents(calendarEvents: CalendarEventInfra[] | undefined): void {
    this.calendarEvents = calendarEvents ? [...calendarEvents] : undefined;
  }

  mapDomainToModel(infraModel: CalendarEventInfra[]): CalendarEvent[] {
    return infraModel.map((infraModel: CalendarEventInfra) => ({
      id: infraModel.id,
      year: infraModel.year,
      month: infraModel.month,
      day: infraModel.day,
      title: infraModel.title,
      startHour: infraModel.startHour,
      startMinute: infraModel.startMinute,
      endHour: infraModel.endHour,
      endMinute: infraModel.endMinute,
      allDay: infraModel.allDay,
    }));
  }

  getEvents(year: number, month: number): Promise<CalendarEvent[]> {
    if (!this.calendarEvents) {
      return Promise.resolve([]);
    }
    const events: CalendarEvent[] = this.mapDomainToModel(this.calendarEvents);
    return Promise.resolve(
      events.filter((x) => x.year == year && x.month == month)
    );
  }

  getDayEvents(
    year: number,
    month: number,
    day: number
  ): Promise<CalendarEvent[]> {
    if (!this.calendarEvents) {
      return Promise.resolve([]);
    }
    const events: CalendarEvent[] = this.mapDomainToModel(this.calendarEvents);
    console.log('all events', events);
    return Promise.resolve(
      events.filter(
        (x) => x.year == year && x.month == month && x.day == day
      )
    );
  }

  addEvent(request: AddCalendarEventRequest): Promise<CalendarEvent> {
    if (!this.calendarEvents) {
      this.calendarEvents = [];
    }

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

    const newEvent: CalendarEventInfra = {
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

    this.calendarEvents.push(newEvent);

    const mapped = this.mapDomainToModel([newEvent]);

    return Promise.resolve(mapped[0]);
  }

  removeEvent(eventId: string): Promise<boolean> {
    if (!eventId || !UUID.isGuid(eventId)) {
      throw new Error("Invalid event id");
    }

    if (!this.calendarEvents) {
      throw new Error(`Unable to remove event: ${eventId}.`);
    }

    this.calendarEvents = [
      ...this.calendarEvents.filter(
        (event: CalendarEventInfra) => event.id !== eventId
      ),
    ];

    return Promise.resolve(true);
  }

  updateEvent(event: CalendarEvent): Promise<CalendarEvent> {
    if (!this.calendarEvents) {
      throw new Error(`Unable to update event: ${event.id}`);
    }

    this.calendarEvents = [
      ...this.calendarEvents.map((curr: CalendarEventInfra) => {
        return event.id === curr.id
          ? {
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
            }
          : curr;
      }),
    ];

    const updatedEvent = this.calendarEvents.find(
      (evt: CalendarEvent) => evt.id === event.id
    );

    if (!updatedEvent) {
      throw new Error("Missing event");
    }

    return Promise.resolve(this.mapDomainToModel([updatedEvent])[0]);
  }
}
