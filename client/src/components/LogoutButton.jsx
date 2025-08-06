import React from "react";
import "../css/LogoutButton.css";

function LogoutButton({ onLogout }) {
    return (
        <div className="logout-container">
            <button className="logout-button" onClick={onLogout}>
                התנתקות 🔒
            </button>
        </div>
    );
}

export default LogoutButton;
