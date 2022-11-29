"use client";

import React, { useState } from "react";
import Link from "next/link";
import { getMonth } from "../../../../utils/calendar";

import styles from "./calendar-widget.module.css";

export type CalendarWidgetProps = {
  year: number;
  month: number;
  basePath: string;
  selectedDate?: Date;
  detailView?: string;  
};

type UiView = "calendar" | "year" | "month";

export default function CalendarWidget(props: CalendarWidgetProps) {
  const [selectedDate, setSelectedDate] = useState<Date | undefined>(
    props.selectedDate
  );

  const [view, setView] = useState<UiView>(() => "calendar");
  const [pickYear, setPickYear] = useState<number | undefined>();
  const [pickMonth, setPickMonth] = useState<number | undefined>();

  const { daysInMonth, daysInLastMonth, offset, monthName } = getMonth(
    props.year,
    props.month
  );

  const handlePrevClick = (day: number) => {
    if (props.month === 1) {
      setSelectedDate(new Date(props.year - 1, 11, day));
    } else {
      setSelectedDate(new Date(props.year, props.month - 1, day));
    }
  };

  const handleDayClick = (day: number) => {
    var dt = new Date(props.year, props.month - 1, day);
    setSelectedDate(dt);
  };

  const handleViewClick = () => {
    if (view === 'calendar') {
        setView('year');
        return;
    } 
    setView('calendar');
  }

  const handleYearClick = (year: number) => {
    setPickYear(year);
    setView('month');
  }

  const thisYear = new Date().getFullYear();

  return (
    <div
      className={[styles["calendar-widget"], styles[`${view}-view`]].join(" ")}
    >
      <div>
        <h2>{monthName + " " + props.year}</h2>
        <button onClick={handleViewClick}>
          {view === "calendar" && (
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
              <circle cx="12" cy="12" r="1" />
              <circle cx="12" cy="5" r="1" />
              <circle cx="12" cy="19" r="1" />
            </svg>
          )}
          {view !== "calendar" && (
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
      {view === "calendar" && (
        <>
          {["SU", "MO", "TU", "WE", "TH", "FR", "SA"].map((str) => (
            <span key={str} className={styles["heading"]}>
              {str}
            </span>
          ))}
          {offset > 1
            ? Array.from(Array(offset).keys())
                .reverse()
                .map((i) => (
                    <Link key={`prev_${i}`} className={[styles['null'], styles['date2']].join(' ')} href={`${props.basePath}/${props.year - 1}/${props.month === 1 ? 12 : props.month - 1}/${daysInLastMonth - i + 1}`}>{daysInLastMonth - i + 1}</Link>                  
                ))
            : null}
          {Array.from(Array(daysInMonth).keys()).map((day) => (
            <Link                
              key={`curr_${day + 1}`}
              href={`${props.basePath}/${props.year}/${props.month}/${day + 1}`}
              className={`${
                day + 1 === selectedDate?.getDate() ? styles["active"] : ""
              } ${day + 1 > 9 ? styles["date2"] : ""}`}
            >
              {day + 1}
            </Link>
          ))}
        </>
      )}
      {view === "year" && (
        <div className={styles["full"]}>
          {Array.from(Array(50).keys())
            .reverse()
            .map((i) => (
              <button onClick={() => handleYearClick(thisYear - i)} key={`year_${i}`}>{thisYear - i}</button>
            ))}
            {Array.from(Array(50).keys())
            .map((i) => (
                <button onClick={() => handleYearClick(thisYear + 1 + i)} key={`year_n${i}`}>{thisYear + 1 + i}</button>
            ))}
        </div>
      )}
      {view === "month" && 
      <div className={styles["full"]}>
      {Array.from(Array(12).keys())
        .map((i) => (
            <Link key={`month_${i}`} href={`${props.basePath}/${pickYear}/${i + 1}`}>{new Date(thisYear, i, 1).toLocaleString('default', { month: 'short'})}</Link>
        ))}
    </div>}
    </div>
  );
}
