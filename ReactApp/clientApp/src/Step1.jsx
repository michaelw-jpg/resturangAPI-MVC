import React, { useContext } from "react";
import { BookingContext } from "./BookingContext";

export default function Step1({ onNext }) {
    const { guests, setGuests, date, setDate, time, setTime } = useContext(BookingContext);

    const generateTimes = () => {
        const times = [];
        for (let hour = 0; hour < 24; hour++) {
            for (let min = 0; min < 60; min += 15) {
                times.push(`${String(hour).padStart(2, "0")}:${String(min).padStart(2, "0")}`);
            }
        }
        return times;
    };

    const handleNext = () => {
        if (!guests || !date || !time) {
            alert("Please enter guests, date, and time first.");
            return;
        }
        onNext();
    };

    return (
        <div>
            <h2>Ange Antal Gäster, Datum & Tid</h2>
            <div>
                <label>Guests</label>
                <input type="number" value={guests} onChange={(e) => setGuests(e.target.value)} />
            </div>
            <div>
                <label>Date</label>
                <input type="date" value={date} onChange={(e) => setDate(e.target.value)} />
            </div>
            <div>
                <label>Time</label>
                <select value={time} onChange={(e) => setTime(e.target.value)}>
                    <option value="">-- Select time --</option>
                    {generateTimes().map((t) => (
                        <option key={t} value={t}>{t}</option>
                    ))}
                </select>
            </div>
            <button type="button" onClick={handleNext}>Next</button>
        </div>
    );
}
