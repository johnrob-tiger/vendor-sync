import React, { createContext, MutableRefObject, Ref } from "react";

export interface ModalContextProps {
  show: () => void;
  hide: () => void;
  elementRef?: MutableRefObject<HTMLDivElement | undefined>;
}

const initialValues: ModalContextProps = {
  show: () => {},
  hide: () => {} 
};

const ModalContext = createContext<ModalContextProps>(initialValues);

export default ModalContext;
