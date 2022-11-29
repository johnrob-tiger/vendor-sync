"use client";

import React, { useState, useContext } from "react";
import EventContext, {
  EventContextProps,
} from "../infrastructure/calendar-event-context";

import { CalendarEvent as DomainCalendarEvent } from "../domain/calendar-event";
import { CalendarEventRequest } from "../domain/calendar-event-request";

export interface CalendarEventsProviderProps {
  children: React.ReactNode;
  events?: DomainCalendarEvent[];
  selectedEvent?: DomainCalendarEvent;
}

export default function CalendarEventsProvider(
  props: CalendarEventsProviderProps
) {
  const [eventRequest, setEventRequest] = useState<
    CalendarEventRequest | undefined
  >();

  const [currentEvents, setCurrentEvents] = useState<DomainCalendarEvent[]>(
    () => props.events ?? []
  );
  const [selectedEvent, setSelectedEvent] = useState<
    DomainCalendarEvent | undefined
  >();

  const state = {
    selectedEvent,
    events: currentEvents,
    eventRequest,
    requestEvent(request: CalendarEventRequest) {
      setSelectedEvent(undefined);
      setEventRequest(undefined);
      setEventRequest(request);
    },
    cancelRequest() {
      setEventRequest(undefined);
    },
    setEvents(events: DomainCalendarEvent[]) {
      setEventRequest(undefined);
      setCurrentEvents(events);
    },
    selectEvent(event: DomainCalendarEvent) {
      setEventRequest(undefined);
      setSelectedEvent(event);
    },
  } as EventContextProps;

  return (
    <EventContext.Provider value={state}>
      {props.children}
    </EventContext.Provider>
  );
}

export const useCalendarEvents = () => {
  return useContext(EventContext);
};
