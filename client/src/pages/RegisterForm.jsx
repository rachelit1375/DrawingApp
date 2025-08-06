import React, { useState } from "react";
import { registerUser } from "../services/api";

function RegisterForm({ onRegister, onSwitch }) {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const handleRegister = async () => {
        try {
            const user = await registerUser({ username, password });
            onRegister(user);
        } catch (err) {
            setError(err.message);
        }
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