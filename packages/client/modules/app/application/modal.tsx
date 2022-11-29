"use client";

import React, { useEffect, useRef } from "react";

import { useModal } from "./modal-provider";

export interface ModalProps {
    children: React.ReactNode;
    open?: boolean;
}

export default function Modal({open, children}: ModalProps) {
    const { elementRef, show, hide } = useModal();

    const ref = useRef<HTMLDivElement>(null);

    useEffect(() => {
        if (elementRef && ref.current) {
            elementRef.current = ref.current;
        }
    }, []);

    useEffect(() => {
        if (open) {
            show();
        } else {
            hide();
        }
    }, [open])

    return (
        <div ref={ref} className="modal">
            {children}
        </div>
    )
}