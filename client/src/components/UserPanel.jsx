import React, { useState } from "react";
import "../css/UserPanel.css";

function UserPanel({ onLogin }) {
    const [username, setUsername] = useState("");

    const handleLogin = () => {
        if (username.trim()) {
            onLogin(username);
        }
    };

    return (
        <div className="user-panel">
            <input
                type="text"
                placeholder="הכנס שם משתמש"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
            />
            <button onClick={handleLogin}>התחבר</button>
        </div>
    );
}

export default UserPanel;
