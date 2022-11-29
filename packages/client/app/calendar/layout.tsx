import React from "react";
import Link from "next/link";

import styles from "./layout.module.css";

export default function CalendarLayout(props: { children: React.ReactNode }) {
  const today = new Date();
  return (
    <div className={styles["calendar-layout"]}>
      <div className={styles["nav"]}>
        <Link className={styles['active']} href={`/calendar/${today.getFullYear()}/${today.getMonth() + 1}`}>
          <svg
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            fill="none"
            stroke="currentColor"
            strokeWidth="2"
            strokeLinecap="round"
            strokeLinejoin="round"
          >
            <path d="M 6 5 L 18 5 Q 20 5, 20 7 L 20 19 Q 20 21, 18 21 L 6 21 Q 4 21, 4 19 L 4 7 Q 4 5, 6 5"></path>
            <line x1="9" y1="3" x2="9" y2="7"></line>
            <line x1="15" y1="3" x2="15" y2="7"></line>
            <line x1="4" y1="11" x2="20" y2="11"></line>
          </svg>
        </Link>
      </div>
      <div className={styles["content"]}>{props.children}</div>
    </div>
  );
}
