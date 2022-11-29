"use client";

import React, { useState, useRef, useContext } from "react";
import ModalContext, { ModalContextProps } from "../infrastructure/modal-context";

export interface ModalProviderProps {
    children: React.ReactNode;
}

export default function ModalProvider(props: ModalProviderProps) {

    const modalRef = useRef<HTMLDivElement>();
    const [isOpen, setIsOpen] = useState<boolean>(false);

    const state: ModalContextProps = {
        elementRef: modalRef,
        show: () => { 
            setIsOpen(true);
            const $dialog = document.querySelector("#modal-dialog");
            if ($dialog) {
                $dialog.classList.add('open');

                if (modalRef.current) {
                    $dialog.appendChild(modalRef.current);
                    modalRef.current.style.display = 'block';
                }
            }

        },
        hide: () => { 
            if (!isOpen) { return; }
            setIsOpen(false);
            const $dialog = document.querySelector("#modal-dialog");
            if ($dialog) {
                $dialog.classList.remove('open');

                if (modalRef.current) {
                    modalRef.current.style.display = 'none';
                }

            }
        }
    }

    return (
        <ModalContext.Provider value={state}>
            {props.children}
        </ModalContext.Provider>
    )
}

export const useModal = () => {
    return useContext(ModalContext);
}