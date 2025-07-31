import React, { useState } from "react";
import "../css/PromptInput.css";

function PromptInput({ onSubmit }) {
    const [prompt, setPrompt] = useState("");

    const handleSubmit = (e) => {
        e.preventDefault();
        if (!prompt.trim()) return;

        // כאן נוכל בעתיד להמיר לפרומפט JSON דרך API
        const mockDrawingCommands = [
            { type: "circle", x: 100, y: 100, radius: 40, color: "yellow" }
        ];

        onSubmit(prompt, mockDrawingCommands);
        setPrompt("");
    };

    return (
        <form className="prompt-input-form" onSubmit={handleSubmit}>
            <input
                type="text"
                className="prompt-input-field"
                placeholder="כתוב הוראת ציור..."
                value={prompt}
                onChange={(e) => setPrompt(e.target.value)}
            />
            <button type="submit" className="prompt-submit-button">שלח</button>
        </form>
    );
}

export default PromptInput;
