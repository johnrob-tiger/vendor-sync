import { useState } from "react";
import TimePicker from "../../../../components/time-picker/time-picker";
import { CalendarEvent as DomainCalendarEvent } from "../../domain/calendar-event";

import { useCalendarEvents } from "../calendar-events-provider";

import { services } from "../../../../config/services";

export interface EditEventFormProps {
    event: DomainCalendarEvent;
    onSave?: () => void;
}

export default function EditEventForm({event, onSave}: EditEventFormProps) {

    const [formErrors, setFormErrors] = useState<string[]>([]);

    const [title, setTitle] = useState<string>(event.title);
    const [startHour, setStartHour] = useState<number>(() => event.startHour);
    const [endHour, setEndHour] = useState<number>(() => event.endHour ?? 0);

    const [startMinute, setStartMinute] = useState<number>(() => event.startMinute);
    const [endMinute, setEndMinute] = useState<number>(() => event.endMinute ?? 0);

    const [amPm,setAmPm] = useState<'am'|'pm'>(() => event.startHour >= 12 ? 'pm' : 'am');

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

        const updatedEvent = await services.calendarEventsService.updateEvent({
            ...event,
            title,
            startHour,
            endHour,
            startMinute,
            endMinute,
            allDay: event.allDay
        });
 
        setEvents([...events.filter(x => x.id != event.id), updatedEvent]);
        selectEvent(updatedEvent);

        onSave && onSave();
    }


    return (
        <form className="form" onSubmit={handleSubmit}>
            <div className="form-field">
                <label>Title</label>
                <input onChange={(e) => setTitle(e.target.value)} type="text" id="title" name="title" defaultValue={event.title} />
            </div>
            <div className="form-field">
                <label>Start</label>
                <TimePicker onChange={(value) => {
                    setStartHour(value.am ? value.hour : value.hour + 12);
                    setStartMinute(value.minute ?? 0);
                    setAmPm(value.am ? 'am' : 'pm');
                    }} hour={event.startHour} minute={event.startMinute} />
            </div>
            <div className="form-field">
                <label>End</label>
                <TimePicker onChange={(value) => {
                    setEndHour(value.am ? value.hour : value.hour + 12);
                    setEndMinute(value.minute ?? 0)
                    }} hour={event.endHour ?? event.startHour} minute={event.endMinute ?? event.startMinute} />
            </div>
            <div className="form-field">
                <label>
                    <input type="checkbox" name="allDay" id="allDay" />
                    {' '}All Day
                </label>
            </div>
            <p>
                <input type="submit" value="Update" />
            </p>
            {formErrors?.length ? <div className="error-box">
                {formErrors.map(err => (
                    <p className="error">{err}</p>
                ))}
            </div> : null}
        </form>
    )
}