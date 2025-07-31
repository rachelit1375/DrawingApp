import React from "react";
import "../css/DrawingControls.css";

function DrawingControls({ onClear, onUndo, onRedo, onSave, onLoad }) {
    return (
        <div className="drawing-controls">
            <button onClick={onUndo}>↩️ בטל</button>
            <button onClick={onRedo}>↪️ החזר</button>
            <button onClick={onClear}>🧹 נקה</button>
            <button onClick={onSave}>💾 שמור</button>
            <button onClick={onLoad}>📂 טען</button>
        </div>
    );
}

export default DrawingControls;
