import { CalendarEvent } from "./calendar-event";
import { CalendarEventService } from "./calendar-event.service";

export const getEvents = async ({
  calendarEventService,
}: {
  calendarEventService: CalendarEventService;
}): Promise<CalendarEvent[]> => {
  try {
    const today = new Date();
    return await calendarEventService.getEvents(
      today.getFullYear(),
      today.getMonth() + 1
    );
  } catch (err: any) {
    throw new Error(err);
  }
};

export const getMonthEvents = async ({
  year,
  month,
  calendarEventService,
}: {
  year: number;
  month: number;
  calendarEventService: CalendarEventService;
}): Promise<CalendarEvent[]> => {
  try {
    return await calendarEventService.getEvents(year, month);
  } catch (err: any) {
    throw new Error(err);
  }
};

export const addEvent = async ({
  calendarEventService,
  date,
  title,
  startHour,
  startMinute,
  endHour,
  endMinute,
  allDay,
}: {
  calendarEventService: CalendarEventService;
  date: Date;
  title: string;
  startHour: number;
  startMinute: number;
  endHour?: number;
  endMinute?: number;
  allDay?: boolean;
}): Promise<CalendarEvent> => {
  try {
    return await calendarEventService.addEvent({
      date,
      title,
      startHour,
      startMinute,
      endHour,
      endMinute,
      allDay,
    });
  } catch (err: any) {
    throw new Error(err);
  }
};

export const removeEvent = async ({
  calendarEventService,
  eventId,
}: {
  calendarEventService: CalendarEventService;
  eventId: string;
}): Promise<boolean> => {
  try {
    return await calendarEventService.removeEvent(eventId);
  } catch (err: any) {
    throw new Error(err);
  }
};

export const updateEvent = async ({
  calendarEventService,
  calendarEvent,
}: {
  calendarEventService: CalendarEventService;
  calendarEvent: CalendarEvent;
}): Promise<CalendarEvent> => {
  try {
    return await calendarEventService.updateEvent(calendarEvent);
  } catch (err: any) {
    throw new Error(err);
  }
};
