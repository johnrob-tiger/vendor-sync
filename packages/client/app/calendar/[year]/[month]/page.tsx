import Link from "next/link"
import { redirect } from "next/navigation";
import SearchBox from "../../../../components/search-box/search-box";
import { services } from "../../../../config/services"
import CalendarEventList from "../../../../modules/events/application/calendar-event-list/calendar-event-list.view";
import CalendarMonthLinks from "../../../../modules/events/application/calendar-navigation/calendar-month-links.view";
import CalendarWidget from "../../../../modules/events/application/calendar-widget/calendar-widget.view";

import styles from "./page.module.css";

async function getEvents(year: number, month: number) {
    
    const events = await services.calendarEventsService.getEvents(year, month);

    return events;
}


export default async function CalendarMonth({ params }: { 
    params: { year: string, month: string }
}) {

    const yearInt = parseInt(params.year);
    const monthInt = parseInt(params.month);

    const events = await getEvents(yearInt, monthInt);

    const handleViewChange = (year: number, month: number) => {
        redirect(`/calendar/${year}/${month}`);
    }

    return (<div className={styles['month-page']}>
        <div className={styles['calendar']}>
            <SearchBox placeholder="Search" />
            <div className={styles['calendar-header']}>
                <h1>Calendar</h1>
                <CalendarMonthLinks currentYear={yearInt} currentMonth={monthInt - 1} />
            </div>
            <CalendarWidget basePath="/calendar"  year={yearInt} month={monthInt} />            
        </div>
        <div className={styles['events']}>
            <CalendarEventList events={events} />
        </div>
    </div>)
}