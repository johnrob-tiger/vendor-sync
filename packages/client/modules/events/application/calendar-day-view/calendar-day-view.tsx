"use client";

import React, { useEffect } from "react";
import EventBox from "./event-box";
import styles from "./calendar-day-view.module.css";

import { useCalendarEvents } from "../calendar-events-provider";
import { CalendarEventRequest } from "../../domain/calendar-event-request";

export interface CalendarDayViewProps {
  year: number;
  month: number;
  day: number;
}

export default function CalendarDayView(props: CalendarDayViewProps) {
  const { events, requestEvent } = useCalendarEvents();

  useEffect(() => {
    let hour = events && events.length ? events[0].startHour : 8;
    const $el = document.querySelector(`[data-hour='${hour}']`);

    if ($el && "scrollIntoView" in $el) {
      $el.scrollIntoView({
        behavior: "smooth",
        block: "center",
        inline: "nearest",
      });
    }
  }, []);

  const newEvent: CalendarEventRequest = {
    title: "New Event",
    year: props.year,
    month: props.month,
    day: props.day,
    startHour: 8,
    startMinute: 0,
  };

  return (
    <div className={styles["day-view-calendar"]}>
      <ul className={styles["time-slots"]}>
        <li data-hour={0} className={styles["hour"]}>
          <span onClick={() => requestEvent({ ...newEvent, startHour: 0 })}>
            12:00 AM
          </span>
        </li>
        <li>
          <span
            onClick={() =>
              requestEvent({ ...newEvent, startHour: 0, startMinute: 30 })
            }
          >
            12:30
          </span>
        </li>
        {Array.from(Array(11).keys()).map((i) => (
          <>
            <li data-hour={i + 1} className={styles["hour"]} key={`hour1_${i}`}>
              <span
                onClick={() => requestEvent({ ...newEvent, startHour: i + 1 })}
              >
                {i + 1}:00 AM
              </span>
            </li>
            <li key={`half-hour1_${i}`}>
              <span
                onClick={() =>
                  requestEvent({
                    ...newEvent,
                    startHour: i + 1,
                    startMinute: 30,
                  })
                }
              >
                {i + 1}:30
              </span>
            </li>
          </>
        ))}
        <li data-hour={12} className={styles["hour"]}>
          <span onClick={() =>
                  requestEvent({
                    ...newEvent,
                    startHour: 12
                  })}>12:00 PM{" "}</span>
        </li>
        <li><span onClick={() =>
                  requestEvent({
                    ...newEvent,
                    startHour: 12,
                    startMinute: 30
                  })}>12:30</span></li>
        {Array.from(Array(11).keys()).map((i) => (
          <>
            <li
              data-hour={13 + i}
              className={styles["hour"]}
              key={`hour2_${i}`}
            >
              <span
                onClick={() => requestEvent({ ...newEvent, startHour: i + 13 })}
              >
                {i + 1}:00 PM
              </span>
            </li>
            <li key={`half-hour2_${i}`}>
              <span
                onClick={() =>
                  requestEvent({
                    ...newEvent,
                    startHour: i + 13,
                    startMinute: 30,
                  })
                }
              >
                {i + 1}:30
              </span>
            </li>
          </>
        ))}
      </ul>
      <div className={styles["events"]}>
        {events &&
          events.map((evt, i) => (
            <EventBox
              key={`box_${evt.id}`}
              width={150}
              push={i * 150}
              gap={16}
              offset={100}
              event={evt}
            />
          ))}
        {!events && <div>No events for today.</div>}
      </div>
    </div>
  );
}
