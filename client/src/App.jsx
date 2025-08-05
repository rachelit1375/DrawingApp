import React, { useState } from "react";
import PromptInput from "./components/PromptInput";
import DrawingCanvas from "./components/DrawingCanvas";
import DrawingControls from "./components/DrawingControls";
import ChatHistory from "./components/ChatHistory";
// import DrawingList from "./components/DrawingList";
// import UserPanel from "./components/UserPanel";
import "./App.css";

function App() {
  const [drawingsHistory, setDrawingsHistory] = useState([[]]); // כל הציורים
  const [currentIndex, setCurrentIndex] = useState(0); // איזה ציור מוצג כרגע
  const [chatMessages, setChatMessages] = useState([]);
  const [promptHistory, setPromptHistory] = useState([""]);//כל הפרומפטים
  const [userId] = useState(1);

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
      status ? { type: "bot", text: "ציור נוסף בצד ימין 🎨" } : { type: "bot", text: "❌ ארעה שגיאה בשליחת הבקשה לשרת." }
    ]);
  };

  const handleClear = () => {
    const newHistory = drawingsHistory.slice(0, currentIndex + 1);
    newHistory.push([]); // ציור ריק
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
      const drawingName = prompt("הזן שם לציור:", "ללא שם");
      if (!drawingName) return;

      const fullPrompt = promptHistory
        .slice(0, currentIndex + 1)
        .join("\n");

      const res = await fetch("http://localhost:5150/api/drawing", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          userId,
          name: drawingName,
          prompt: fullPrompt,
          commandsJson: JSON.stringify(drawingsHistory)
        }),
      });
      if (!res.ok) throw new Error(await res.text());
      const saved = await res.json();
      setChatMessages(prev => [
        ...prev,
        { type: "bot", text: "✅ הציור נשמר בהצלחה!" }
      ]);
    } catch (err) {
      console.error(err);
      setChatMessages(prev => [
        ...prev,
        { type: "bot", text: "❌ קרתה שגיאה בשמירה." }
      ]);
    }
  };

  return (
    <div className="app-container">
      <div className="left-panel">
        <h2 className="chat-title">הצ'אט שלך עם הבוט 💬</h2>
        <ChatHistory messages={chatMessages} />
        <PromptInput prevDrawings={currentDrawing} onSubmit={handleNewPrompt} />
      </div>
      <div className="right-panel">
        <DrawingControls
          onClear={handleClear}
          onUndo={handleUndo}
          onRedo={handleRedo}
          onSave={handleSave}
        />
        <DrawingCanvas commands={currentDrawing} />
      </div>
    </div>
  );
}

export default App;
