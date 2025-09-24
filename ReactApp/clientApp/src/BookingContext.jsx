// BookingContext.jsx
import React, { createContext, useState } from "react";

export const BookingContext = createContext();

export const BookingProvider = ({ children }) => {
    const [step, setStep] = useState(1);
    const [guests, setGuests] = useState("");
    const [date, setDate] = useState("");
    const [time, setTime] = useState("");
    const [selectedTable, setSelectedTable] = useState("");
    const [name, setName] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const [email, setEmail] = useState("");

    return (
        <BookingContext.Provider
            value={{
                step,
                guests,
                setGuests,
                date,
                setDate,
                time,
                setTime,
                selectedTable,
                setSelectedTable,
                name,
                setName,
                phoneNumber,
                setPhoneNumber,
                email,
                setEmail
            }}
        >
            {children}
        </BookingContext.Provider>
    );
};
