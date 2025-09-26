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
            <div className="form-group">
                <label>Antal Gäster</label>
                <input
                    type="number"
                    className="form-control"
                    value={guests}
                    onChange={(e) => setGuests(e.target.value)}
                />
            </div>

            <div className="form-group">
                <label>Datum</label>
                <input
                    type="date"
                    className="form-control"
                    value={date}
                    onChange={(e) => setDate(e.target.value)}
                />
            </div>

            <div className="form-group">
                <label>Tid</label>
                <select
                    className="form-control"
                    value={time}
                    onChange={(e) => setTime(e.target.value)}
                >
                    <option value="">-- Select time --</option>
                    {generateTimes().map((t) => (
                        <option key={t} value={t}>
                            {t}
                        </option>
                    ))}
                </select>
            </div>

            <button type="button" className="btn btn-primary mt-3" onClick={handleNext}>
                Nästa
            </button>
        </div>
    );
}
