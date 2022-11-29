import styles from "./page.module.css";
import { getDay } from "../../../../../utils/calendar";
import { services } from "../../../../../config/services";

import CalendarEventsProvider from "../../../../../modules/events/application/calendar-events-provider";
import CalendarDayView from "../../../../../modules/events/application/calendar-day-view/calendar-day-view";
import CurrentEventDetail from "../../../../../modules/events/application/current-event-detail/current-event-detail";
import Link from "next/link";

async function getEvents(year: number, month: number, day: number) {
  const events = await services.calendarEventsService.getDayEvents(
    year,
    month,
    day
  );

  return events;
}

export default async function DayPage({
  params,
}: {
  params: { year: number, month: number, day: number };
}) {

  const { monthName, dayName, suffix } = getDay(
    params.year,
    params.month,
    params.day
  );

  const events = await getEvents(params.year, params.month, params.day);

  return (
      <CalendarEventsProvider events={events}>
        <div className={styles["day-page"]}>
          <div className={styles["day-view"]}>
            <h2>
              {dayName}, {monthName} {params.day}
              <sup>{suffix}</sup>, {params.year}
            </h2>
            <CalendarDayView year={params.year} month={params.month} day={params.day} />
          </div>
          <div className={styles["event"]}>
            <CurrentEventDetail />
          </div>
        </div>
      </CalendarEventsProvider>
  );
}
