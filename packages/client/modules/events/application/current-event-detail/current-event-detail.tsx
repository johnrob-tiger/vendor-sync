"use client";

import React, { useEffect, useState } from "react";

import EventTime from "../event-time/event-time";
import { useCalendarEvents } from "../calendar-events-provider";
import CreateEventForm from "../create-event-form/create-event-form";
import EditEventForm from "../edit-event-form/edit-event-form";

import styles from "./current-event-detail.module.css";

export default function CurrentEventDetail() {
  const {
    cancelRequest,
    selectEvent,
    selectedEvent,
    events,
    eventRequest,
    setEvents,
  } = useCalendarEvents();

  const [editMode, setEditMode] = useState(false);

  useEffect(() => {
    if (!selectedEvent && events.length) {
      selectEvent(events[0]);
    }
  }, []);

  if (eventRequest) {
    return (
      <div>
        <div className={styles["header"]}>

        <h4>New Event</h4>
        <button onClick={() => { cancelRequest(); }}>          
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              viewBox="0 0 24 24"
              fill="none"
              stroke="currentColor"
              strokeWidth="2"
              strokeLinecap="round"
              strokeLinejoin="round"
            >
              <line x1="18" y1="6" x2="6" y2="18" />
              <line x1="6" y1="6" x2="18" y2="18" />
            </svg>
        </button>
        </div>
        
        <CreateEventForm eventRequest={eventRequest} />
      </div>
    );
  }

  return selectedEvent ? (
    <div className={styles["current-event-detail"]}>
      <div className={styles["header"]}>
        <h4>{selectedEvent.title}</h4>

        <button onClick={() => setEditMode((editMode) => !editMode)}>
          {!editMode && (
            <svg
              width="24"
              height="24"
              viewBox="0 0 24 24"
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              stroke="currentColor"
              strokeWidth="2"
              strokeLinecap="round"
              strokeLinejoin="round"
            >
              <path d="M17 3a2.828 2.828 0 1 1 4 4L7.5 20.5 2 22l1.5-5.5L17 3z" />
            </svg>
          )}
          {editMode && (
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              viewBox="0 0 24 24"
              fill="none"
              stroke="currentColor"
              strokeWidth="2"
              strokeLinecap="round"
              strokeLinejoin="round"
            >
              <line x1="18" y1="6" x2="6" y2="18" />
              <line x1="6" y1="6" x2="18" y2="18" />
            </svg>
          )}
        </button>
      </div>
      {editMode ? (
        <EditEventForm event={selectedEvent} onSave={() => setEditMode(false) } />
      ) : (
        <p>
          <EventTime event={selectedEvent} />
        </p>
      )}
    </div>
  ) : (
    <div></div>
  );
}
