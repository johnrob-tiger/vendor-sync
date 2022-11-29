import { createContext, useContext } from "react";
import { CalendarEvent as DomainCalendarEvent } from "../domain/calendar-event";
import { CalendarEventRequest } from "../domain/calendar-event-request";

export interface EventContextProps {
  eventRequest: CalendarEventRequest | undefined;
  selectedEvent: DomainCalendarEvent | undefined;
  events: DomainCalendarEvent[];
  requestEvent: (event: CalendarEventRequest) => void;
  cancelRequest: () => void;
  selectEvent: (event: DomainCalendarEvent) => void;
  setEvents: (events: DomainCalendarEvent[]) => void;
}

const initialValues: EventContextProps = {
  eventRequest: undefined,
  selectedEvent: undefined,
  events: [],
  requestEvent: (event: CalendarEventRequest) => {},
  cancelRequest: () => {},
  selectEvent: (event: DomainCalendarEvent) => {},
  setEvents: (events: DomainCalendarEvent[]) => {}
};

const EventContext = createContext<EventContextProps>(initialValues);

export default EventContext;
