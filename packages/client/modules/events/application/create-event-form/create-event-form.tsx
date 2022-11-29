import { useState } from "react";
import TimePicker from "../../../../components/time-picker/time-picker";
import { CalendarEventRequest } from "../../domain/calendar-event-request";

import { useCalendarEvents } from "../calendar-events-provider";

import { services } from "../../../../config/services";

export interface CreateEventFormProps {
    eventRequest: CalendarEventRequest
}

export default function CreateEventForm({eventRequest}: CreateEventFormProps) {

    const [formErrors, setFormErrors] = useState<string[]>([]);

    const [title, setTitle] = useState<string>(eventRequest.title);
    const [startHour, setStartHour] = useState<number>(() => eventRequest.startHour);
    const [endHour, setEndHour] = useState<number>(() => eventRequest.endHour ?? 0);

    const [startMinute, setStartMinute] = useState<number>(() => eventRequest.startMinute);
    const [endMinute, setEndMinute] = useState<number>(() => eventRequest.endMinute ?? 0);

    const [amPm,setAmPm] = useState<'am'|'pm'>(() => eventRequest.startHour >= 12 ? 'pm' : 'am');

    const { cancelRequest, events, setEvents, selectEvent } = useCalendarEvents();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        const errors: string[] = [];

        if (!title) {
            errors.push("A title is required!");
        }

        if (endHour < startHour) {
            errors.push("End hour must be after start hour.");
        }
        if (startHour == endHour && endMinute < startMinute) {
            errors.push("End minutes must be after start minutes.");
        }

        if (errors.length) {
            setFormErrors(errors);
            return;
        }

        const newEvent = await services.calendarEventsService.addEvent({
            title,
            year: eventRequest.year,
            month: eventRequest.month,
            day: eventRequest.day,
            startHour,
            startMinute,
            endHour,
            endMinute,
            allDay: false
        });
        
        setEvents([...events, newEvent]);
        selectEvent(newEvent);
    }


    return (
        <form className="form" onSubmit={handleSubmit}>
            <div className="form-field">
                <label>Title</label>
                <input onChange={(e) => setTitle(e.target.value)} type="text" id="title" name="title" defaultValue={eventRequest.title} />
            </div>
            <div className="form-field">
                <label>Start</label>
                <TimePicker onChange={(value) => {
                    setStartHour(value.am ? value.hour : value.hour + 12);
                    setStartMinute(value.minute ?? 0);
                    setAmPm(value.am ? 'am' : 'pm');
                    }} hour={eventRequest.startHour} minute={eventRequest.startMinute} />
            </div>
            <div className="form-field">
                <label>End</label>
                <TimePicker onChange={(value) => {
                    setEndHour(value.am ? value.hour : value.hour + 12);
                    setEndMinute(value.minute ?? 0)
                    }} hour={eventRequest.endHour ?? eventRequest.startHour} minute={eventRequest.endMinute ?? eventRequest.startMinute} />
            </div>
            <div className="form-field">
                <label>
                    <input type="checkbox" name="allDay" id="allDay" />
                    {' '}All Day
                </label>
            </div>
            <p>
                <input type="submit" value="Create" />
            </p>
            {formErrors?.length ? <div className="error-box">
                {formErrors.map(err => (
                    <p className="error">{err}</p>
                ))}
            </div> : null}
        </form>
    )
}