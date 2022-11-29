
import { CalendarEventsInMemory } from "../modules/events/infrastructure/calendar-events.in-memory";

import { calendarEventFakes } from "../modules/events/infrastructure/calendar-events.fakes";

const calendarEventsService = new CalendarEventsInMemory();
calendarEventsService.setCalendarEvents(calendarEventFakes);

export const services = {
    calendarEventsService
}
