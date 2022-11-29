"use client";

import React from "react";

import styles from "./fab.module.css";

export interface FabProps extends React.HTMLAttributes<HTMLButtonElement> {
  size?: "sm" | "md" | "lg";
  position?: "top-left" | "top-middle" | "top-right";
  fixed?: boolean;
  children: React.ReactNode;
}

export default function Fab({ size = "md", fixed, position, children, ...rest }: FabProps) {
  return (
    <button
      className={[
        styles["fab"], 
        styles[`fab--${size}`],
        !fixed && position ? styles[`fab--${position}`] : "",
        fixed ? styles['fab--fixed'] : ""
        ].join(" ")}
      {...rest}
    >
      {children}
    </button>
  );
}
