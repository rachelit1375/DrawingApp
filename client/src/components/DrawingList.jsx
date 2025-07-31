import React, { useEffect, useState } from "react";
import "../css/DrawingList.css";

function DrawingList({ onSelect }) {
    const [drawings, setDrawings] = useState([]);

    useEffect(() => {
        // קריאה לשרת להבאת רשימת ציורים שמורים
        // זהו מיקום לדוגמה – ניתן להחליף בקריאה אמיתית ל־API
        const mockData = [
            { id: 1, name: "שמש צהובה" },
            { id: 2, name: "בית עם גג אדום" },
            { id: 3, name: "פרח וספסל" },
        ];
        setDrawings(mockData);
    }, []);

    return (
        <div className="drawing-list">
            <h4>🎨 הציורים שלי</h4>
            <ul>
                {drawings.map((drawing) => (
                    <li key={drawing.id} onClick={() => onSelect(drawing.id)}>
                        {drawing.name}
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default DrawingList;
