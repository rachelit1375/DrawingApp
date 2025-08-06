import React, { useState } from "react";

function LoginForm({ onLogin, onSwitch }) {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const handleLogin = async () => {
        const response = await fetch("http://localhost:5150/api/user/login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ username, password }),
        });

        if (!response.ok) {
            const message = await response.text();
            setError(message || "שגיאה בהתחברות");
            return;
        }

        const user = await response.json();
        onLogin(user);
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
