import React, { useState } from "react";
import { loginUser } from "../services/api";

function LoginForm({ onLogin, onSwitch }) {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const handleLogin = async () => {
        try {
            const user = await loginUser({ username, password });
            onLogin(user);
        } catch (err) {
            setError(err.message);
        }
    };

    return (
        <div className="form login-form">
            <h2>התחברות</h2>
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
            <button onClick={handleLogin}>התחבר</button>
            {error && <p className="error">{error}</p>}
            <p className="switch-link">
                אין לך עדיין שם משתמש?{" "}
                <button onClick={onSwitch}>הירשם</button>
            </p>
        </div>
    );
}

export default LoginForm;