"use client";

import { useEffect, useRef } from "react";
import { CalendarEvent as CalendarEventDomain } from "../../domain/calendar-event";
import styles from "./calendar-day-view.module.css";
import { useCalendarEvents } from "../calendar-events-provider";

export type EventBoxProps = {
  event: CalendarEventDomain;
  offset?: number;
  gap?: number;
  push?: number;
  width?: number;
};

export default function EventBox(props: EventBoxProps) {
  const boxRef = useRef<HTMLUListElement>(null);
  const itemRef = useRef<HTMLLIElement>(null);

  const { selectEvent, selectedEvent } = useCalendarEvents();
  const { startHour, startMinute, endHour, endMinute } = props.event;

  useEffect(() => {
    if (boxRef.current && itemRef.current) {
      boxRef.current.style.top =
        startHour * (props.gap ?? 16) +
        (startHour * (props.offset ?? 100) +
          (startMinute ? (startMinute / 60) * (props.offset ?? 100) : 0)) +
        "px";

      const diff = (endHour ?? startHour) - startHour;
      const minDiff = startMinute ? startMinute / 60 : 0;
      const minDiffEnd = endMinute ? endMinute / 60 : 0;

      const h =
        diff * (props.offset ?? 100) +
        diff * (props.gap ?? 16) +
        minDiff * (props.offset ?? 100) +
        minDiffEnd * (props.offset ?? 100);

      if (h > 30) {
        itemRef.current.style.height = h + "px";
      }

      if (props.width) {
        boxRef.current.style.width = props.width + "px";
      }

      if (props.push) {
        boxRef.current.style.left = props.push + "px";
      }

      boxRef.current.style.opacity = "1";
    }
  }, [startHour, startMinute, endHour, endMinute]);

  return (
    <ul
      onClick={() => selectEvent(props.event)}
      ref={boxRef}
      className={[
        styles["event-box"],
        selectedEvent?.id === props.event.id ? styles["active"] : "",
      ].join(" ")}
    >
      <li ref={itemRef}>
        <strong>{props.event.title}</strong>
        <p>{props.event.allDay ? "All Day" : ""}</p>
      </li>
    </ul>
  );
}
