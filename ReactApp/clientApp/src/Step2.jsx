import React, { useContext, useEffect, useState } from "react";
import { BookingContext } from "./BookingContext";

export default function Step2({ apiBaseUrl, onNext, onBack }) {
    const { guests, date, time, selectedTable, setSelectedTable } = useContext(BookingContext);
    const [tables, setTables] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        if (!guests || !date || !time) return;

        // Properly combine date and time into a Date object
        const [hours, minutes] = time.split(":").map(Number);
        const selectedDateTime = new Date(date);
        selectedDateTime.setHours(hours, minutes, 0, 0);
        const now = new Date();
        console.log("Selected DateTime:", selectedDateTime);

        // Client-side validation for past date
        if (selectedDateTime < now) {
            setError("Du får inte välja ett passerat datum");
            setTables([]);
            setLoading(false);
            return;
        }

        const fetchTables = async () => {
            setLoading(true);
            setError("");
            try {
                const res = await fetch(
                    `${apiBaseUrl}api/Tables/availabletables?date=${date}T${time}&guests=${guests}`
                );
                console.log("API Response Status:", res.status);

                // Read the response body once, regardless of status
                const responseText = await res.text();
                console.log("API Response Text:", responseText);
                if (!res.ok) {
                    // Handle error response
                    let translatedMessage = responseText;

                    if (responseText.includes("Booking date cannot be in the past")) {
                        translatedMessage = "Du får inte välja ett passerat datum";
                    }

                    throw new Error(translatedMessage || `API error: ${res.status}`);
                }

                // Parse JSON from text if response is OK
                const data = JSON.parse(responseText);
                setTables(data);

            } catch (err) {
                console.error(err);
                // Check if it's a JSON parsing error and provide a more specific message
                if (err instanceof SyntaxError && err.message.includes("JSON")) {
                    setError("Server returned invalid response");
                } else {
                    setError(err.message);
                }
                setTables([]);
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
    if (error) return <div>{error}</div>;
    if (tables.length === 0) return <div>Inga bord finns tillgängliga</div>;

    return (
        <div>
            <h2>Välj Bord</h2>
            <div className="form-group">
                <label>Bord</label>
                <select
                    className="form-control"
                    value={selectedTable || ""}
                    onChange={(e) => setSelectedTable(e.target.value)}
                >
                    <option value="">-- Välj Bord --</option>
                    {tables.map((t) => (
                        <option key={t.tableId} value={t.tableId}>
                            Table {t.tableId} ({t.seats} seats)
                        </option>
                    ))}
                </select>
            </div>
            <div className="mt-3">
                <button
                    type="button"
                    className="btn btn-secondary mr-2"
                    onClick={onBack}
                >
                    Tillbaka
                </button>
                <button
                    type="button"
                    className="btn btn-primary"
                    onClick={handleNext}
                    disabled={tables.length === 0 || !selectedTable}
                >
                    Nästa
                </button>
            </div>
        </div>
    );
}