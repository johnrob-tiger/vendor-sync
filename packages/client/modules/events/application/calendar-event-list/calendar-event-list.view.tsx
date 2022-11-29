"use client";

import React from "react";
import { CalendarEvent as CalendarEventDomain } from "../../domain/calendar-event";

export type CalendarEventListViewProps = {
    events: CalendarEventDomain[]
}

export default function CalendarEventList(props: CalendarEventListViewProps) {

    return (
        <div>
            {props.events && props.events.length ? 
                props.events.map((evt: CalendarEventDomain) => (
                    <div key={evt.id}>
                        <label>{evt.day}th</label> -
                        <label>{evt.title}</label> -
                        <label>{evt.startHour}:{evt.startMinute}</label>
                    </div>
                )) : <div>No events</div>}
        </div>

    )
    
}