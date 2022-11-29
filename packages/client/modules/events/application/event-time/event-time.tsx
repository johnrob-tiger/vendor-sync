"use client";

import { CalendarEvent as DomainCalendarEvent } from "../../domain/calendar-event"

import styles from "./event-time.module.css";

export type EventTimeProps = {
    event: DomainCalendarEvent;    
}
export default function EventTime({event}: EventTimeProps) {

    const formatHour = (time: number) => {
        let t = time > 12 ? time - 12 : time;
        return `00${t.toString()}`.slice(-2);
    }

    const formatMinute = (time: number) => {
        return `00${time.toString()}`.slice(-2);
    }

    return (
        <div className={styles["event-time"]}>
            <span>{formatHour(event.startHour)}:{formatMinute(event.startMinute)}</span>
            <span>{event.startHour < 12 ? "AM" : "PM"}</span>
            {event.endHour && event.endHour != event.startHour && event.endMinute != event.startMinute ? 
                <>
                    <span>-</span>
                    <span>{formatHour(event.endHour)}:{formatMinute(event.endMinute ?? 0)}</span>
                    <span>{event.endHour < 12 ? "AM" : "PM"}</span>
                </> : null}
        </div>
    )
}