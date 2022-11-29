"use client";

import React, { useRef } from "react";

import styles from "./search-box.module.css";

type SearchBoxProps = {
  placeholder?: string;
  onChange?: (searchText: string) => void;
};

export default function SearchBox(props: SearchBoxProps) {
  const inputRef = useRef<HTMLInputElement>(null);

  const handleKeyDown = (e: React.KeyboardEvent) => {
    if (e.key === "13" && inputRef.current) {
      props.onChange && props.onChange(inputRef.current.value);
    }
  };
  return (
    <div className={styles["search-box"]}>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 24 24"
        fill="none"
        stroke="currentColor"
        strokeWidth="2"
        strokeLinecap="round"
        strokeLinejoin="round"
      >
        <circle cx="14" cy="10" r="7"></circle>
        <line x1="4" y1="20" x2="9" y2="15"></line>
      </svg>
      <input
        ref={inputRef}
        onKeyDown={handleKeyDown}
        type="search"
        placeholder={props.placeholder}
      />
    </div>
  );
}
