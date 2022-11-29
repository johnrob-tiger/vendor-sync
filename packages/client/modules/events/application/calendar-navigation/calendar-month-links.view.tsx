import Link from "next/link";
import addMonths from "date-fns/addMonths";

import styles from "./calendar-month-links.module.css";

export type CalendarMonthLinksProps = {
  currentYear: number;
  currentMonth: number;
};

export default function CalendarMonthLinks(props: CalendarMonthLinksProps) {
  const thisMonth = new Date(props.currentYear, props.currentMonth, 15);
  const nextMonth = addMonths(thisMonth, 1);
  const lastMonth = addMonths(thisMonth, -1);

  const lastMonthName = lastMonth.toLocaleString("default", { month: "short" });
  const nextMonthName = nextMonth.toLocaleString("default", { month: "short" });

  return (
    <div className={styles["calendar-month-links"]}>
      <div className={styles["calendar-month-link"]}>
        <Link
          className={styles["calendar-last-month-link"]}
          href={`/calendar/${lastMonth.getFullYear()}/${lastMonth.getMonth() + 1}`}
        ><span>{lastMonthName}</span></Link>        
      </div>
      <div className={styles["calendar-month-link"]}>        
        <Link
          className={styles["calendar-next-month-link"]}
          href={`/calendar/${nextMonth.getFullYear()}/${nextMonth.getMonth() + 1}`}
        ><span>{nextMonthName}</span></Link>
      </div>
    </div>
  );
}
