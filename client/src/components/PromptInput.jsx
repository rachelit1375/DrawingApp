import React, { useState } from "react";
import "../css/PromptInput.css";

function PromptInput({ onSubmit }) {
    const [prompt, setPrompt] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!prompt.trim()) return;

        const response = await fetch("http://localhost:5150/api/prompt", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ prompt: prompt, prevDrawings: [] })
        });
        const drawingCommands = await response.json();

        onSubmit(prompt, drawingCommands);
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
