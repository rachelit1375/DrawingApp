import React, { useState } from "react";

function RegisterForm({ onRegister, onSwitch }) {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const handleRegister = async () => {
        const response = await fetch("http://localhost:5150/api/user/register", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ username, password }),
        });

        if (!response.ok) {
            const message = await response.text();
            setError(message || "שגיאה בהרשמה");
            return;
        }

        const user = await response.json();
        onRegister(user);
    };


    return (
        <div className="form register-form">
            <h2>הרשמה</h2>
            <input
                type="text"
                placeholder="שם משתמש"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
            />

            <input
                type="password"
                placeholder="סיסמה"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
            />

            <button onClick={handleRegister}>הירשם</button>
            {error && <p className="error">{error}</p>}
            <p className="switch-link">
                יש לך כבר חשבון?{" "}
                <button onClick={onSwitch}>התחבר</button>
            </p>
        </div>
    );
}

export default RegisterForm;
