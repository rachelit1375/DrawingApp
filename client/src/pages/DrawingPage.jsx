import React, { useState } from "react";
import PromptInput from "../components/PromptInput";
import DrawingCanvas from "../components/DrawingCanvas";
import DrawingControls from "../components/DrawingControls";
import ChatHistory from "../components/ChatHistory";
import LogoutButton from "../components/LogoutButton";
import { saveDrawing, loadDrawing } from "../services/api";

function DrawingPage({ user }) {
    const [drawingsHistory, setDrawingsHistory] = useState([[]]);
    const [currentIndex, setCurrentIndex] = useState(0);
    const [chatMessages, setChatMessages] = useState([]);
    const [promptHistory, setPromptHistory] = useState([""]);
    const [lastSavedIndex, setLastSavedIndex] = useState(-1);

    const currentDrawing = drawingsHistory[currentIndex] || [];

    const handleNewPrompt = (prompt, commands, status = true) => {
        const newDrawingHistory = drawingsHistory.slice(0, currentIndex + 1);
        const updatedCommands = [...(drawingsHistory[currentIndex] || []), ...commands];
        newDrawingHistory.push(updatedCommands);
        setDrawingsHistory(newDrawingHistory);

        const newPromptHistory = promptHistory.slice(0, currentIndex + 1);
        newPromptHistory.push(prompt);
        setPromptHistory(newPromptHistory);

        setCurrentIndex(newDrawingHistory.length - 1);

        setChatMessages((prev) => [
            ...prev,
            { type: "user", text: prompt },
            status
                ? { type: "bot", text: "ציור נוסף בצד ימין 🎨" }
                : { type: "bot", text: "❌ ארעה שגיאה בשליחת הבקשה לשרת." }
        ]);
    };

    const handleClear = () => {
        const newHistory = drawingsHistory.slice(0, currentIndex + 1);
        newHistory.push([]);
        setDrawingsHistory(newHistory);
        setCurrentIndex(newHistory.length - 1);
    };

    const handleUndo = () => {
        if (currentIndex > 0) {
            setCurrentIndex(currentIndex - 1);
        }
    };

    const handleRedo = () => {
        if (currentIndex < drawingsHistory.length - 1) {
            setCurrentIndex(currentIndex + 1);
        }
    };

    const handleSave = async () => {
        try {
            if (!user) {
                alert("יש להתחבר לפני שמירה");
                return;
            }

            if (currentDrawing.length === 0) {
                setChatMessages(prev => [
                    ...prev,
                    { type: "bot", text: "אין מה לשמור – הציור ריק." }
                ]);
                return;
            }

            if (currentIndex === lastSavedIndex) {
                setChatMessages(prev => [
                    ...prev,
                    { type: "bot", text: "הציור כבר נשמר – לא נוספו פקודות חדשות." }
                ]);
                return;
            }

            const drawingName = prompt("הזן שם לציור:", "ללא שם");
            if (!drawingName) return;

            await saveDrawing({
                userId: user.id,
                name: drawingName,
                prompt: promptHistory.slice(0, currentIndex + 1).join("\n"),
                commandJson: JSON.stringify(drawingsHistory.slice(0, currentIndex + 1))
            });

            setLastSavedIndex(currentIndex);
            setChatMessages(prev => [
                ...prev,
                { type: "bot", text: "הציור נשמר בהצלחה✅" }
            ]);
        } catch (err) {
            setChatMessages(prev => [
                ...prev,
                { type: "bot", text: "❌ קרתה שגיאה בשמירה." }
            ]);
        }
    };

    const handleLoadDrawing = async (drawingId) => {
        try {
            const drawing = await loadDrawing(drawingId);
            const parsedCommands = JSON.parse(drawing.commandJson);

            setDrawingsHistory(parsedCommands);
            setCurrentIndex(parsedCommands.length - 1);
            setLastSavedIndex(parsedCommands.length - 1);
            setPromptHistory(drawing.prompt.split("\n"));
            setChatMessages((prev) => [
                ...prev,
                { type: "bot", text: `✅ הציור "${drawing.name}" נטען בהצלחה` },
            ]);
        } catch (err) {
            console.error(err);
            setChatMessages((prev) => [
                ...prev,
                { type: "bot", text: "❌ שגיאה בטעינת הציור" },
            ]);
        }
    };

    const handleLogout = () => {
        window.location.reload(); // טוען מחדש את האפליקציה ומאפס את ה־user
    };

    return (
        <div className="app-container">
            <LogoutButton onLogout={handleLogout} />
            <div className="left-panel">
                <h2 className="chat-title">הצ'אט שלך עם הבוט 💬</h2>
                <ChatHistory messages={chatMessages} />
                <PromptInput prevDrawings={currentDrawing} onSubmit={handleNewPrompt} />
            </div>
            <div className="right-panel">
                <DrawingControls
                    userId={user.id}
                    onClear={handleClear}
                    onUndo={handleUndo}
                    onRedo={handleRedo}
                    onSave={handleSave}
                    onLoad={handleLoadDrawing}
                />
                <DrawingCanvas commands={currentDrawing} />
            </div>
        </div>
    );
}

export default DrawingPage;