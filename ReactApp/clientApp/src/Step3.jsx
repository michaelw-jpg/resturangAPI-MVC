import React, { useContext } from "react";
import { BookingContext } from "./BookingContext";

export default function Step3({ mvcActionUrl }) {
    const { guests, date, time, selectedTable, name, setName, phoneNumber, setPhoneNumber, email, setEmail } =
        useContext(BookingContext);

    const handleSubmit = async (e) => {
        e.preventDefault();

        const payload = {
            Name: name,
            PhoneNumber: phoneNumber,
            Email: email,
            TableId_FK: Number(selectedTable),
            Guests: Number(guests),
            BookingTime: `${date}T${time}`,
        };

        try {
            const response = await fetch(mvcActionUrl, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(payload),
            });

            if (response.ok) {
                alert("Booking created successfully!");
                window.location.href = "/Booking/Index";
            } else {
                const text = await response.text();
                alert("Failed to create booking: " + text);
            }
        } catch (err) {
            console.error(err);
            alert("Something went wrong.");
        }
    };

    return (
        <div>
            <h2>Ange kontaktinformation</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Name</label>
                    <input value={name} onChange={(e) => setName(e.target.value)} maxLength={100} />
                </div>
                <div>
                    <label>Phone Number</label>
                    <input value={phoneNumber} onChange={(e) => setPhoneNumber(e.target.value)} maxLength={18} />
                </div>
                <div>
                    <label>Email</label>
                    <input value={email} onChange={(e) => setEmail(e.target.value)} maxLength={50} />
                </div>
                <button type="submit">Create Booking</button>
            </form>
        </div>
    );
}
