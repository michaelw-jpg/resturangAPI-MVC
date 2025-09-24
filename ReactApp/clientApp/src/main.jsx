import React from "react";
import ReactDOM from "react-dom/client";
import BookingForm from "./BookingForm";

window.mountBookingForm = (elementId, apiBaseUrl, mvcActionUrl) => {
    const container = document.getElementById(elementId);
    if (!container) return;

    const root = ReactDOM.createRoot(container);
    root.render(<BookingForm apiBaseUrl={apiBaseUrl} mvcActionUrl={mvcActionUrl} />);
};
