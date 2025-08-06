import React, { useState } from "react";
import "../css/PromptInput.css";
import { sendPrompt } from "../services/api";

function PromptInput({ onSubmit, prevDrawings }) {
    const [prompt, setPrompt] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!prompt.trim()) return;
        try {
            const drawingCommands = await sendPrompt(prompt, prevDrawings);
            onSubmit(prompt, drawingCommands);
        } catch (error) {
            console.error("Error:", error);
            onSubmit(prompt, [], false);
        }
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