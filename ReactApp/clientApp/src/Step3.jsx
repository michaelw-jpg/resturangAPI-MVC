import React, { useContext } from "react";
import { BookingContext } from "./BookingContext";

export default function Step3({ mvcActionUrl, onBack }) {
    const {
        guests,
        date,
        time,
        selectedTable,
        name,
        setName,
        phoneNumber,
        setPhoneNumber,
        email,
        setEmail,
    } = useContext(BookingContext);

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
                window.location.href = "/";
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
                <div className="form-group">
                    <label>Name</label>
                    <input
                        type="text"
                        className="form-control"
                        value={name}
                        maxLength={100}
                        onChange={(e) => setName(e.target.value)}
                    />
                </div>

                <div className="form-group">
                    <label>Phone Number</label>
                    <input
                        type="text"
                        className="form-control"
                        value={phoneNumber}
                        maxLength={18}
                        onChange={(e) => setPhoneNumber(e.target.value)}
                    />
                </div>

                <div className="form-group">
                    <label>Email</label>
                    <input
                        type="email"
                        className="form-control"
                        value={email}
                        maxLength={50}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                </div>

                <div className="mt-3">
                    <button type="button" className="btn btn-secondary mr-2" onClick={onBack}>
                        Tillbaka
                    </button>
                    <button type="submit" className="btn btn-primary">
                        Create Booking
                    </button>
                </div>
            </form>
        </div>
    );
}
