import React, { useState } from "react";
import Step1 from "./Step1";
import Step2 from "./Step2";
import Step3 from "./Step3";
import { BookingProvider } from "./BookingContext";

export default function BookingForm({ apiBaseUrl, mvcActionUrl }) {
    const [step, setStep] = useState(1);

    const goNext = () => setStep(step + 1);
    const goBack = () => setStep(step - 1);


    return (
        <BookingProvider>
            <div>
                {step === 1 && <Step1 onNext={goNext} />}
                {step === 2 && <Step2 apiBaseUrl={apiBaseUrl} onNext={goNext} onBack={goBack} />}
                {step === 3 && <Step3 mvcActionUrl={mvcActionUrl} onBack={goBack} />}
            </div>
        </BookingProvider>
    );
}
