import React, { useContext, useEffect, useState } from "react";
import { BookingContext } from "./BookingContext";

export default function Step2({ apiBaseUrl, onNext }) {
    const { guests, date, time, selectedTable, setSelectedTable } = useContext(BookingContext);
    const [tables, setTables] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (!guests || !date || !time) return;

        const fetchTables = async () => {
            setLoading(true);
            try {
                const res = await fetch(`${apiBaseUrl}api/Tables/availabletables?date=${date}T${time}&guests=${guests}`);
                const data = await res.json();
                setTables(data);
            } catch (err) {
                console.error(err);
                alert("Failed to fetch tables");
            } finally {
                setLoading(false);
            }
        };
        fetchTables();
    }, [guests, date, time, apiBaseUrl]);

    const handleNext = () => {
        if (!selectedTable) {
            alert("Please select a table.");
            return;
        }
        onNext();
    };

    if (loading) return <div>Loading available tables...</div>;
    if (tables.length === 0) return <div>No tables available for this time.</div>;

    return (
        <div>
            <h2>Välj Bord</h2>
            <div>
                <label>Table</label>
                <select value={selectedTable || ""} onChange={(e) => setSelectedTable(e.target.value)}>
                    <option value="">-- Select Table --</option>
                    {tables.map((t) => (
                        <option key={t.tableId} value={t.tableId}>
                            Table {t.tableId} ({t.seats} seats)
                        </option>
                    ))}
                </select>
            </div>
            <button type="button" onClick={handleNext}>Next</button>
        </div>
    );
}
