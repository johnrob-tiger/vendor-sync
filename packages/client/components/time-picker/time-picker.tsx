import React, { useEffect, useState } from "react";

import styles from "./time-picker.module.css";

export interface TimePickerProps {
  hour?: number;
  minute?: number;
  pm?: boolean;
  onChange?: (value: { hour: number, minute: number, am: boolean }) => void;
}

function TimeSpinner({
  value,
  values,
  onChange,
}: {
  value: number;
  values: number[];
  onChange: (val: number) => void;
}) {
  const [valueIndex, setValueIndex] = useState<number>(() => {
    return values.indexOf(value) >= 0 ? values.indexOf(value) : 0;
  });

  const [spinnerValue, setSpinnerValue] = useState<number>(value ?? values[0]);

  const formatValue = (val: number) => {
    if (val < 10) {
        return '0' + val;
    }
   
    return val;
  }

  const handleUp = (e: React.MouseEvent) => {
    e.preventDefault();
    if (valueIndex + 1 >= values.length) {
      return;
    }
    setValueIndex((valueIndex) => valueIndex + 1);
  };

  const handleDown = (e: React.MouseEvent) => {
    e.preventDefault();
    if (valueIndex - 1 < 0) {
      return;
    }
    setValueIndex((valueIndex) => valueIndex - 1);    
  };

  useEffect(() => {
    onChange && onChange(spinnerValue);
  }, [spinnerValue])

  useEffect(() => {
    setSpinnerValue(values[valueIndex]);
  }, [valueIndex])

  return (
    <div className={styles["time-picker__spinner"]}>
      <button onClick={handleUp} className={styles["time-picker__spinner-up"]}></button>
      <label>{formatValue(spinnerValue)}</label>
      <button
        onClick={handleDown}
        className={styles["time-picker__spinner-down"]}
      ></button>
    </div>
  );
}

export default function TimePicker(props: TimePickerProps) {
  
  const [currentHour, setCurrentHour] = useState<number>(() => {
    if (props.hour) {
        if (props.hour == 0) {
            return 12;
        }

        if (props.hour > 12) {
            return props.hour - 12;
        }
        return props.hour;
    } else {
        return 12;
    }
  });
  const [currentMinute, setCurrentMinute] = useState<number>(props.minute ?? 0);
  const [amPm, setAmPm] = useState<'am' | 'pm'>(() => props.hour && props.hour > 11 ? 'pm' : 'am')

  const handleAmPm = (e: React.MouseEvent, value: 'am' | 'pm') => {
    e.preventDefault();
    setAmPm(value);
    handleChange(currentHour, currentMinute);
  }

  const handleChange = (hour: number, minute: number) => {
    setTimeout(() => {
        props.onChange && props.onChange({ hour, minute, am: amPm === 'am'});
    }, 0);    
  }

  return (
    <div className={styles["time-picker"]}>
      <TimeSpinner
        values={Array.from(Array(12).keys()).map((x) => x + 1)}
        value={currentHour}
        onChange={(n) => {setCurrentHour(n); handleChange(n, currentMinute)}}
      />
      <TimeSpinner
        values={[0, 15, 30, 45]}
        value={currentMinute}
        onChange={(n) => {setCurrentMinute(n); handleChange(currentHour, n)}}
      />
      <div className={styles["time-picker__spinner"]}>
        <button onClick={(e: React.MouseEvent) => handleAmPm(e, 'am')} className={styles["time-picker__spinner-up"]}></button>
        <label>{amPm == 'am' ? 'AM' : 'PM'}</label>
        <button
            onClick={(e: React.MouseEvent) => handleAmPm(e, 'pm')}
            className={styles["time-picker__spinner-down"]}
        ></button>
    </div>
    </div>
  );
}
