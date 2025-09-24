import React, { useContext, useEffect, useState } from "react";
import { BookingContext } from "./BookingContext";

export default function Step2({ apiBaseUrl, onNext, onBack }) {
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

    if (loading) return <div>Laddar tillgängliga bord...</div>;
    if (tables.length === 0) return <div>Inga bord finns tillgängliga</div>;

    return (
        <div>
            <h2>Välj Bord</h2>
            <div>
                <label>Bord</label>
                <select value={selectedTable || ""} onChange={(e) => setSelectedTable(e.target.value)}>
                    <option value="">-- Välj Bord --</option>
                    {tables.map((t) => (
                        <option key={t.tableId} value={t.tableId}>
                            Table {t.tableId} ({t.seats} seats)
                        </option>
                    ))}
                </select>
            </div>
            <button type="button" onClick={onBack}>Tillbaka</button>
            <button type="button" onClick={handleNext}>Nästa</button>
        </div>
    );
}
