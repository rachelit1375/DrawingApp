import React, { useEffect, useState } from "react";
import "../css/DrawingControls.css";
import { getUserDrawings } from "../services/api";

function DrawingControls({ userId, onClear, onUndo, onRedo, onSave, onLoad }) {
    const [drawings, setDrawings] = useState([]);
    const [selectedId, setSelectedId] = useState("");

    const fetchDrawings = async () => {
        try {
            const data = await getUserDrawings(userId);
            setDrawings(data);
        } catch (err) {
            console.error("שגיאה בטעינת הציורים:", err);
        }
    };

    useEffect(() => {
        if (userId) {
            fetchDrawings();
        }
    }, [userId]);

    const handleSelectChange = (e) => {
        const value = e.target.value;
        if (value === "__refresh__") {
            fetchDrawings();
            setSelectedId("");
        } else {
            setSelectedId(value);
        }
    };

    const handleLoad = () => {
        if (selectedId) {
            onLoad(parseInt(selectedId));
        }
    };

    return (
        <div className="drawing-controls">
            <button onClick={onUndo}>↩️ בטל</button>
            <button onClick={onRedo}>↪️ החזר</button>
            <button onClick={onClear}>🧹 נקה</button>
            <button onClick={onSave}>💾 שמור</button>

            <select value={selectedId} onChange={handleSelectChange}>
                <option value="">📂 בחר ציור לטעינה</option>
                <option value="__refresh__">🔄 רענן רשימה</option>
                {drawings.map((drawing) => (
                    <option key={drawing.id} value={drawing.id}>
                        {drawing.name}
                    </option>
                ))}
            </select>

            <button onClick={handleLoad} disabled={!selectedId || selectedId === "__refresh__"}>🚀 טען</button>
        </div>
    );
}

export default DrawingControls;
