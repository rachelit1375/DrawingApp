import React, { useState } from "react";
import PromptInput from "./components/PromptInput";
import DrawingCanvas from "./components/DrawingCanvas";
import DrawingControls from "./components/DrawingControls";
import ChatHistory from "./components/ChatHistory";
import DrawingList from "./components/DrawingList";
import UserPanel from "./components/UserPanel";
import "./App.css";

function App() {
  const [drawingCommands, setDrawingCommands] = useState([]);
  const [chatMessages, setChatMessages] = useState([]);

  const handleNewPrompt = (prompt, commands) => {
    setDrawingCommands(commands);
    setChatMessages([...chatMessages, { type: "user", text: prompt }]);
    setChatMessages((prev) => [...prev, { type: "bot", text: "ציור נוסף בצד ימין 🌸" }]);
  };

  const handleClear = () => {
    setDrawingCommands([]);
  };

  return (
    <div className="app-container">
      <div className="left-panel">
        <h2 className="chat-title">הצ'אט שלך עם הבוט 💬</h2>
        <ChatHistory messages={chatMessages} />
        <PromptInput onSubmit={handleNewPrompt} />
      </div>
      <div className="right-panel">
        <DrawingControls onClear={handleClear} />
        <DrawingCanvas commands={drawingCommands} />
      </div>
    </div>
  );
}

export default App;
